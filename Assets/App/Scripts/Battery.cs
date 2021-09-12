using UnityEngine;

public class Battery : MonoBehaviour {

    public Forklift forklift;

    public void LinkForklift(Forklift forklift) {
        this.forklift = forklift;
    }

    public float MaxPower = 100.0f;
    public float InitialPower = 100.0f;
    public float BaseDrainRate = 0.5f;
    public float BaseChargeRate = 2.0f;

    private float power = 0f;

    public enum State {
        CHARGE,
        IDLE,
        RUNNING,
    }

    private State state = State.IDLE;

    public float Power{
        get{
            return power;
        }
    }

    public void Awake(){
        power = InitialPower;
    }

    public void Update(){
        if(forklift.pilot != null) {
            state = State.RUNNING;
        }

        switch (state){
            case State.CHARGE:
                power = power +(BaseChargeRate * Time.deltaTime);
                break;

            case State.RUNNING:
                power = power -(BaseDrainRate * Time.deltaTime);
                break;

            default:
                break;
        }
        power = Mathf.Clamp(power,0.0f,MaxPower);
    }

    public void batteryState(State newstate){
        state = newstate;
    }
}