using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beacon : MonoBehaviour {
    public Billboard billboard;
    public Light mainLight;

    public void Update() {
        billboard.SetColor(mainLight.color);
    }
}
