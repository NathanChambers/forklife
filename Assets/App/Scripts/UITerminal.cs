using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class UITerminal : MonoBehaviour {
    public Transform parentTransform;
    public RectTransform canvasRect;
    public float unitResolution = 100;

    public void Update() {
        Vector2 res = new Vector2(unitResolution * parentTransform.lossyScale.x, unitResolution * parentTransform.lossyScale.y);
        canvasRect.sizeDelta = res;

        Vector3 localScale = Vector3.one;
        localScale.x = 1.0f / res.x;
        localScale.y = 1.0f / res.y;
        transform.localScale = localScale;
    }
}
