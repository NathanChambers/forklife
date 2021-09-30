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
    public PilotSeat startVehicle;
    public AsteroidField asteroidField;

    ////////////////////////////////////////////////////////////////////////////////

    private VehicleBase activeVehicle;
    private bool interactMode = false;
    private Vector3 velocity = Vector3.zero;

    ////////////////////////////////////////////////////////////////////////////////

    public bool InteractMode => interactMode;

    ////////////////////////////////////////////////////////////////////////////////

    public void Awake() {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = true;

        if(startVehicle != null) {
            startVehicle.Enter(this);
        }
    }
    

    public void Update() {
        if(Input.GetKeyDown(KeyCode.Tab) == true) {
            SetInteraction(!interactMode);
        }

        UpdateInteraction();
        UpdateMovement();
    }

    public void FixedUpdate() {
        if (asteroidField != null) {
            if ((transform.position + velocity).magnitude > (asteroidField.fieldSize / 2) * asteroidField.cellSize) {
            velocity = Vector3.zero;
            return;
            }
        }

        if(activeVehicle == null) {
            cc.Move(velocity);
        }
    }

    private void UpdateInteraction() {
        if(interactMode == true) {
            return;
        }

        if(Input.GetKeyDown(KeyCode.Return) == false) {
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
        if(seat != null) {
            seat.Enter(this);
            return;
        }

        AnimInteraction interaction = hit.collider.GetComponent<AnimInteraction>();
        if(interaction != null) {
            interaction.Invoke();
            return;
        }

        
    }


    private void UpdateMovement() {
        if(interactMode == true) {
            return;
        }

        Vector3 impulse = Vector3.zero;

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
                impulse += transform.forward;
            }

            if (Input.GetKey(KeyCode.S) == true) {
                impulse -= transform.forward;
            }

            if (Input.GetKey(KeyCode.D) == true) {
                impulse += transform.right;
            }

            if (Input.GetKey(KeyCode.A) == true) {
                impulse -= transform.right;
            }

            if (Input.GetKey(KeyCode.Space) == true) {
                impulse += transform.up;
            }

            if (Input.GetKey(KeyCode.LeftControl) == true) {
                impulse -= transform.up;
            }

            velocity += (impulse.normalized * walkSpeed * Time.deltaTime) * Time.deltaTime;
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
        velocity = Vector3.zero;
    }

    public void OnExitVehicle(Transform target) {
        velocity = Vector3.zero;
        activeVehicle = null;
        cc.enabled = false;
        cc.transform.position = target.position;
        cc.enabled = true;
        cc.detectCollisions = true;

        cameraHolder.rotation = Quaternion.identity;
    }

    public void OnCollisionEnter(Collision collision) {
        if(collision == null) {
            return;
        }

        Debug.Log($"Enter: {collision.transform.name}");
    }

    public void OnCollisionStay(Collision collision) {
        if(collision == null) {
            return;
        }

        Debug.Log($"Stay: {collision.transform.name}");
    }
}

