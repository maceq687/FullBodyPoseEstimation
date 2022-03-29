using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class VRMap
{
    public Transform vrTarget;
    public Transform rigTarget;
    public Vector3 trackingPositionOffset;
    public Vector3 trackingRotationOffset;

    public void Map()
    {
        rigTarget.position = vrTarget.TransformPoint(trackingPositionOffset);
        rigTarget.rotation = vrTarget.rotation * Quaternion.Euler(trackingRotationOffset);
    }
}

public class VRRig : MonoBehaviour
{
    public float turnSmoothness;
    public VRMap head;
    public VRMap leftHand;
    public VRMap rightHand;
    public VRMap hips;
    public VRMap leftAnkle;
    public VRMap rightAnkle;

    public Transform headConstrain;
    public Vector3 headBodyOffset;
    
    void Start()
    {
        headBodyOffset = transform.position - headConstrain.position;
    }

    void FixedUpdate()
    {
        transform.position = headConstrain.position + headBodyOffset;
        transform.forward = Vector3.Lerp(transform.forward, Vector3.ProjectOnPlane(headConstrain.up, Vector3.up).normalized, Time.deltaTime * turnSmoothness);

        head.Map();
        leftHand.Map();
        rightHand.Map();
        hips.Map();
        leftAnkle.Map();
        rightAnkle.Map();
    }
}
