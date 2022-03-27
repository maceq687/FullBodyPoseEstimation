using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transformer : MonoBehaviour
{
    public GameObject obj;
    public Vector3 newPosition = Vector3.zero;
    public Quaternion newRotation;
    private PoseVisualizer3D poseVisualizer;
    public string referencePointPosition;
    public string referencePointRotation;

    void Start()
    {
        poseVisualizer = obj.GetComponent<PoseVisualizer3D>();
    }

    void Update()
    {
        if (referencePointPosition.Length != 0)
        {
            newPosition = (Vector3)poseVisualizer.GetType().GetField(referencePointPosition)?.GetValue(poseVisualizer);
            gameObject.transform.position = newPosition;
        }

        if (referencePointRotation.Length != 0)
        {
            newRotation = (Quaternion)poseVisualizer.GetType().GetField(referencePointRotation)?.GetValue(poseVisualizer);
            gameObject.transform.rotation = newRotation;
        }
    }
}
