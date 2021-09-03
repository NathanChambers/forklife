using UnityEngine;

public abstract class VehicleBase : MonoBehaviour {
    public abstract void Enter(IPilot pilot);
    public abstract void Exit();

    public virtual bool AllowFreelook => true;
}