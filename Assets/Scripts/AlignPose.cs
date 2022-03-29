using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using System;

public class AlignPose : MonoBehaviour
{
    private InputDevice hmdDevice;
    private InputDevice leftController;
    private InputDevice rightController;
    private Vector3 hmdPosition = Vector3.zero;
    private Vector3 leftControllerPosition = Vector3.zero;
    private Vector3 rightControllerPosition = Vector3.zero;
    public GameObject obj;
    private PoseVisualizer3D poseVisualizer;
    public Vector3 headPosition = Vector3.zero;
    public Vector3 leftHandPosition = Vector3.zero;
    public Vector3 rightHandPosition = Vector3.zero;
    public Vector3 hipsPosition = Vector3.zero;
    public Vector3 leftAnklePosition = Vector3.zero;
    public Vector3 rightAnklePosition = Vector3.zero;
    public Vector3 vrRigCentroidPointPosition = Vector3.zero;
    public Quaternion vrRigCentroidPointRotation;
    public Vector3 poseCentroidPointPosition = Vector3.zero;
    public Quaternion poseCentroidPointRotation;
    
    
    void Start()
    {
        hmdDevice = InputDevices.GetDeviceAtXRNode(XRNode.CenterEye);
        leftController = InputDevices.GetDeviceAtXRNode(XRNode.LeftHand);
        rightController = InputDevices.GetDeviceAtXRNode(XRNode.RightHand);
        poseVisualizer = obj.GetComponent<PoseVisualizer3D>();
    }

    void Update()
    {
        if (hmdDevice.isValid)
			hmdDevice.TryGetFeatureValue(CommonUsages.devicePosition, out hmdPosition);
        if (leftController.isValid)
			leftController.TryGetFeatureValue(CommonUsages.devicePosition, out leftControllerPosition);
        if (rightController.isValid)
			rightController.TryGetFeatureValue(CommonUsages.devicePosition, out rightControllerPosition);

        headPosition = poseVisualizer.bpPose[0];
        leftHandPosition = poseVisualizer.bpPose[1];
        rightHandPosition = poseVisualizer.bpPose[2];
        hipsPosition = poseVisualizer.bpPose[3];
        leftAnklePosition = poseVisualizer.bpPose[4];
        rightAnklePosition = poseVisualizer.bpPose[5];

        
        Vector3[] vrRigPoints = {hmdPosition, leftControllerPosition, rightControllerPosition};
        vrRigCentroidPointPosition = CalculateCentroidPointPosition(vrRigPoints);
        vrRigCentroidPointRotation = CalculateCentroidPointRotation(vrRigPoints);

        poseCentroidPointPosition = CalculateCentroidPointPosition(poseVisualizer.bpPose);
        poseCentroidPointRotation = CalculateCentroidPointRotation(poseVisualizer.bpPose);

        // WIP
        gameObject.transform.position = new Vector3(0, 1.2f, 0) - headPosition;
        // gameObject.transform.position = vrRigCentroidPointPosition;
        // gameObject.transform.rotation = vrRigCentroidPointRotation;
    }

    private Vector3 CalculateCentroidPointPosition(Vector3[] centerPoints){
        Array.Resize(ref centerPoints, 3);
        Vector3 centroid = Vector3.zero;
        int numPoints = centerPoints.Length;
        foreach (Vector3 point in centerPoints){
            centroid += point;
        }
        centroid /= numPoints;
        return centroid;
    }

    private Quaternion CalculateCentroidPointRotation(Vector3[] upperBodyPoints){
        // Calculate vector "hands", the line between hands
        Vector3 hands = upperBodyPoints[2] - upperBodyPoints[1];
        // Calculate vector "forehead", the line between the head and head's projection on the "hands" vector
        Vector3 forehead = Vector3.Project((upperBodyPoints[0] - upperBodyPoints[2]), (upperBodyPoints[1] - upperBodyPoints[2])) + upperBodyPoints[2] - upperBodyPoints[0];
        // Calculate rotation
        Quaternion centroidPointRotation = Quaternion.identity;
        if (hands != Vector3.zero && forehead != Vector3.zero)
            centroidPointRotation = Quaternion.LookRotation(hands, forehead);
        return centroidPointRotation;
    }
}
