using RootSystem = System;
using System.Linq;
using System.Collections.Generic;
namespace Microsoft.Kinect.Face
{
    //
    // Microsoft.Kinect.Face.FaceFrameFeatures
    //
    [RootSystem.Flags]
    public enum FaceFrameFeatures : uint
    {
        None                                     =0,
        BoundingBoxInInfraredSpace               =1,
        PointsInInfraredSpace                    =2,
        BoundingBoxInColorSpace                  =4,
        PointsInColorSpace                       =8,
        RotationOrientation                      =16,
        Happy                                    =32,
        RightEyeClosed                           =64,
        LeftEyeClosed                            =128,
        MouthOpen                                =256,
        MouthMoved                               =512,
        LookingAway                              =1024,
        Glasses                                  =2048,
        FaceEngagement                           =4096,
    }

}
