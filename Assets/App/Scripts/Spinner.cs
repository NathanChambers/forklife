using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spinner : MonoBehaviour {
    public Vector3 axis;
    public float speed;

    public void Update() {
        transform.localRotation *= Quaternion.Euler(axis * speed * Time.deltaTime);
    }
}
