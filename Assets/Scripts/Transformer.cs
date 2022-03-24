using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transformer : MonoBehaviour
{
    public GameObject obj;
    public Vector3 newReference = Vector3.zero;
    private PoseVisualizer3D poseVisualizer;
    public string point;

    void Start()
    {
        poseVisualizer = obj.GetComponent<PoseVisualizer3D>();
    }

    void Update()
    {
        newReference = (Vector3)poseVisualizer.GetType().GetField(point)?.GetValue(poseVisualizer);
        gameObject.transform.position = newReference;
    }
}
