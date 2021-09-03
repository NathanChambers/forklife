using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelModel : MonoBehaviour {
    public WheelCollider wc;
    public bool invert = false;
    public void Update() {
        var euler = transform.localRotation.eulerAngles;
        euler.y =  invert ? -wc.steerAngle : wc.steerAngle;
        transform.localRotation = Quaternion.Euler(euler);
    }
}

