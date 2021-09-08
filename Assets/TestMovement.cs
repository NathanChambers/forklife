using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMovement : MonoBehaviour {
    public float minX;
    public float maxX;
    public float rotSpeed;

    private Vector3 initialPosition;
    
    public void Awake() {
        initialPosition = transform.position;
    }
}
