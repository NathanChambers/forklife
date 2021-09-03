using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class Door : MonoBehaviour {
    public Vector3 PositionClosed;
    public Vector3 PositionOpen;
    public bool startOpen = false;
    public float speed = 1.0f;

    public Transform door;
    public List<Interactive> interactions;

    private Vector3 targetPosition = Vector3.zero;
    private bool isOpen = false;

    public bool IsOpen {
        get {
            return isOpen;
        }
    }

    public void Awake() {
        foreach(var interaction in interactions) {
            interaction.onInteract += Toggle;
        }

        isOpen = startOpen;
        targetPosition = isOpen ? PositionOpen : PositionClosed;
        door.transform.localPosition = targetPosition;
    }

    public void Open() {
        targetPosition = PositionOpen;
    }

    public void Close() {
        targetPosition = PositionClosed;
    }

    public void Toggle() {
        isOpen = !isOpen;
        targetPosition = isOpen ? PositionOpen : PositionClosed;
    }

    public void Update() {
        Vector3 dir = targetPosition - door.transform.localPosition;
        float dist = dir.magnitude;

        if(dist <= speed * Time.deltaTime) {
            return;
        }

        if(dist < 0.1f) {
            door.transform.localPosition = targetPosition;
        } else {
            door.transform.localPosition += dir.normalized * speed * Time.deltaTime;
        }
    }
}


#if UNITY_EDITOR
[UnityEditor.CustomEditor(typeof(Door))]
public class DoorInspector : UnityEditor.Editor {
    private Door self = null;
    public override void OnInspectorGUI() {
        if(self == null) {
            self = target as Door;
        }

        base.OnInspectorGUI();

        if(GUILayout.Button("Open") == true) {
            self.Open();
        }

        if(GUILayout.Button("Close") == true) {
            self.Close();
        }
    }
}
#endif