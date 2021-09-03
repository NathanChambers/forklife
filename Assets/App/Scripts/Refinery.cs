using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Refinery : MonoBehaviour {
    public enum State {
        IDLE,
        PROCESSING,
    }

    public Door cargoDoor;
    public MineralDetector detector;
    public UIRefineryTerminal terminal;
    public ParticleSystem processingPfx;
    public float processingTime;
    private float processingTimer;

    private State state = State.IDLE;

    public string UIProcessingDesc => $"Processing {processingTime - processingTimer:F1}";
    public float UIProcessingValue => processingTimer / processingTime;

    public void Awake() {
        processingPfx.enableEmission = false;
    }

    public void UICallback_StartProcessing() {
        if(state != State.IDLE) {
            return;
        }

        if(detector.Minerals.Count <= 0) {
            return;
        }

        state = State.PROCESSING;
        processingTimer = 0.0f;
        processingPfx.enableEmission = true;
        terminal.SetState(UIRefineryTerminal.UIState.PROCESSING);
        cargoDoor.Close();

        StartCoroutine(ProcessingAsync());
    }

    public void Update() {
        if(state != State.PROCESSING) {
            return;
        }

        if(processingTimer < processingTime) {
            processingTimer += Time.deltaTime;
            return;
        }

        state = State.IDLE;
        processingPfx.enableEmission = false;
        terminal.SetState(UIRefineryTerminal.UIState.OVERVIEW);
        cargoDoor.Open();
    }

    private IEnumerator ProcessingAsync() {
        yield return new WaitForSeconds(3.0f);

        for(int i = detector.Minerals.Count - 1; i >= 0; --i) {
            var mineral = detector.Minerals[i];
            detector.Remove(mineral);
            GameObject.Destroy(mineral.gameObject);
        }

        yield break;
    }
}
