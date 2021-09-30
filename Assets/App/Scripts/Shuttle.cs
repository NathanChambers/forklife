using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shuttle : VehicleBase {
    public PilotSeat pilotSeat;
    public IPilot pilot = null;
    public Rigidbody rb;

    public float rollAuthority;
    public float pitchAuthority;
    public float yawAuthority;

    public float xPosAuthority;
    public float xNegAuthority;
    
    public float yPosAuthority;
    public float yNegAuthority;

    public float zPosAuthority;
    public float zNegAuthority;


    public bool dampingChannelRoll;
    public bool dampingChannelPitch;
    public bool dampingChannelYaw;
    public float pitchYawRollDamping = 0.8f;


    private Vector3 thrusters = Vector3.zero;
    private Vector3 pitchYawRoll = Vector3.zero;
    private Vector3 velocity = Vector3.zero;
    private Vector3 collisionForce = Vector3.zero;

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

        UpdateMovement();
    }

    public void FixedUpdate() {
        rb.MovePosition(rb.position + velocity + collisionForce);
        rb.MoveRotation(rb.rotation * Quaternion.Euler(pitchYawRoll * Time.deltaTime * Time.deltaTime));
        collisionForce = Vector3.zero;
    }

    private void UpdateMovement() {
        thrusters = Vector3.zero;

        if(Input.GetKey(KeyCode.W) == true) {
            thrusters.z += zPosAuthority;
        } else if(Input.GetKey(KeyCode.S) == true) {
            thrusters.z -= zNegAuthority;
        }

        if(Input.GetKey(KeyCode.D) == true) {
            thrusters.x += xPosAuthority;
        } else if(Input.GetKey(KeyCode.A) == true) {
            thrusters.x -= xNegAuthority;
        }

        if(Input.GetKey(KeyCode.Space) == true) {
            thrusters.y += yNegAuthority;
        } else if(Input.GetKey(KeyCode.LeftControl) == true) {
            thrusters.y -= yNegAuthority;
        }

        if(Input.GetKey(KeyCode.Q) == true) {
            pitchYawRoll.z += rollAuthority;
        } else if(Input.GetKey(KeyCode.E) == true) {
            pitchYawRoll.z -= rollAuthority;
        }

        if(Input.GetKey(KeyCode.UpArrow) == true) {
            pitchYawRoll.x += pitchAuthority;
        } else if(Input.GetKey(KeyCode.DownArrow) == true) {
            pitchYawRoll.x -= pitchAuthority;
        }

        if(Input.GetKey(KeyCode.RightArrow) == true) {
            pitchYawRoll.y += yawAuthority;
        } else if(Input.GetKey(KeyCode.LeftArrow) == true) {
            pitchYawRoll.y -= yawAuthority;
        }

        if(dampingChannelRoll == true) {
            pitchYawRoll.z -= pitchYawRoll.z * pitchYawRollDamping * Time.deltaTime;
        }

        if(dampingChannelPitch == true) {
            pitchYawRoll.x -= pitchYawRoll.x * pitchYawRollDamping * Time.deltaTime;
        }

        if(dampingChannelYaw == true) {
            pitchYawRoll.y -= pitchYawRoll.y * pitchYawRollDamping * Time.deltaTime;
        }
        
        if(thrusters == Vector3.zero) {
            return;
        }

        velocity += transform.rotation * thrusters * Time.deltaTime * Time.deltaTime;
    }

    public void OnCollisionEnter(Collision collision) {
        Debug.Log($"RV: {collision.relativeVelocity}");
        Debug.Log($"IMP: {collision.impulse}");
        for(int i = 0; i < collision.contactCount; ++i) {
            var c = collision.contacts[i];
            Debug.Log($"CONT {i} NORM: {c.normal}");
            Debug.Log($"CONT {i} SEP: {c.separation}");

            velocity.x -= velocity.x * Mathf.Abs(c.normal.x);
            velocity.y -= velocity.y * Mathf.Abs(c.normal.y);
            velocity.z -= velocity.z * Mathf.Abs(c.normal.z);
        }
    }
}
