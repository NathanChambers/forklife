using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour,  IPilot {
    public CharacterController cc;
    public float sensitivity;
    public float walkSpeed;
    public Transform cameraHolder;

    ////////////////////////////////////////////////////////////////////////////////

    private VehicleBase activeVehicle;
    private bool interactMode = false;

    ////////////////////////////////////////////////////////////////////////////////

    public bool InteractMode => interactMode;

    ////////////////////////////////////////////////////////////////////////////////

    public void Awake() {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = true;
    }
    

    public void Update() {
        if(Input.GetKeyDown(KeyCode.Tab) == true) {
            SetInteraction(!interactMode);
        }

        if(interactMode == true) {
            return;
        }

        UpdateEnterVehicle();
        UpdateMovement();
    }

    private void UpdateEnterVehicle() {
        if(Input.GetKeyDown(KeyCode.E) == false) {
            return;
        }

        if(activeVehicle != null) {
            activeVehicle.Exit();
            return;
        }

        RaycastHit hit;
        var pickRay = Camera.main.ViewportPointToRay(Vector3.one * 0.5f);
        if(Physics.Raycast(pickRay.origin, pickRay.direction, out hit, 3.0f) == false) {
            return;
        }


        PilotSeat seat = hit.collider.GetComponent<PilotSeat>();
        if(seat == null) {
            return;
        }

        seat.Enter(this);
    }


    private void UpdateMovement() {
        if(interactMode == true) {
            return;
        }

        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");
        Vector3 movement = Vector3.zero;
        Vector3 cameraEuler = cameraHolder.localRotation.eulerAngles;
        cameraEuler.x -= mouseY * sensitivity;
        if(activeVehicle != null && activeVehicle.AllowFreelook == true) {
            cameraEuler.y += mouseX * sensitivity;
        }
        cameraHolder.localRotation = Quaternion.Euler(cameraEuler);

        if(activeVehicle == null) {
            transform.rotation *= Quaternion.Euler(0, mouseX * sensitivity, 0);

            if (Input.GetKey(KeyCode.W) == true) {
                movement.z += walkSpeed * Time.deltaTime;
            }

            if (Input.GetKey(KeyCode.S) == true) {
                movement.z -= walkSpeed * Time.deltaTime;
            }

            if (Input.GetKey(KeyCode.D) == true) {
                movement.x += walkSpeed * Time.deltaTime;
            }

            if (Input.GetKey(KeyCode.A) == true) {
                movement.x -= walkSpeed * Time.deltaTime;
            }

            movement.y -= 9.6f * Time.deltaTime;

            cc.Move(transform.rotation * (movement.normalized * walkSpeed * Time.deltaTime));
        }
    }

    public void SetInteraction(bool active) {
        interactMode = active;

        if(interactMode == true) {
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
        } else {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    public void OnEnterVehicle(VehicleBase vehicle) {
        activeVehicle = vehicle;
        cc.detectCollisions = false;
    }

    public void OnExitVehicle(Transform target) {
        activeVehicle = null;
        cc.enabled = false;
        cc.transform.position = target.position;
        cc.enabled = true;
        cc.detectCollisions = true;

        cameraHolder.rotation = Quaternion.identity;
    }
}

