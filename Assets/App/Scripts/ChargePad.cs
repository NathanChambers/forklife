using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargePad : MonoBehaviour
{

    private void OnTriggerEnter(Collider other) {
        Battery battery = other.GetComponentInChildren<Battery>();
        if (battery != null ) {
            battery.batteryState(Battery.State.CHARGE);
        }
    }

    private void OnTriggerStay(Collider other) {
        Battery battery = other.GetComponentInChildren<Battery>();
        if (battery != null ) {
            battery.batteryState(Battery.State.CHARGE);
        }
    }


    private void OnTriggerExit(Collider other) {
        Battery battery = other.GetComponentInChildren<Battery>();
        if (battery != null ) {
            battery.batteryState(Battery.State.RUNNING);
        }
    }
}