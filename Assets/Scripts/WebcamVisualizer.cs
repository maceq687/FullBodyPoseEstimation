using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WebcamVisualizer : MonoBehaviour
{
    [SerializeField] WebCamInput webCamInput;
    [SerializeField] RawImage inputImageUI;
    
    
    void Start()
    {
        
    }

    void LateUpdate()
    {
        inputImageUI.texture = webCamInput.inputImageTexture;
    }
}
