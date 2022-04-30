using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class CameraManager : MonoBehaviour
{
    public CameraManager CamManager;
    public Camera mainCamera;
    private Camera supportCamera;
    private bool vrRunning;
    
    void Start()
    {
        supportCamera = CamManager.GetComponent<Camera>();
        vrRunning = isVrRunning();
        if (vrRunning)
            {
                supportCamera.enabled = false;
                mainCamera.enabled = true;
            }
            else
            {
                supportCamera.enabled = true;
                mainCamera.enabled = false;
            }
    }

    private static bool isVrRunning()
    {
        var xrDisplaySubsystems = new List<XRDisplaySubsystem>();
        SubsystemManager.GetInstances<XRDisplaySubsystem>(xrDisplaySubsystems);
        foreach (var xrDisplay in xrDisplaySubsystems)
        {
            if (xrDisplay.running)
            {
                return true;
            }
        }
        return false;
    }
}
