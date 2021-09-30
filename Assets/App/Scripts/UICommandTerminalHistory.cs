using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UICommandTerminalHistory : MonoBehaviour {
    [SerializeField] private TMPro.TMP_Text line;
    private string output = string.Empty;

    public void Write(string log) {
        output = $"{output}{log}";
        line.text = output;
    }
}
