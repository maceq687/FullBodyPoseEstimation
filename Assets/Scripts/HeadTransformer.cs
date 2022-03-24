using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadTransformer : MonoBehaviour
{
    public GameObject obj;
    public Vector3 newReference = Vector3.zero;
    private PoseVisualizer3D poseVisualizer;

    // Start is called before the first frame update
    void Start()
    {
        poseVisualizer = obj.GetComponent<PoseVisualizer3D>();
    }

    // Update is called once per frame
    void Update()
    {
        newReference = poseVisualizer.head;
        gameObject.transform.position = newReference;
    }
}
