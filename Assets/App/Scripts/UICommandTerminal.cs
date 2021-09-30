using System;
using System.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UICommandTerminal : MonoBehaviour {
    [SerializeField] private UICommandTerminalHistory historyPrefab;
    [SerializeField] private LayoutGroup layout;
    [SerializeField] private UICommandTerminalInput commandInput;


    private Queue<UICommandTerminalHistory> history = new Queue<UICommandTerminalHistory>();
    private UICommandTerminalHistory runningCommand = null;

    [SerializeField] private TMPro.TMP_InputField inputField;

    public delegate TerminalProgram CreateProgram();

    private Dictionary<string, CreateProgram> programs = new Dictionary<string, CreateProgram>() {
        {"echo", () => {return new CMDEcho();}},
        {"test", () => {return new CMDTest();}},
    };

    public void ExecuteCommand(string command) {
        var args = command.Split(' ');
        if(args.Length <= 0) {
            return;
        }
        
        var cmd = args[0];
        if(programs.ContainsKey(cmd) == false) {
            return;
        }

        var program = programs[cmd]();
        program.Run(OnRun, OnTerminated, OnOutput, args);

        inputField.text = string.Empty;
    }

    public void Awake() {
        inputField.onSubmit.AddListener(ExecuteCommand);
    }

    public void OnRun() {
        commandInput.gameObject.SetActive(false);
        UICommandTerminalHistory stdout = null;
        if(history.Count > 3) {
            stdout = history.Dequeue();
        } else {
            stdout = GameObject.Instantiate(historyPrefab);
        }
        
        runningCommand = stdout;
        history.Enqueue(stdout);
        stdout.transform.SetParent(layout.transform, false);
        stdout.transform.SetAsLastSibling();
        commandInput.transform.SetAsLastSibling();
    }

    public void OnTerminated() {
        runningCommand = null;
        commandInput.gameObject.SetActive(true);
    }

    public void OnOutput(string stdout) {
        runningCommand.Write(stdout);
    }
}


public abstract class TerminalProgram {
    public abstract void Run(Action run, Action terminate, Action<string> output, params string[] args);
}

public class CMDTest : TerminalProgram {
    public override void Run(Action run, Action terminate, Action<string> output, params string[] args) {
        Program(run, terminate, output, args);
    }

    private async void Program(Action run, Action terminate, Action<string> output, params string[] args) {
        run?.Invoke();
        output?.Invoke("Processing...\n");
        await Task.Delay(3000);
        output?.Invoke("Hacking Complete\n");
        terminate?.Invoke();
    }

}

public class CMDEcho : TerminalProgram {
    public override void Run(Action run, Action terminate, Action<string> output, params string[] args) {
        Program(run, terminate, output, args);
    }

    private async void Program(Action run, Action terminate, Action<string> output, params string[] args) {
        run?.Invoke();

        if(args.Length < 2) {
            output?.Invoke("Not enough arguments");
            terminate?.Invoke();
            return;
        }

        output?.Invoke(args[1]);
        await Task.Delay(1000);
        terminate?.Invoke();
    }
}