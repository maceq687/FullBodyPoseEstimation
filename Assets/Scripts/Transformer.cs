using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transformer : MonoBehaviour
{
    public GameObject obj;
    public Vector3 newPosition = Vector3.zero;
    public Quaternion newRotation;
    private AlignPose alignPose;
    public string referencePointPosition;
    public string referencePointRotation;

    void Start()
    {
        alignPose = obj.GetComponent<AlignPose>();
    }

    void Update()
    {
        if (referencePointPosition.Length != 0)
        {
            newPosition = (Vector3)alignPose.GetType().GetField(referencePointPosition)?.GetValue(alignPose);
            gameObject.transform.localPosition = newPosition;
        }

        if (referencePointRotation.Length != 0)
        {
            newRotation = (Quaternion)alignPose.GetType().GetField(referencePointRotation)?.GetValue(alignPose);
            gameObject.transform.localRotation = newRotation;
        }
    }
}
