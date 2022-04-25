using RootSystem = System;
using System.Linq;
using System.Collections.Generic;
namespace Microsoft.Kinect.Face
{
    //
    // Microsoft.Kinect.Face.HighDefinitionFaceFrameReference
    //
    public sealed partial class HighDefinitionFaceFrameReference : Helper.INativeWrapper

    {
        internal RootSystem.IntPtr _pNative;
        RootSystem.IntPtr Helper.INativeWrapper.nativePtr { get { return _pNative; } }

        // Constructors and Finalizers
        internal HighDefinitionFaceFrameReference(RootSystem.IntPtr pNative)
        {
            _pNative = pNative;
            Microsoft_Kinect_Face_HighDefinitionFaceFrameReference_AddRefObject(ref _pNative);
        }

        ~HighDefinitionFaceFrameReference()
        {
            Dispose(false);
        }

        [RootSystem.Runtime.InteropServices.DllImport("KinectFaceUnityAddin", CallingConvention=RootSystem.Runtime.InteropServices.CallingConvention.Cdecl, SetLastError=true)]
        private static extern void Microsoft_Kinect_Face_HighDefinitionFaceFrameReference_ReleaseObject(ref RootSystem.IntPtr pNative);
        [RootSystem.Runtime.InteropServices.DllImport("KinectFaceUnityAddin", CallingConvention=RootSystem.Runtime.InteropServices.CallingConvention.Cdecl, SetLastError=true)]
        private static extern void Microsoft_Kinect_Face_HighDefinitionFaceFrameReference_AddRefObject(ref RootSystem.IntPtr pNative);
        private void Dispose(bool disposing)
        {
            if (_pNative == RootSystem.IntPtr.Zero)
            {
                return;
            }

            __EventCleanup();

            Helper.NativeObjectCache.RemoveObject<HighDefinitionFaceFrameReference>(_pNative);
                Microsoft_Kinect_Face_HighDefinitionFaceFrameReference_ReleaseObject(ref _pNative);

            _pNative = RootSystem.IntPtr.Zero;
        }


        // Public Properties
        [RootSystem.Runtime.InteropServices.DllImport("KinectFaceUnityAddin", CallingConvention=RootSystem.Runtime.InteropServices.CallingConvention.Cdecl, SetLastError=true)]
        private static extern long Microsoft_Kinect_Face_HighDefinitionFaceFrameReference_get_RelativeTime(RootSystem.IntPtr pNative);
        public  RootSystem.TimeSpan RelativeTime
        {
            get
            {
                if (_pNative == RootSystem.IntPtr.Zero)
                {
                    throw new RootSystem.ObjectDisposedException("HighDefinitionFaceFrameReference");
                }

                return RootSystem.TimeSpan.FromMilliseconds(Microsoft_Kinect_Face_HighDefinitionFaceFrameReference_get_RelativeTime(_pNative));
            }
        }


        // Public Methods
        [RootSystem.Runtime.InteropServices.DllImport("KinectFaceUnityAddin", CallingConvention=RootSystem.Runtime.InteropServices.CallingConvention.Cdecl, SetLastError=true)]
        private static extern RootSystem.IntPtr Microsoft_Kinect_Face_HighDefinitionFaceFrameReference_AcquireFrame(RootSystem.IntPtr pNative);
        public Microsoft.Kinect.Face.HighDefinitionFaceFrame AcquireFrame()
        {
            if (_pNative == RootSystem.IntPtr.Zero)
            {
                throw new RootSystem.ObjectDisposedException("HighDefinitionFaceFrameReference");
            }

            RootSystem.IntPtr objectPointer = Microsoft_Kinect_Face_HighDefinitionFaceFrameReference_AcquireFrame(_pNative);
            Helper.ExceptionHelper.CheckLastError();
            if (objectPointer == RootSystem.IntPtr.Zero)
            {
                return null;
            }

            return Helper.NativeObjectCache.CreateOrGetObject<Microsoft.Kinect.Face.HighDefinitionFaceFrame>(objectPointer, n => new Microsoft.Kinect.Face.HighDefinitionFaceFrame(n));
        }

        private void __EventCleanup()
        {
        }
    }

}
