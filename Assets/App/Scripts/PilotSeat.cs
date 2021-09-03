using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PilotSeat : MonoBehaviour {
    public VehicleBase vehicle;
    public Transform seat;
    public Transform exit;


    public void Enter(IPilot pilot) {
        vehicle.Enter(pilot);
        pilot.OnEnterVehicle(vehicle);
    }
}
