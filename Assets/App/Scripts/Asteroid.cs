using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour {
    private Vector3 axis = Vector3.zero;
    private float speed = 0.0f;

    public void Awake() {
        axis = new Vector3(UnityEngine.Random.Range(-5, 5), UnityEngine.Random.Range(-5, 5), UnityEngine.Random.Range(-5, 5)).normalized;
        speed = Random.Range(-5.0f,5.0f);
    }

    public void Update() {
        Quaternion delta = Quaternion.Euler(axis * speed * Time.deltaTime);
        transform.localRotation *= delta;
    }
}
