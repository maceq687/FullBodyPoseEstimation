using RootSystem = System;
using System.Linq;
using System.Collections.Generic;
namespace Microsoft.Kinect.Face
{
    //
    // Microsoft.Kinect.Face.RectI
    //
    [RootSystem.Runtime.InteropServices.StructLayout(RootSystem.Runtime.InteropServices.LayoutKind.Sequential)]
    public struct RectI
    {
        public int Left { get; set; }
        public int Top { get; set; }
        public int Right { get; set; }
        public int Bottom { get; set; }

        public override int GetHashCode()
        {
            return Left.GetHashCode() ^ Top.GetHashCode() ^ Right.GetHashCode() ^ Bottom.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (!(obj is RectI))
            {
                return false;
            }

            return this.Equals((RectI)obj);
        }

        public bool Equals(RectI obj)
        {
            return Left.Equals(obj.Left) && Top.Equals(obj.Top) && Right.Equals(obj.Right) && Bottom.Equals(obj.Bottom);
        }

        public static bool operator ==(RectI a, RectI b)
        {
            return a.Equals(b);
        }

        public static bool operator !=(RectI a, RectI b)
        {
            return !(a.Equals(b));
        }
    }

}
