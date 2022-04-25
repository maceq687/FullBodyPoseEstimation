using RootSystem = System;
using System.Linq;
using System.Collections.Generic;
namespace Microsoft.Kinect.Face
{
    //
    // Microsoft.Kinect.Face.HighDefinitionFaceFrame
    //
    public sealed partial class HighDefinitionFaceFrame : RootSystem.IDisposable, Helper.INativeWrapper

    {
        internal RootSystem.IntPtr _pNative;
        RootSystem.IntPtr Helper.INativeWrapper.nativePtr { get { return _pNative; } }

        // Constructors and Finalizers
        internal HighDefinitionFaceFrame(RootSystem.IntPtr pNative)
        {
            _pNative = pNative;
            Microsoft_Kinect_Face_HighDefinitionFaceFrame_AddRefObject(ref _pNative);
        }

        ~HighDefinitionFaceFrame()
        {
            Dispose(false);
        }

        [RootSystem.Runtime.InteropServices.DllImport("KinectFaceUnityAddin", CallingConvention=RootSystem.Runtime.InteropServices.CallingConvention.Cdecl, SetLastError=true)]
        private static extern void Microsoft_Kinect_Face_HighDefinitionFaceFrame_ReleaseObject(ref RootSystem.IntPtr pNative);
        [RootSystem.Runtime.InteropServices.DllImport("KinectFaceUnityAddin", CallingConvention=RootSystem.Runtime.InteropServices.CallingConvention.Cdecl, SetLastError=true)]
        private static extern void Microsoft_Kinect_Face_HighDefinitionFaceFrame_AddRefObject(ref RootSystem.IntPtr pNative);
        private void Dispose(bool disposing)
        {
            if (_pNative == RootSystem.IntPtr.Zero)
            {
                return;
            }

            __EventCleanup();

            Helper.NativeObjectCache.RemoveObject<HighDefinitionFaceFrame>(_pNative);

            if (disposing)
            {
                Microsoft_Kinect_Face_HighDefinitionFaceFrame_Dispose(_pNative);
            }
                Microsoft_Kinect_Face_HighDefinitionFaceFrame_ReleaseObject(ref _pNative);

            _pNative = RootSystem.IntPtr.Zero;
        }


        // Public Properties
        [RootSystem.Runtime.InteropServices.DllImport("KinectFaceUnityAddin", CallingConvention=RootSystem.Runtime.InteropServices.CallingConvention.Cdecl, SetLastError=true)]
        private static extern RootSystem.IntPtr Microsoft_Kinect_Face_HighDefinitionFaceFrame_get_BodyFrameReference(RootSystem.IntPtr pNative);
        public  Windows.Kinect.BodyFrameReference BodyFrameReference
        {
            get
            {
                if (_pNative == RootSystem.IntPtr.Zero)
                {
                    throw new RootSystem.ObjectDisposedException("HighDefinitionFaceFrame");
                }

                RootSystem.IntPtr objectPointer = Microsoft_Kinect_Face_HighDefinitionFaceFrame_get_BodyFrameReference(_pNative);
                Helper.ExceptionHelper.CheckLastError();
                if (objectPointer == RootSystem.IntPtr.Zero)
                {
                    return null;
                }

                return Helper.NativeObjectCache.CreateOrGetObject<Windows.Kinect.BodyFrameReference>(objectPointer, n => new Windows.Kinect.BodyFrameReference(n));
            }
        }

        [RootSystem.Runtime.InteropServices.DllImport("KinectFaceUnityAddin", CallingConvention=RootSystem.Runtime.InteropServices.CallingConvention.Cdecl, SetLastError=true)]
        private static extern RootSystem.IntPtr Microsoft_Kinect_Face_HighDefinitionFaceFrame_get_ColorFrameReference(RootSystem.IntPtr pNative);
        public  Windows.Kinect.ColorFrameReference ColorFrameReference
        {
            get
            {
                if (_pNative == RootSystem.IntPtr.Zero)
                {
                    throw new RootSystem.ObjectDisposedException("HighDefinitionFaceFrame");
                }

                RootSystem.IntPtr objectPointer = Microsoft_Kinect_Face_HighDefinitionFaceFrame_get_ColorFrameReference(_pNative);
                Helper.ExceptionHelper.CheckLastError();
                if (objectPointer == RootSystem.IntPtr.Zero)
                {
                    return null;
                }

                return Helper.NativeObjectCache.CreateOrGetObject<Windows.Kinect.ColorFrameReference>(objectPointer, n => new Windows.Kinect.ColorFrameReference(n));
            }
        }

        [RootSystem.Runtime.InteropServices.DllImport("KinectFaceUnityAddin", CallingConvention=RootSystem.Runtime.InteropServices.CallingConvention.Cdecl, SetLastError=true)]
        private static extern RootSystem.IntPtr Microsoft_Kinect_Face_HighDefinitionFaceFrame_get_DepthFrameReference(RootSystem.IntPtr pNative);
        public  Windows.Kinect.DepthFrameReference DepthFrameReference
        {
            get
            {
                if (_pNative == RootSystem.IntPtr.Zero)
                {
                    throw new RootSystem.ObjectDisposedException("HighDefinitionFaceFrame");
                }

                RootSystem.IntPtr objectPointer = Microsoft_Kinect_Face_HighDefinitionFaceFrame_get_DepthFrameReference(_pNative);
                Helper.ExceptionHelper.CheckLastError();
                if (objectPointer == RootSystem.IntPtr.Zero)
                {
                    return null;
                }

                return Helper.NativeObjectCache.CreateOrGetObject<Windows.Kinect.DepthFrameReference>(objectPointer, n => new Windows.Kinect.DepthFrameReference(n));
            }
        }

        [RootSystem.Runtime.InteropServices.DllImport("KinectFaceUnityAddin", CallingConvention=RootSystem.Runtime.InteropServices.CallingConvention.Cdecl, SetLastError=true)]
        private static extern Microsoft.Kinect.Face.FaceAlignmentQuality Microsoft_Kinect_Face_HighDefinitionFaceFrame_get_FaceAlignmentQuality(RootSystem.IntPtr pNative);
        public  Microsoft.Kinect.Face.FaceAlignmentQuality FaceAlignmentQuality
        {
            get
            {
                if (_pNative == RootSystem.IntPtr.Zero)
                {
                    throw new RootSystem.ObjectDisposedException("HighDefinitionFaceFrame");
                }

                return Microsoft_Kinect_Face_HighDefinitionFaceFrame_get_FaceAlignmentQuality(_pNative);
            }
        }

        [RootSystem.Runtime.InteropServices.DllImport("KinectFaceUnityAddin", CallingConvention=RootSystem.Runtime.InteropServices.CallingConvention.Cdecl, SetLastError=true)]
        private static extern RootSystem.IntPtr Microsoft_Kinect_Face_HighDefinitionFaceFrame_get_FaceModel(RootSystem.IntPtr pNative);
        public  Microsoft.Kinect.Face.FaceModel FaceModel
        {
            get
            {
                if (_pNative == RootSystem.IntPtr.Zero)
                {
                    throw new RootSystem.ObjectDisposedException("HighDefinitionFaceFrame");
                }

                RootSystem.IntPtr objectPointer = Microsoft_Kinect_Face_HighDefinitionFaceFrame_get_FaceModel(_pNative);
                Helper.ExceptionHelper.CheckLastError();
                if (objectPointer == RootSystem.IntPtr.Zero)
                {
                    return null;
                }

                return Helper.NativeObjectCache.CreateOrGetObject<Microsoft.Kinect.Face.FaceModel>(objectPointer, n => new Microsoft.Kinect.Face.FaceModel(n));
            }
        }

        [RootSystem.Runtime.InteropServices.DllImport("KinectFaceUnityAddin", CallingConvention=RootSystem.Runtime.InteropServices.CallingConvention.Cdecl, SetLastError=true)]
        private static extern RootSystem.IntPtr Microsoft_Kinect_Face_HighDefinitionFaceFrame_get_HighDefinitionFaceFrameSource(RootSystem.IntPtr pNative);
        public  Microsoft.Kinect.Face.HighDefinitionFaceFrameSource HighDefinitionFaceFrameSource
        {
            get
            {
                if (_pNative == RootSystem.IntPtr.Zero)
                {
                    throw new RootSystem.ObjectDisposedException("HighDefinitionFaceFrame");
                }

                RootSystem.IntPtr objectPointer = Microsoft_Kinect_Face_HighDefinitionFaceFrame_get_HighDefinitionFaceFrameSource(_pNative);
                Helper.ExceptionHelper.CheckLastError();
                if (objectPointer == RootSystem.IntPtr.Zero)
                {
                    return null;
                }

                return Helper.NativeObjectCache.CreateOrGetObject<Microsoft.Kinect.Face.HighDefinitionFaceFrameSource>(objectPointer, n => new Microsoft.Kinect.Face.HighDefinitionFaceFrameSource(n));
            }
        }

        [RootSystem.Runtime.InteropServices.DllImport("KinectFaceUnityAddin", CallingConvention=RootSystem.Runtime.InteropServices.CallingConvention.Cdecl, SetLastError=true)]
        private static extern RootSystem.IntPtr Microsoft_Kinect_Face_HighDefinitionFaceFrame_get_InfraredFrameReference(RootSystem.IntPtr pNative);
        public  Windows.Kinect.InfraredFrameReference InfraredFrameReference
        {
            get
            {
                if (_pNative == RootSystem.IntPtr.Zero)
                {
                    throw new RootSystem.ObjectDisposedException("HighDefinitionFaceFrame");
                }

                RootSystem.IntPtr objectPointer = Microsoft_Kinect_Face_HighDefinitionFaceFrame_get_InfraredFrameReference(_pNative);
                Helper.ExceptionHelper.CheckLastError();
                if (objectPointer == RootSystem.IntPtr.Zero)
                {
                    return null;
                }

                return Helper.NativeObjectCache.CreateOrGetObject<Windows.Kinect.InfraredFrameReference>(objectPointer, n => new Windows.Kinect.InfraredFrameReference(n));
            }
        }

        [RootSystem.Runtime.InteropServices.DllImport("KinectFaceUnityAddin", CallingConvention=RootSystem.Runtime.InteropServices.CallingConvention.Cdecl, SetLastError=true)]
        private static extern bool Microsoft_Kinect_Face_HighDefinitionFaceFrame_get_IsFaceTracked(RootSystem.IntPtr pNative);
        public  bool IsFaceTracked
        {
            get
            {
                if (_pNative == RootSystem.IntPtr.Zero)
                {
                    throw new RootSystem.ObjectDisposedException("HighDefinitionFaceFrame");
                }

                return Microsoft_Kinect_Face_HighDefinitionFaceFrame_get_IsFaceTracked(_pNative);
            }
        }

        [RootSystem.Runtime.InteropServices.DllImport("KinectFaceUnityAddin", CallingConvention=RootSystem.Runtime.InteropServices.CallingConvention.Cdecl, SetLastError=true)]
        private static extern bool Microsoft_Kinect_Face_HighDefinitionFaceFrame_get_IsTrackingIdValid(RootSystem.IntPtr pNative);
        public  bool IsTrackingIdValid
        {
            get
            {
                if (_pNative == RootSystem.IntPtr.Zero)
                {
                    throw new RootSystem.ObjectDisposedException("HighDefinitionFaceFrame");
                }

                return Microsoft_Kinect_Face_HighDefinitionFaceFrame_get_IsTrackingIdValid(_pNative);
            }
        }

        [RootSystem.Runtime.InteropServices.DllImport("KinectFaceUnityAddin", CallingConvention=RootSystem.Runtime.InteropServices.CallingConvention.Cdecl, SetLastError=true)]
        private static extern long Microsoft_Kinect_Face_HighDefinitionFaceFrame_get_RelativeTime(RootSystem.IntPtr pNative);
        public  RootSystem.TimeSpan RelativeTime
        {
            get
            {
                if (_pNative == RootSystem.IntPtr.Zero)
                {
                    throw new RootSystem.ObjectDisposedException("HighDefinitionFaceFrame");
                }

                return RootSystem.TimeSpan.FromMilliseconds(Microsoft_Kinect_Face_HighDefinitionFaceFrame_get_RelativeTime(_pNative));
            }
        }

        [RootSystem.Runtime.InteropServices.DllImport("KinectFaceUnityAddin", CallingConvention=RootSystem.Runtime.InteropServices.CallingConvention.Cdecl, SetLastError=true)]
        private static extern ulong Microsoft_Kinect_Face_HighDefinitionFaceFrame_get_TrackingId(RootSystem.IntPtr pNative);
        public  ulong TrackingId
        {
            get
            {
                if (_pNative == RootSystem.IntPtr.Zero)
                {
                    throw new RootSystem.ObjectDisposedException("HighDefinitionFaceFrame");
                }

                return Microsoft_Kinect_Face_HighDefinitionFaceFrame_get_TrackingId(_pNative);
            }
        }


        // Public Methods
        [RootSystem.Runtime.InteropServices.DllImport("KinectFaceUnityAddin", CallingConvention=RootSystem.Runtime.InteropServices.CallingConvention.Cdecl, SetLastError=true)]
        private static extern void Microsoft_Kinect_Face_HighDefinitionFaceFrame_GetAndRefreshFaceAlignmentResult(RootSystem.IntPtr pNative, RootSystem.IntPtr faceAlignmentResults);
        public void GetAndRefreshFaceAlignmentResult(Microsoft.Kinect.Face.FaceAlignment faceAlignmentResults)
        {
            if (_pNative == RootSystem.IntPtr.Zero)
            {
                throw new RootSystem.ObjectDisposedException("HighDefinitionFaceFrame");
            }

            Microsoft_Kinect_Face_HighDefinitionFaceFrame_GetAndRefreshFaceAlignmentResult(_pNative, Helper.NativeWrapper.GetNativePtr(faceAlignmentResults));
            Helper.ExceptionHelper.CheckLastError();
        }

        [RootSystem.Runtime.InteropServices.DllImport("KinectFaceUnityAddin", CallingConvention=RootSystem.Runtime.InteropServices.CallingConvention.Cdecl, SetLastError=true)]
        private static extern void Microsoft_Kinect_Face_HighDefinitionFaceFrame_Dispose(RootSystem.IntPtr pNative);
        public void Dispose()
        {
            if (_pNative == RootSystem.IntPtr.Zero)
            {
                return;
            }

            Dispose(true);
            RootSystem.GC.SuppressFinalize(this);
        }

        private void __EventCleanup()
        {
        }
    }

}
