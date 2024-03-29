﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class PreviewManager : MonoBehaviour
{
    public PreviewManager MirrorPreview;
    public GameObject WebCamPreview;
    private Canvas VirtualMirrorCanvas;
    private Camera VirtualMirrorCamera;
    private InputDevice rightController;
    private bool lastPrimaryButtonValue = false;
    private bool WebCam = true;
    private bool VirtualCam = false;

    
    
    void Start()
    {
        VirtualMirrorCanvas = MirrorPreview.GetComponentInChildren<Canvas>();
        VirtualMirrorCamera = MirrorPreview.GetComponentInChildren<Camera>();
        rightController = InputDevices.GetDeviceAtXRNode(XRNode.RightHand);
        VirtualMirrorCanvas.gameObject.SetActive(VirtualCam);
        VirtualMirrorCamera.gameObject.SetActive(VirtualCam);
    }

    void Update()
    {
        bool primaryButtonValue = false;
        if (UnityEngine.InputSystem.Keyboard.current.gKey.wasPressedThisFrame || (rightController.TryGetFeatureValue(CommonUsages.primaryButton, out primaryButtonValue) && primaryButtonValue != lastPrimaryButtonValue && primaryButtonValue))
        {
            WebCam = !WebCam;
            VirtualCam = !VirtualCam;
            VirtualMirrorCanvas.gameObject.SetActive(VirtualCam);
            VirtualMirrorCamera.gameObject.SetActive(VirtualCam);
            WebCamPreview.SetActive(WebCam);
        }
        lastPrimaryButtonValue = primaryButtonValue;
    }        
}
