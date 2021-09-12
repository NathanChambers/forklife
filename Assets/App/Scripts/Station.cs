using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Station : MonoBehaviour {
    public SphereCollider clearZone;
    public Beacon beaconPrefab;
    public float beaconSpacing;

    public void Awake() {
        int beaconCount = (int)(360.0f / beaconSpacing);
        for(int i = 0; i < beaconCount; ++i) {
            GameObject beaconInstance = GameObject.Instantiate(beaconPrefab.gameObject);
            beaconInstance.transform.SetParent(transform, false);
            beaconInstance.transform.localPosition = Quaternion.AngleAxis(beaconSpacing * i, transform.up) * (transform.forward * clearZone.radius);
            beaconInstance.transform.localRotation = Quaternion.identity;
        }
    }

    public void OnTriggerEnter(Collider collider) {
        Asteroid asteroid = collider.GetComponentInParent<Asteroid>();
        if(asteroid != null) {
            GameObject.Destroy(asteroid.gameObject);
        }
    }
}
