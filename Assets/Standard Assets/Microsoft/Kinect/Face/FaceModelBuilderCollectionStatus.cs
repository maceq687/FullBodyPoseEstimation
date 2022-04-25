using RootSystem = System;
using System.Linq;
using System.Collections.Generic;
namespace Microsoft.Kinect.Face
{
    //
    // Microsoft.Kinect.Face.FaceModelBuilderCollectionStatus
    //
    [RootSystem.Flags]
    public enum FaceModelBuilderCollectionStatus : uint
    {
        Complete                                 =0,
        MoreFramesNeeded                         =1,
        FrontViewFramesNeeded                    =2,
        LeftViewsNeeded                          =4,
        RightViewsNeeded                         =8,
        TiltedUpViewsNeeded                      =16,
    }

}
