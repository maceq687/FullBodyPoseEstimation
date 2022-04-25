using RootSystem = System;
using System.Linq;
using System.Collections.Generic;
namespace Microsoft.Kinect.Face
{
    //
    // Microsoft.Kinect.Face.HighDefinitionFaceFrameArrivedEventArgs
    //
    public sealed partial class HighDefinitionFaceFrameArrivedEventArgs : RootSystem.EventArgs, Helper.INativeWrapper

    {
        internal RootSystem.IntPtr _pNative;
        RootSystem.IntPtr Helper.INativeWrapper.nativePtr { get { return _pNative; } }

        // Constructors and Finalizers
        internal HighDefinitionFaceFrameArrivedEventArgs(RootSystem.IntPtr pNative)
        {
            _pNative = pNative;
            Microsoft_Kinect_Face_HighDefinitionFaceFrameArrivedEventArgs_AddRefObject(ref _pNative);
        }

        ~HighDefinitionFaceFrameArrivedEventArgs()
        {
            Dispose(false);
        }

        [RootSystem.Runtime.InteropServices.DllImport("KinectFaceUnityAddin", CallingConvention=RootSystem.Runtime.InteropServices.CallingConvention.Cdecl, SetLastError=true)]
        private static extern void Microsoft_Kinect_Face_HighDefinitionFaceFrameArrivedEventArgs_ReleaseObject(ref RootSystem.IntPtr pNative);
        [RootSystem.Runtime.InteropServices.DllImport("KinectFaceUnityAddin", CallingConvention=RootSystem.Runtime.InteropServices.CallingConvention.Cdecl, SetLastError=true)]
        private static extern void Microsoft_Kinect_Face_HighDefinitionFaceFrameArrivedEventArgs_AddRefObject(ref RootSystem.IntPtr pNative);
        private void Dispose(bool disposing)
        {
            if (_pNative == RootSystem.IntPtr.Zero)
            {
                return;
            }

            __EventCleanup();

            Helper.NativeObjectCache.RemoveObject<HighDefinitionFaceFrameArrivedEventArgs>(_pNative);
                Microsoft_Kinect_Face_HighDefinitionFaceFrameArrivedEventArgs_ReleaseObject(ref _pNative);

            _pNative = RootSystem.IntPtr.Zero;
        }


        // Public Properties
        [RootSystem.Runtime.InteropServices.DllImport("KinectFaceUnityAddin", CallingConvention=RootSystem.Runtime.InteropServices.CallingConvention.Cdecl, SetLastError=true)]
        private static extern RootSystem.IntPtr Microsoft_Kinect_Face_HighDefinitionFaceFrameArrivedEventArgs_get_FrameReference(RootSystem.IntPtr pNative);
        public  Microsoft.Kinect.Face.HighDefinitionFaceFrameReference FrameReference
        {
            get
            {
                if (_pNative == RootSystem.IntPtr.Zero)
                {
                    throw new RootSystem.ObjectDisposedException("HighDefinitionFaceFrameArrivedEventArgs");
                }

                RootSystem.IntPtr objectPointer = Microsoft_Kinect_Face_HighDefinitionFaceFrameArrivedEventArgs_get_FrameReference(_pNative);
                Helper.ExceptionHelper.CheckLastError();
                if (objectPointer == RootSystem.IntPtr.Zero)
                {
                    return null;
                }

                return Helper.NativeObjectCache.CreateOrGetObject<Microsoft.Kinect.Face.HighDefinitionFaceFrameReference>(objectPointer, n => new Microsoft.Kinect.Face.HighDefinitionFaceFrameReference(n));
            }
        }

        private void __EventCleanup()
        {
        }
    }

}
