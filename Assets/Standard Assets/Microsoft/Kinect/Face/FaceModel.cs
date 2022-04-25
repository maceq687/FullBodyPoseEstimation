using RootSystem = System;
using System.Linq;
using System.Collections.Generic;
namespace Microsoft.Kinect.Face
{
    //
    // Microsoft.Kinect.Face.FaceModel
    //
    public sealed partial class FaceModel : RootSystem.IDisposable, Helper.INativeWrapper

    {
        internal RootSystem.IntPtr _pNative;
        RootSystem.IntPtr Helper.INativeWrapper.nativePtr { get { return _pNative; } }

        // Constructors and Finalizers
        internal FaceModel(RootSystem.IntPtr pNative)
        {
            _pNative = pNative;
            Microsoft_Kinect_Face_FaceModel_AddRefObject(ref _pNative);
        }

        ~FaceModel()
        {
            Dispose(false);
        }

        [RootSystem.Runtime.InteropServices.DllImport("KinectFaceUnityAddin", CallingConvention=RootSystem.Runtime.InteropServices.CallingConvention.Cdecl, SetLastError=true)]
        private static extern void Microsoft_Kinect_Face_FaceModel_ReleaseObject(ref RootSystem.IntPtr pNative);
        [RootSystem.Runtime.InteropServices.DllImport("KinectFaceUnityAddin", CallingConvention=RootSystem.Runtime.InteropServices.CallingConvention.Cdecl, SetLastError=true)]
        private static extern void Microsoft_Kinect_Face_FaceModel_AddRefObject(ref RootSystem.IntPtr pNative);
        private void Dispose(bool disposing)
        {
            if (_pNative == RootSystem.IntPtr.Zero)
            {
                return;
            }

            __EventCleanup();

            Helper.NativeObjectCache.RemoveObject<FaceModel>(_pNative);

            if (disposing)
            {
                Microsoft_Kinect_Face_FaceModel_Dispose(_pNative);
            }
                Microsoft_Kinect_Face_FaceModel_ReleaseObject(ref _pNative);

            _pNative = RootSystem.IntPtr.Zero;
        }


        // Public Properties
        [RootSystem.Runtime.InteropServices.DllImport("KinectFaceUnityAddin", CallingConvention=RootSystem.Runtime.InteropServices.CallingConvention.Cdecl, SetLastError=true)]
        private static extern int Microsoft_Kinect_Face_FaceModel_get_FaceShapeDeformations(RootSystem.IntPtr pNative, [RootSystem.Runtime.InteropServices.Out] Microsoft.Kinect.Face.FaceShapeDeformations[] outKeys, float[] outValues, int outCollectionSize);
        [RootSystem.Runtime.InteropServices.DllImport("KinectFaceUnityAddin", CallingConvention=RootSystem.Runtime.InteropServices.CallingConvention.Cdecl, SetLastError=true)]
        private static extern int Microsoft_Kinect_Face_FaceModel_get_FaceShapeDeformations_Length(RootSystem.IntPtr pNative);
        public  RootSystem.Collections.Generic.Dictionary<Microsoft.Kinect.Face.FaceShapeDeformations, float> FaceShapeDeformations
        {
            get
            {
                if (_pNative == RootSystem.IntPtr.Zero)
                {
                    throw new RootSystem.ObjectDisposedException("FaceModel");
                }

                int outCollectionSize = Microsoft_Kinect_Face_FaceModel_get_FaceShapeDeformations_Length(_pNative);
                var outKeys = new Microsoft.Kinect.Face.FaceShapeDeformations[outCollectionSize];
                var outValues = new float[outCollectionSize];
                var managedDictionary = new RootSystem.Collections.Generic.Dictionary<Microsoft.Kinect.Face.FaceShapeDeformations, float>();

                outCollectionSize = Microsoft_Kinect_Face_FaceModel_get_FaceShapeDeformations(_pNative, outKeys, outValues, outCollectionSize);
                Helper.ExceptionHelper.CheckLastError();
                for(int i=0;i<outCollectionSize;i++)
                {
                    managedDictionary.Add(outKeys[i], outValues[i]);
                }
                return managedDictionary;
            }
        }

        [RootSystem.Runtime.InteropServices.DllImport("KinectFaceUnityAddin", CallingConvention=RootSystem.Runtime.InteropServices.CallingConvention.Cdecl, SetLastError=true)]
        private static extern float Microsoft_Kinect_Face_FaceModel_get_Scale(RootSystem.IntPtr pNative);
        public  float Scale
        {
            get
            {
                if (_pNative == RootSystem.IntPtr.Zero)
                {
                    throw new RootSystem.ObjectDisposedException("FaceModel");
                }

                return Microsoft_Kinect_Face_FaceModel_get_Scale(_pNative);
            }
        }

        [RootSystem.Runtime.InteropServices.DllImport("KinectFaceUnityAddin", CallingConvention=RootSystem.Runtime.InteropServices.CallingConvention.Cdecl, SetLastError=true)]
        private static extern uint Microsoft_Kinect_Face_FaceModel_get_TriangleCount();
        public static uint TriangleCount
        {
            get
            {
                return Microsoft_Kinect_Face_FaceModel_get_TriangleCount();
            }
        }

        [RootSystem.Runtime.InteropServices.DllImport("KinectFaceUnityAddin", CallingConvention=RootSystem.Runtime.InteropServices.CallingConvention.Cdecl, SetLastError=true)]
        private static extern int Microsoft_Kinect_Face_FaceModel_get_TriangleIndices(uint[] outCollection, int outCollectionSize);
        [RootSystem.Runtime.InteropServices.DllImport("KinectFaceUnityAddin", CallingConvention=RootSystem.Runtime.InteropServices.CallingConvention.Cdecl, SetLastError=true)]
        private static extern int Microsoft_Kinect_Face_FaceModel_get_TriangleIndices_Length();
        public static RootSystem.Collections.Generic.IList<uint> TriangleIndices
        {
            get
            {
                int outCollectionSize = Microsoft_Kinect_Face_FaceModel_get_TriangleIndices_Length();
                var outCollection = new uint[outCollectionSize];
                var managedCollection = new uint[outCollectionSize];

                outCollectionSize = Microsoft_Kinect_Face_FaceModel_get_TriangleIndices(outCollection, outCollectionSize);
                Helper.ExceptionHelper.CheckLastError();
                for(int i=0;i<outCollectionSize;i++)
                {
                    managedCollection[i] = outCollection[i];
                }
                return managedCollection;
            }
        }

        [RootSystem.Runtime.InteropServices.DllImport("KinectFaceUnityAddin", CallingConvention=RootSystem.Runtime.InteropServices.CallingConvention.Cdecl, SetLastError=true)]
        private static extern uint Microsoft_Kinect_Face_FaceModel_get_VertexCount();
        public static uint VertexCount
        {
            get
            {
                return Microsoft_Kinect_Face_FaceModel_get_VertexCount();
            }
        }


        // Public Methods
        [RootSystem.Runtime.InteropServices.DllImport("KinectFaceUnityAddin", CallingConvention=RootSystem.Runtime.InteropServices.CallingConvention.Cdecl, SetLastError=true)]
        private static extern int Microsoft_Kinect_Face_FaceModel_CalculateVerticesForAlignment_Length(RootSystem.IntPtr pNative, RootSystem.IntPtr faceAlignment);
        [RootSystem.Runtime.InteropServices.DllImport("KinectFaceUnityAddin", CallingConvention=RootSystem.Runtime.InteropServices.CallingConvention.Cdecl, SetLastError=true)]
        private static extern int Microsoft_Kinect_Face_FaceModel_CalculateVerticesForAlignment(RootSystem.IntPtr pNative, RootSystem.IntPtr faceAlignment, [RootSystem.Runtime.InteropServices.Out] Windows.Kinect.CameraSpacePoint[] outCollection, int outCollectionSize);
        public RootSystem.Collections.Generic.IList<Windows.Kinect.CameraSpacePoint> CalculateVerticesForAlignment(Microsoft.Kinect.Face.FaceAlignment faceAlignment)
        {
            if (_pNative == RootSystem.IntPtr.Zero)
            {
                throw new RootSystem.ObjectDisposedException("FaceModel");
            }

            int outCollectionSize = Microsoft_Kinect_Face_FaceModel_CalculateVerticesForAlignment_Length(_pNative, Helper.NativeWrapper.GetNativePtr(faceAlignment));
            var outCollection = new Windows.Kinect.CameraSpacePoint[outCollectionSize];
            var managedCollection = new Windows.Kinect.CameraSpacePoint[outCollectionSize];

            outCollectionSize = Microsoft_Kinect_Face_FaceModel_CalculateVerticesForAlignment(_pNative, Helper.NativeWrapper.GetNativePtr(faceAlignment), outCollection, outCollectionSize);
            Helper.ExceptionHelper.CheckLastError();
            for(int i=0;i<outCollectionSize;i++)
            {
                managedCollection[i] = outCollection[i];
            }
            return managedCollection;
        }

        [RootSystem.Runtime.InteropServices.DllImport("KinectFaceUnityAddin", CallingConvention=RootSystem.Runtime.InteropServices.CallingConvention.Cdecl, SetLastError=true)]
        private static extern void Microsoft_Kinect_Face_FaceModel_Dispose(RootSystem.IntPtr pNative);
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
