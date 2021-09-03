using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactive : MonoBehaviour {
    public System.Action onInteract;

    public void Interact() {
        onInteract?.Invoke();
    }
}


#if UNITY_EDITOR
[UnityEditor.CustomEditor(typeof(Interactive))]
public class InteractiveInspector : UnityEditor.Editor {
    private Interactive self = null;
    public override void OnInspectorGUI() {
        if(self == null) {
            self = target as Interactive;
        }

        base.OnInspectorGUI();

        if(GUILayout.Button("Interact") == true) {
            self.Interact();
        }
    }
}
#endif