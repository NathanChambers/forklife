using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rope : MonoBehaviour {
    public RopeSegment ropeSegmentPrefab;
    public float length;
    public int segments;

    private List<RopeSegment> ropeSegments = new List<RopeSegment>();

    public void Awake() {
        if(segments < 2) {
            segments = 2;
        }

        float interval = length / segments;
        for(int i = 0; i < segments; ++i) {
            Vector3 offset = Vector3.right * (i * interval);
            var ropeSegment = GameObject.Instantiate(ropeSegmentPrefab);
            ropeSegments.Add(ropeSegment);
            ropeSegment.transform.SetParent(transform, false);

            if(i == 0) {
                var firstJoint = ropeSegment.GetComponent<ConfigurableJoint>();
                GameObject.Destroy(firstJoint);
            }

            var rb = ropeSegment.GetComponent<Rigidbody>();
            if(i == 0 || i == segments - 1) {
                rb.constraints = RigidbodyConstraints.FreezeAll;
            }
            rb.MovePosition(transform.position + offset);
            rb.MoveRotation(Quaternion.identity);
            
            if(i > 0) {
                ConfigurableJoint joint = ropeSegment.GetComponent<ConfigurableJoint>();
                joint.connectedBody = ropeSegments[i - 1].GetComponent<Rigidbody>();
            }
        }
    }
}
