using RootSystem = System;
using System.Linq;
using System.Collections.Generic;
namespace Microsoft.Kinect.Face
{
    //
    // Microsoft.Kinect.Face.FaceModelBuilderAttributes
    //
    [RootSystem.Flags]
    public enum FaceModelBuilderAttributes : uint
    {
        None                                     =0,
        SkinColor                                =1,
        HairColor                                =2,
    }

}
