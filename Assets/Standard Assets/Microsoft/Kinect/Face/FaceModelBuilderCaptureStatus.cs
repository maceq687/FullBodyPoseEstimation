using RootSystem = System;
using System.Linq;
using System.Collections.Generic;
namespace Microsoft.Kinect.Face
{
    //
    // Microsoft.Kinect.Face.FaceModelBuilderCaptureStatus
    //
    public enum FaceModelBuilderCaptureStatus : int
    {
        GoodFrameCapture                         =0,
        OtherViewsNeeded                         =1,
        LostFaceTrack                            =2,
        FaceTooFar                               =3,
        FaceTooNear                              =4,
        MovingTooFast                            =5,
        SystemError                              =6,
    }

}
