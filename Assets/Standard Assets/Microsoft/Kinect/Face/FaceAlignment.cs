using RootSystem = System;
using System.Linq;
using System.Collections.Generic;
namespace Microsoft.Kinect.Face
{
    //
    // Microsoft.Kinect.Face.FaceAlignment
    //
    public sealed partial class FaceAlignment : Helper.INativeWrapper

    {
        internal RootSystem.IntPtr _pNative;
        RootSystem.IntPtr Helper.INativeWrapper.nativePtr { get { return _pNative; } }

        // Constructors and Finalizers
        internal FaceAlignment(RootSystem.IntPtr pNative)
        {
            _pNative = pNative;
            Microsoft_Kinect_Face_FaceAlignment_AddRefObject(ref _pNative);
        }

        ~FaceAlignment()
        {
            Dispose(false);
        }

        [RootSystem.Runtime.InteropServices.DllImport("KinectFaceUnityAddin", CallingConvention=RootSystem.Runtime.InteropServices.CallingConvention.Cdecl, SetLastError=true)]
        private static extern void Microsoft_Kinect_Face_FaceAlignment_ReleaseObject(ref RootSystem.IntPtr pNative);
        [RootSystem.Runtime.InteropServices.DllImport("KinectFaceUnityAddin", CallingConvention=RootSystem.Runtime.InteropServices.CallingConvention.Cdecl, SetLastError=true)]
        private static extern void Microsoft_Kinect_Face_FaceAlignment_AddRefObject(ref RootSystem.IntPtr pNative);
        private void Dispose(bool disposing)
        {
            if (_pNative == RootSystem.IntPtr.Zero)
            {
                return;
            }

            __EventCleanup();

            Helper.NativeObjectCache.RemoveObject<FaceAlignment>(_pNative);
                Microsoft_Kinect_Face_FaceAlignment_ReleaseObject(ref _pNative);

            _pNative = RootSystem.IntPtr.Zero;
        }


        // Public Properties
        [RootSystem.Runtime.InteropServices.DllImport("KinectFaceUnityAddin", CallingConvention=RootSystem.Runtime.InteropServices.CallingConvention.Cdecl, SetLastError=true)]
        private static extern RootSystem.IntPtr Microsoft_Kinect_Face_FaceAlignment_get_HeadPivotPoint(RootSystem.IntPtr pNative);
        [RootSystem.Runtime.InteropServices.DllImport("KinectFaceUnityAddin", CallingConvention=RootSystem.Runtime.InteropServices.CallingConvention.Cdecl, SetLastError=true)]
        private static extern void Microsoft_Kinect_Face_FaceAlignment_put_HeadPivotPoint(RootSystem.IntPtr pNative, Windows.Kinect.CameraSpacePoint headPivotPoint);
        public  Windows.Kinect.CameraSpacePoint HeadPivotPoint
        {
            get
            {
                if (_pNative == RootSystem.IntPtr.Zero)
                {
                    throw new RootSystem.ObjectDisposedException("FaceAlignment");
                }

                var objectPointer = Microsoft_Kinect_Face_FaceAlignment_get_HeadPivotPoint(_pNative);
                Helper.ExceptionHelper.CheckLastError();
                var obj = (Windows.Kinect.CameraSpacePoint)RootSystem.Runtime.InteropServices.Marshal.PtrToStructure(objectPointer, typeof(Windows.Kinect.CameraSpacePoint));
                Microsoft.Kinect.Face.KinectFaceUnityAddinUtils.FreeMemory(objectPointer);
                return obj;
            }
            set
            {
                if (_pNative == RootSystem.IntPtr.Zero)
                {
                    throw new RootSystem.ObjectDisposedException("FaceAlignment");
                }

                Microsoft_Kinect_Face_FaceAlignment_put_HeadPivotPoint(_pNative, value);
                Helper.ExceptionHelper.CheckLastError();
            }
        }

        [RootSystem.Runtime.InteropServices.DllImport("KinectFaceUnityAddin", CallingConvention=RootSystem.Runtime.InteropServices.CallingConvention.Cdecl, SetLastError=true)]
        private static extern RootSystem.IntPtr Microsoft_Kinect_Face_FaceAlignment_get_FaceOrientation(RootSystem.IntPtr pNative);
        [RootSystem.Runtime.InteropServices.DllImport("KinectFaceUnityAddin", CallingConvention=RootSystem.Runtime.InteropServices.CallingConvention.Cdecl, SetLastError=true)]
        private static extern void Microsoft_Kinect_Face_FaceAlignment_put_FaceOrientation(RootSystem.IntPtr pNative, Windows.Kinect.Vector4 faceOrientation);
        public  Windows.Kinect.Vector4 FaceOrientation
        {
            get
            {
                if (_pNative == RootSystem.IntPtr.Zero)
                {
                    throw new RootSystem.ObjectDisposedException("FaceAlignment");
                }

                var objectPointer = Microsoft_Kinect_Face_FaceAlignment_get_FaceOrientation(_pNative);
                Helper.ExceptionHelper.CheckLastError();
                var obj = (Windows.Kinect.Vector4)RootSystem.Runtime.InteropServices.Marshal.PtrToStructure(objectPointer, typeof(Windows.Kinect.Vector4));
                Microsoft.Kinect.Face.KinectFaceUnityAddinUtils.FreeMemory(objectPointer);
                return obj;
            }
            set
            {
                if (_pNative == RootSystem.IntPtr.Zero)
                {
                    throw new RootSystem.ObjectDisposedException("FaceAlignment");
                }

                Microsoft_Kinect_Face_FaceAlignment_put_FaceOrientation(_pNative, value);
                Helper.ExceptionHelper.CheckLastError();
            }
        }

        [RootSystem.Runtime.InteropServices.DllImport("KinectFaceUnityAddin", CallingConvention=RootSystem.Runtime.InteropServices.CallingConvention.Cdecl, SetLastError=true)]
        private static extern int Microsoft_Kinect_Face_FaceAlignment_get_AnimationUnits(RootSystem.IntPtr pNative, [RootSystem.Runtime.InteropServices.Out] Microsoft.Kinect.Face.FaceShapeAnimations[] outKeys, float[] outValues, int outCollectionSize);
        [RootSystem.Runtime.InteropServices.DllImport("KinectFaceUnityAddin", CallingConvention=RootSystem.Runtime.InteropServices.CallingConvention.Cdecl, SetLastError=true)]
        private static extern int Microsoft_Kinect_Face_FaceAlignment_get_AnimationUnits_Length(RootSystem.IntPtr pNative);
        public  RootSystem.Collections.Generic.Dictionary<Microsoft.Kinect.Face.FaceShapeAnimations, float> AnimationUnits
        {
            get
            {
                if (_pNative == RootSystem.IntPtr.Zero)
                {
                    throw new RootSystem.ObjectDisposedException("FaceAlignment");
                }

                int outCollectionSize = Microsoft_Kinect_Face_FaceAlignment_get_AnimationUnits_Length(_pNative);
                var outKeys = new Microsoft.Kinect.Face.FaceShapeAnimations[outCollectionSize];
                var outValues = new float[outCollectionSize];
                var managedDictionary = new RootSystem.Collections.Generic.Dictionary<Microsoft.Kinect.Face.FaceShapeAnimations, float>();

                outCollectionSize = Microsoft_Kinect_Face_FaceAlignment_get_AnimationUnits(_pNative, outKeys, outValues, outCollectionSize);
                Helper.ExceptionHelper.CheckLastError();
                for(int i=0;i<outCollectionSize;i++)
                {
                    managedDictionary.Add(outKeys[i], outValues[i]);
                }
                return managedDictionary;
            }
        }

        [RootSystem.Runtime.InteropServices.DllImport("KinectFaceUnityAddin", CallingConvention=RootSystem.Runtime.InteropServices.CallingConvention.Cdecl, SetLastError=true)]
        private static extern RootSystem.IntPtr Microsoft_Kinect_Face_FaceAlignment_get_FaceBoundingBox(RootSystem.IntPtr pNative);
        public  Microsoft.Kinect.Face.RectI FaceBoundingBox
        {
            get
            {
                if (_pNative == RootSystem.IntPtr.Zero)
                {
                    throw new RootSystem.ObjectDisposedException("FaceAlignment");
                }

                var objectPointer = Microsoft_Kinect_Face_FaceAlignment_get_FaceBoundingBox(_pNative);
                Helper.ExceptionHelper.CheckLastError();
                var obj = (Microsoft.Kinect.Face.RectI)RootSystem.Runtime.InteropServices.Marshal.PtrToStructure(objectPointer, typeof(Microsoft.Kinect.Face.RectI));
                Microsoft.Kinect.Face.KinectFaceUnityAddinUtils.FreeMemory(objectPointer);
                return obj;
            }
        }

        [RootSystem.Runtime.InteropServices.DllImport("KinectFaceUnityAddin", CallingConvention=RootSystem.Runtime.InteropServices.CallingConvention.Cdecl, SetLastError=true)]
        private static extern Microsoft.Kinect.Face.FaceAlignmentQuality Microsoft_Kinect_Face_FaceAlignment_get_Quality(RootSystem.IntPtr pNative);
        public  Microsoft.Kinect.Face.FaceAlignmentQuality Quality
        {
            get
            {
                if (_pNative == RootSystem.IntPtr.Zero)
                {
                    throw new RootSystem.ObjectDisposedException("FaceAlignment");
                }

                return Microsoft_Kinect_Face_FaceAlignment_get_Quality(_pNative);
            }
        }

        private void __EventCleanup()
        {
        }
    }

}
