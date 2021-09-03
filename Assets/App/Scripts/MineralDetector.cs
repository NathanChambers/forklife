using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineralDetector : MonoBehaviour {
    private List<Mineral> minerals = new List<Mineral>();
    public IList<Mineral> Minerals => minerals;

    private void OnTriggerEnter(Collider other) {
        var detectedMinerals = other.GetComponentsInChildren<Mineral>();
        foreach(var mineral in detectedMinerals) {
            minerals.Add(mineral);
        }
    }

    private void OnTriggerExit(Collider other) {
        var detectedMinerals = other.GetComponentsInChildren<Mineral>();
        foreach(var mineral in detectedMinerals) {
            minerals.Remove(mineral);
        }
    }

    public void Remove(Mineral mineral) {
        minerals.Remove(mineral);
    }
}
