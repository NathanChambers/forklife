using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PalletSpawner : MonoBehaviour {
    public GameObject palletPrefab;
    public List<GameObject> pallets = new List<GameObject>();

    public void UICallback_Spawn() {
        var pallet = GameObject.Instantiate(palletPrefab, transform.position + (Vector3.up * 0.25f), transform.rotation);
        pallets.Add(pallet);
    }
}
