using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimInteraction : MonoBehaviour {
    public Animator targetAnimator;
    public float duration = 1.0f;
    public AnimationCurve doorCurve;
    private float delta = 0.0f;
    private bool opening = false;


    public void Invoke() {
        opening = !opening;
    }

    public void Update() {
        if(delta < 1.0f && opening == true) {
            delta += Time.deltaTime / duration;
        } else if(delta > 0.0f && opening == false) {
            delta -= Time.deltaTime / duration;
        }
        delta = Mathf.Clamp(delta, 0.0f, 1.0f);
        float frame = doorCurve.Evaluate(delta);
        targetAnimator.Play("DoorAction",0,frame);
    }

}

