using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIRefineryTerminal : MonoBehaviour {
    public enum UIState {
        OVERVIEW,
        PROCESSING,
    }

    public Refinery refinery;
    public GameObject overview;
    public GameObject processing;

    public TMPro.TMP_Text processingDesc;
    public RectTransform processingFillRect;
    private UIState state = UIState.OVERVIEW;
    

    public void LinkRefinery(Refinery refinery) {
        this.refinery = refinery;
    }

    public void Refresh() {
        overview.SetActive(state == UIState.OVERVIEW);
        processing.SetActive(state == UIState.PROCESSING);
    }

    public void Update() {
        if(state != UIState.PROCESSING) {
            return;
        }

        processingDesc.text = refinery.UIProcessingDesc;
        processingFillRect.anchorMax = new Vector2(refinery.UIProcessingValue, 1.0f);
        processingFillRect.offsetMin = Vector2.zero;
        processingFillRect.offsetMax = Vector2.zero;
    }

    public void SetState(UIState state) {
        if(this.state == state) {
            return;
        }

        this.state = state;
        Refresh();
    }
}
