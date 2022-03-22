using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transformer : MonoBehaviour
{
    public GameObject obj;
    public Vector3 newReference = Vector3.zero;
    public PoseVisualizer3D poseVisualiser;

    // Start is called before the first frame update
    void Start()
    {
        poseVisualiser = obj.GetComponent<PoseVisualizer3D>();
    }

    // Update is called once per frame
    void Update()
    {
        newReference = poseVisualiser.nose;
        gameObject.transform.position = newReference;
    }
}
