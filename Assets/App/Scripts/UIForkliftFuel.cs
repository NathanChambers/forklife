using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIForkliftFuel : MonoBehaviour {
    public enum UIState {
        LEVEL,
    }

    public Forklift forklift;
    public GameObject level;

    public RectTransform levelFillRect;

    public void LinkForklift(Forklift forklift) {
        this.forklift = forklift;
    }

    public void Update() {
        if(forklift.pilot == null) {
            levelFillRect.anchorMax = new Vector2(0.0f, 1.0f);
            return;
        }

        levelFillRect.anchorMax = new Vector2((forklift.battery.Power / forklift.battery.MaxPower), 1.0f);
        levelFillRect.offsetMin = Vector2.zero;
        levelFillRect.offsetMax = Vector2.zero;
    }
}