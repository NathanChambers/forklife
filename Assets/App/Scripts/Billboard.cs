using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour {

    public float size;
    public SpriteRenderer spriteRenderer;

    private Camera mainCamera = null;
    public void Awake() {
        mainCamera = Camera.main;
    }

    public void Update() {
        Vector3 diff = transform.position - mainCamera.transform.position;
        float scale = (diff.magnitude / size);

        transform.rotation = Quaternion.LookRotation(diff, Vector3.up);
        transform.localScale = Vector3.one * scale;
    }

    public void SetColor(Color color) {
        spriteRenderer.color = color;
    }

}
