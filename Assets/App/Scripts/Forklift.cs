using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Forklift : VehicleBase {
    public bool controlable;

    public PilotSeat pilotSeat;
    public IPilot pilot = null;

    public float power = 1;
    public float wheelRate = 45;
    public float steeringLock = 75;
    public float recenterForce = 0.9f;


    public Transform forkTransform;
    public float forkHeightMin;
    public float forkHeightMax;
    public float forkHeight;
    public float forkPower;
    public float forkRampPower = 1.0f;
    public float forkSlowPower = 0.5f;
    public float forkVelocity;
    public float forkDamper;


    public Rigidbody rb;
    public WheelCollider[] driveWheels;
    public WheelCollider[] steerWheels;

    public float steerAngle = 0.0f;

    public Vector3 cachePosition;
    public Quaternion cacheRotation;

    public Transform steeringWheel;


    public void Awake() {
        cachePosition = transform.position;
        cacheRotation = transform.rotation;
    }

    public override void Enter(IPilot pilot) {
        this.pilot = pilot;
        pilot.transform.SetParent(pilotSeat.seat, false);
        pilot.transform.localPosition = Vector3.zero;
        pilot.transform.localRotation = Quaternion.identity;
        pilot.OnEnterVehicle(this);
    }

    public override void Exit() {
        pilot.transform.SetParent(null, false);
        pilot.OnExitVehicle(pilotSeat.exit);
        pilot = null;
    }

    public void Update() {
        if(pilot == null) {
            return;
        }

        steeringWheel.localRotation = Quaternion.Euler(0.0f, 0.0f, -steerWheels[0].steerAngle);

        UpdateMovement();
        UpdateFork();
    }

    public void UpdateMovement() {
        float driveForce = 0.0f;
        float brakeForce = 0.0f;
        if (Input.GetKey(KeyCode.W) == true) {
            driveForce += power;
            if(Vector3.Dot(rb.velocity, transform.forward) < -0.1) {
                brakeForce += power;
            }
        }

        if (Input.GetKey(KeyCode.S) == true) {
            driveForce -= power;
            if(Vector3.Dot(rb.velocity, transform.forward) > 0.1) {
                brakeForce += power;
            }
           
        }
        
        foreach(var wheel in driveWheels) {
            wheel.motorTorque = driveForce;
            wheel.brakeTorque = brakeForce;
        }
        
        foreach(var wheel in steerWheels) {
            wheel.steerAngle = steerAngle;
        }
        
        bool steering = false;
        if (Input.GetKey(KeyCode.A) == true) {
            steerAngle += wheelRate * Time.deltaTime;
            steering = true;
        }

        if (Input.GetKey(KeyCode.D) == true) {
            steerAngle -= wheelRate * Time.deltaTime;
            steering = true;
        }

        steerAngle = Mathf.Clamp(steerAngle, -steeringLock, steeringLock);

        if(steering == false) {
            steerAngle -= (steerAngle * recenterForce * Time.deltaTime) * Mathf.Clamp(rb.velocity.magnitude / 1.0f, 0, 1);
        }
    }

    public void UpdateFork() {
        bool movement = false;
        if (Input.GetKey(KeyCode.R) == true) {
            forkVelocity = (forkPower * forkDamper) * Time.deltaTime;
            movement = true;

        }
        if (Input.GetKey(KeyCode.F) == true) {
            forkVelocity = (-forkPower * forkDamper) * Time.deltaTime;
            movement = true;
        }

        if(movement == true) {
            forkDamper = Mathf.Min(forkDamper + (forkRampPower * Time.deltaTime), 1.0f);
        } else {
            forkDamper = 0.1f;
            forkVelocity -= (forkVelocity * forkSlowPower * Time.deltaTime);
        }

        forkHeight += forkVelocity;
        forkHeight = Mathf.Clamp(forkHeight, forkHeightMin, forkHeightMax);
        forkTransform.localPosition = Vector3.up * forkHeight;
    }
}
