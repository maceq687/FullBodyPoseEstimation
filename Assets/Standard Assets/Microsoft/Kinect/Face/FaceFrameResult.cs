using RootSystem = System;
using System.Linq;
using System.Collections.Generic;
namespace Microsoft.Kinect.Face
{
    //
    // Microsoft.Kinect.Face.FaceFrameResult
    //
    public sealed partial class FaceFrameResult : Helper.INativeWrapper

    {
        internal RootSystem.IntPtr _pNative;
        RootSystem.IntPtr Helper.INativeWrapper.nativePtr { get { return _pNative; } }

        // Constructors and Finalizers
        internal FaceFrameResult(RootSystem.IntPtr pNative)
        {
            _pNative = pNative;
            Microsoft_Kinect_Face_FaceFrameResult_AddRefObject(ref _pNative);
        }

        ~FaceFrameResult()
        {
            Dispose(false);
        }

        [RootSystem.Runtime.InteropServices.DllImport("KinectFaceUnityAddin", CallingConvention=RootSystem.Runtime.InteropServices.CallingConvention.Cdecl, SetLastError=true)]
        private static extern void Microsoft_Kinect_Face_FaceFrameResult_ReleaseObject(ref RootSystem.IntPtr pNative);
        [RootSystem.Runtime.InteropServices.DllImport("KinectFaceUnityAddin", CallingConvention=RootSystem.Runtime.InteropServices.CallingConvention.Cdecl, SetLastError=true)]
        private static extern void Microsoft_Kinect_Face_FaceFrameResult_AddRefObject(ref RootSystem.IntPtr pNative);
        private void Dispose(bool disposing)
        {
            if (_pNative == RootSystem.IntPtr.Zero)
            {
                return;
            }

            __EventCleanup();

            Helper.NativeObjectCache.RemoveObject<FaceFrameResult>(_pNative);
                Microsoft_Kinect_Face_FaceFrameResult_ReleaseObject(ref _pNative);

            _pNative = RootSystem.IntPtr.Zero;
        }


        // Public Properties
        [RootSystem.Runtime.InteropServices.DllImport("KinectFaceUnityAddin", CallingConvention=RootSystem.Runtime.InteropServices.CallingConvention.Cdecl, SetLastError=true)]
        private static extern RootSystem.IntPtr Microsoft_Kinect_Face_FaceFrameResult_get_FaceBoundingBoxInColorSpace(RootSystem.IntPtr pNative);
        public  Microsoft.Kinect.Face.RectI FaceBoundingBoxInColorSpace
        {
            get
            {
                if (_pNative == RootSystem.IntPtr.Zero)
                {
                    throw new RootSystem.ObjectDisposedException("FaceFrameResult");
                }

                var objectPointer = Microsoft_Kinect_Face_FaceFrameResult_get_FaceBoundingBoxInColorSpace(_pNative);
                Helper.ExceptionHelper.CheckLastError();
                var obj = (Microsoft.Kinect.Face.RectI)RootSystem.Runtime.InteropServices.Marshal.PtrToStructure(objectPointer, typeof(Microsoft.Kinect.Face.RectI));
                Microsoft.Kinect.Face.KinectFaceUnityAddinUtils.FreeMemory(objectPointer);
                return obj;
            }
        }

        [RootSystem.Runtime.InteropServices.DllImport("KinectFaceUnityAddin", CallingConvention=RootSystem.Runtime.InteropServices.CallingConvention.Cdecl, SetLastError=true)]
        private static extern RootSystem.IntPtr Microsoft_Kinect_Face_FaceFrameResult_get_FaceBoundingBoxInInfraredSpace(RootSystem.IntPtr pNative);
        public  Microsoft.Kinect.Face.RectI FaceBoundingBoxInInfraredSpace
        {
            get
            {
                if (_pNative == RootSystem.IntPtr.Zero)
                {
                    throw new RootSystem.ObjectDisposedException("FaceFrameResult");
                }

                var objectPointer = Microsoft_Kinect_Face_FaceFrameResult_get_FaceBoundingBoxInInfraredSpace(_pNative);
                Helper.ExceptionHelper.CheckLastError();
                var obj = (Microsoft.Kinect.Face.RectI)RootSystem.Runtime.InteropServices.Marshal.PtrToStructure(objectPointer, typeof(Microsoft.Kinect.Face.RectI));
                Microsoft.Kinect.Face.KinectFaceUnityAddinUtils.FreeMemory(objectPointer);
                return obj;
            }
        }

        [RootSystem.Runtime.InteropServices.DllImport("KinectFaceUnityAddin", CallingConvention=RootSystem.Runtime.InteropServices.CallingConvention.Cdecl, SetLastError=true)]
        private static extern Microsoft.Kinect.Face.FaceFrameFeatures Microsoft_Kinect_Face_FaceFrameResult_get_FaceFrameFeatures(RootSystem.IntPtr pNative);
        public  Microsoft.Kinect.Face.FaceFrameFeatures FaceFrameFeatures
        {
            get
            {
                if (_pNative == RootSystem.IntPtr.Zero)
                {
                    throw new RootSystem.ObjectDisposedException("FaceFrameResult");
                }

                return Microsoft_Kinect_Face_FaceFrameResult_get_FaceFrameFeatures(_pNative);
            }
        }

        [RootSystem.Runtime.InteropServices.DllImport("KinectFaceUnityAddin", CallingConvention=RootSystem.Runtime.InteropServices.CallingConvention.Cdecl, SetLastError=true)]
        private static extern int Microsoft_Kinect_Face_FaceFrameResult_get_FaceProperties(RootSystem.IntPtr pNative, [RootSystem.Runtime.InteropServices.Out] Microsoft.Kinect.Face.FaceProperty[] outKeys, [RootSystem.Runtime.InteropServices.Out] Windows.Kinect.DetectionResult[] outValues, int outCollectionSize);
        [RootSystem.Runtime.InteropServices.DllImport("KinectFaceUnityAddin", CallingConvention=RootSystem.Runtime.InteropServices.CallingConvention.Cdecl, SetLastError=true)]
        private static extern int Microsoft_Kinect_Face_FaceFrameResult_get_FaceProperties_Length(RootSystem.IntPtr pNative);
        public  RootSystem.Collections.Generic.Dictionary<Microsoft.Kinect.Face.FaceProperty, Windows.Kinect.DetectionResult> FaceProperties
        {
            get
            {
                if (_pNative == RootSystem.IntPtr.Zero)
                {
                    throw new RootSystem.ObjectDisposedException("FaceFrameResult");
                }

                int outCollectionSize = Microsoft_Kinect_Face_FaceFrameResult_get_FaceProperties_Length(_pNative);
                var outKeys = new Microsoft.Kinect.Face.FaceProperty[outCollectionSize];
                var outValues = new Windows.Kinect.DetectionResult[outCollectionSize];
                var managedDictionary = new RootSystem.Collections.Generic.Dictionary<Microsoft.Kinect.Face.FaceProperty, Windows.Kinect.DetectionResult>();

                outCollectionSize = Microsoft_Kinect_Face_FaceFrameResult_get_FaceProperties(_pNative, outKeys, outValues, outCollectionSize);
                Helper.ExceptionHelper.CheckLastError();
                for(int i=0;i<outCollectionSize;i++)
                {
                    managedDictionary.Add(outKeys[i], outValues[i]);
                }
                return managedDictionary;
            }
        }

        [RootSystem.Runtime.InteropServices.DllImport("KinectFaceUnityAddin", CallingConvention=RootSystem.Runtime.InteropServices.CallingConvention.Cdecl, SetLastError=true)]
        private static extern RootSystem.IntPtr Microsoft_Kinect_Face_FaceFrameResult_get_FaceRotationQuaternion(RootSystem.IntPtr pNative);
        public  Windows.Kinect.Vector4 FaceRotationQuaternion
        {
            get
            {
                if (_pNative == RootSystem.IntPtr.Zero)
                {
                    throw new RootSystem.ObjectDisposedException("FaceFrameResult");
                }

                var objectPointer = Microsoft_Kinect_Face_FaceFrameResult_get_FaceRotationQuaternion(_pNative);
                Helper.ExceptionHelper.CheckLastError();
                var obj = (Windows.Kinect.Vector4)RootSystem.Runtime.InteropServices.Marshal.PtrToStructure(objectPointer, typeof(Windows.Kinect.Vector4));
                Microsoft.Kinect.Face.KinectFaceUnityAddinUtils.FreeMemory(objectPointer);
                return obj;
            }
        }

        [RootSystem.Runtime.InteropServices.DllImport("KinectFaceUnityAddin", CallingConvention=RootSystem.Runtime.InteropServices.CallingConvention.Cdecl, SetLastError=true)]
        private static extern long Microsoft_Kinect_Face_FaceFrameResult_get_RelativeTime(RootSystem.IntPtr pNative);
        public  RootSystem.TimeSpan RelativeTime
        {
            get
            {
                if (_pNative == RootSystem.IntPtr.Zero)
                {
                    throw new RootSystem.ObjectDisposedException("FaceFrameResult");
                }

                return RootSystem.TimeSpan.FromMilliseconds(Microsoft_Kinect_Face_FaceFrameResult_get_RelativeTime(_pNative));
            }
        }

        [RootSystem.Runtime.InteropServices.DllImport("KinectFaceUnityAddin", CallingConvention=RootSystem.Runtime.InteropServices.CallingConvention.Cdecl, SetLastError=true)]
        private static extern ulong Microsoft_Kinect_Face_FaceFrameResult_get_TrackingId(RootSystem.IntPtr pNative);
        public  ulong TrackingId
        {
            get
            {
                if (_pNative == RootSystem.IntPtr.Zero)
                {
                    throw new RootSystem.ObjectDisposedException("FaceFrameResult");
                }

                return Microsoft_Kinect_Face_FaceFrameResult_get_TrackingId(_pNative);
            }
        }

        private void __EventCleanup()
        {
        }
    }

}
