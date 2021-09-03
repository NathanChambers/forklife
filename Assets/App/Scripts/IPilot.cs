using UnityEngine;
public interface IPilot {
    GameObject gameObject {get;}
    Transform transform {get;}
    bool InteractMode {get;}
    void OnEnterVehicle(VehicleBase vehicle);
    void OnExitVehicle(Transform target);
}