using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceShip : VehicleBase {
    public Rigidbody rb;
    public Transform pilotSeat;
    public float powerScale = 1.0f;
    public float sensitivity = 1.0f;
    public float altHoldStrength = 1.0f;
    private IPilot pilot = null;
    private Vector3 velocity = Vector3.zero;

    private Vector3 pitchRollYaw = Vector3.zero;
    

    public void Update() {
        if(pilot == null) {
            return;
        }

        bool velInput = false;
        bool pitchRollInput = false;
        if(Input.GetKey(KeyCode.W) == true) {
            velocity.z += powerScale * Time.deltaTime;
            velInput = true;
        }

        if(Input.GetKey(KeyCode.S) == true) {
            velocity.z -= powerScale * Time.deltaTime;
            velInput = true;
        }

        if(Input.GetKey(KeyCode.D) == true) {
            velocity.x += powerScale * Time.deltaTime;
            velInput = true;
        }

        if(Input.GetKey(KeyCode.A) == true) {
            velocity.x -= powerScale * Time.deltaTime;
            velInput = true;
        }

        if(Input.GetKey(KeyCode.R) == true) {
            velocity.y += powerScale * Time.deltaTime;
            velInput = true;
        }

        if(Input.GetKey(KeyCode.F) == true) {
            velocity.y -= powerScale * Time.deltaTime;
            velInput = true;
        }

        if(Input.GetKey(KeyCode.Q) == true) {
            pitchRollYaw.y -= sensitivity * Time.deltaTime;
            pitchRollInput = true;
        }

        if(Input.GetKey(KeyCode.Return) == true) {
            pitchRollYaw.y += sensitivity * Time.deltaTime;
            pitchRollInput = true;
        }

        float mouseX = Input.GetAxis("Mouse X");

        float mouseY = Input.GetAxis("Mouse Y");

        pitchRollYaw.x -= mouseY * sensitivity;
        pitchRollYaw.z -= mouseX * sensitivity;

        // var altHoldMag = Vector3.Dot(rb.velocity, Vector3.up);
        // if(velInput == false && altHoldMag < 0) {
        //     velocity.y += -rb.velocity.y * altHoldMag * altHoldStrength * Time.deltaTime;
        // }

        if(velInput == false) {
            velocity -= velocity * 5 * Time.deltaTime;
        }

        rb.AddRelativeForce(velocity, ForceMode.Acceleration);
        rb.MoveRotation(Quaternion.Euler(pitchRollYaw));
    }

    public override void Enter(IPilot pilot) {
        this.pilot = pilot;
        pilot.transform.SetParent(pilotSeat, false);
        pilot.transform.localPosition = Vector3.zero;
        pilot.transform.localRotation = Quaternion.identity;
    }

    public override void Exit() {
        pilot = null;
    }
}
