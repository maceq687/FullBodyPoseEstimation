using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class GetQuestCentroid : MonoBehaviour
{
    private InputDevice hmdDevice;
    private InputDevice leftController;
    private InputDevice rightController;
    private Vector3 headPosition = Vector3.zero;
    private Vector3 leftHandPosition = Vector3.zero;
    private Vector3 rightHandPosition = Vector3.zero;
    public Vector3 centroidPointPosition = Vector3.zero;
    public Quaternion centroidPointRotation;
    
    
    // Start is called before the first frame update
    void Start()
    {
        hmdDevice = InputDevices.GetDeviceAtXRNode(XRNode.CenterEye);
        leftController = InputDevices.GetDeviceAtXRNode(XRNode.LeftHand);
        rightController = InputDevices.GetDeviceAtXRNode(XRNode.RightHand);
    }

    // Update is called once per frame
    void Update()
    {
        if (hmdDevice.isValid)
		{
			hmdDevice.TryGetFeatureValue(CommonUsages.devicePosition, out headPosition);
		}
        if (leftController.isValid)
		{
			leftController.TryGetFeatureValue(CommonUsages.devicePosition, out leftHandPosition);
		}
        if (rightController.isValid)
		{
			rightController.TryGetFeatureValue(CommonUsages.devicePosition, out rightHandPosition);
		}
        
        Vector3[] points = {headPosition, leftHandPosition, rightHandPosition};
        centroidPointPosition = calculateCentroid(points);

        // Calculate vector "hands", the line between hands
        Vector3 hands = leftHandPosition - rightHandPosition;
        // Calculate vector "forehead", the line between the head and head projection on the "hands" vector
        Vector3 forehead = Vector3.Project((headPosition - rightHandPosition), (leftHandPosition - rightHandPosition)) + rightHandPosition - headPosition;
        // Calculate rotation
        centroidPointRotation = Quaternion.LookRotation(hands, forehead);

        gameObject.transform.position = centroidPointPosition;
        gameObject.transform.rotation = centroidPointRotation;
    }

    private Vector3 calculateCentroid(Vector3[] centerPoints){
        Vector3 centroid = Vector3.zero;
        int numPoints = centerPoints.Length;
        foreach (Vector3 point in centerPoints){
            centroid += point;
        }
        
        centroid /= numPoints;
        
        return centroid;
    }
}
