using RootSystem = System;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace Microsoft.Kinect.Face
{
    //
    // Microsoft.Kinect.Face.Point
    //
    [RootSystem.Runtime.InteropServices.StructLayout(RootSystem.Runtime.InteropServices.LayoutKind.Sequential)]
    public struct Point
    {
        public float X { get; set; }
        public float Y { get; set; }

        public override int GetHashCode()
        {
            return X.GetHashCode() ^ Y.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (!(obj is Point))
            {
                return false;
            }

            return this.Equals((Point)obj);
        }

        public bool Equals(Point obj)
        {
            return X.Equals(obj.X) && Y.Equals(obj.Y);
        }

        public static bool operator ==(Point a, Point b)
        {
            return a.Equals(b);
        }

        public static bool operator !=(Point a, Point b)
        {
            return !(a.Equals(b));
        }
    }

    //
    // Microsoft.Kinect.Face.Color
    //
    [RootSystem.Runtime.InteropServices.StructLayout(RootSystem.Runtime.InteropServices.LayoutKind.Sequential)]
    public struct Color
    {
        public byte A { get; set; }
        public byte R { get; set; }
        public byte G { get; set; }
        public byte B { get; set; }

        public override int GetHashCode()
        {
            return A.GetHashCode() ^ R.GetHashCode() ^ G.GetHashCode() ^ B.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (!(obj is Color))
            {
                return false;
            }

            return this.Equals((Color)obj);
        }

        public bool Equals(Color obj)
        {
            return A.Equals(obj.A) && R.Equals(obj.R) && G.Equals(obj.G) && B.Equals(obj.B);
        }

        public static bool operator ==(Color a, Color b)
        {
            return a.Equals(b);
        }

        public static bool operator !=(Color a, Color b)
        {
            return !(a.Equals(b));
        }
    }

    //
    // Microsoft.Kinect.Face.FaceModel
    //
    public sealed partial class FaceModel
    {   
        [RootSystem.Runtime.InteropServices.DllImport("KinectFaceUnityAddin", CallingConvention=RootSystem.Runtime.InteropServices.CallingConvention.Cdecl)]
        private static extern RootSystem.IntPtr Microsoft_Kinect_Face_FaceModel_ctor(float scale, Microsoft.Kinect.Face.FaceShapeDeformations[] faceShapeDeformationsKeys, float[] faceShapeDeformationsValues, int faceShapeDeformationsSize);
        public static FaceModel Create(float scale, RootSystem.Collections.Generic.Dictionary<Microsoft.Kinect.Face.FaceShapeDeformations, float> faceShapeDeformations)
        {
            int _faceShapeDeformationsKeys_idx=0;
            var _faceShapeDeformationsKeys = new Microsoft.Kinect.Face.FaceShapeDeformations[faceShapeDeformations.Keys.Count];
            foreach(var key in faceShapeDeformations.Keys)
            {
                _faceShapeDeformationsKeys[_faceShapeDeformationsKeys_idx] = (Microsoft.Kinect.Face.FaceShapeDeformations)key;
                _faceShapeDeformationsKeys_idx++;
            }
            int _faceShapeDeformationsValues_idx=0;
            var _faceShapeDeformationsValues = new float[faceShapeDeformations.Values.Count];
            foreach(var value in faceShapeDeformations.Values)
            {
                _faceShapeDeformationsValues[_faceShapeDeformationsValues_idx] = (float)value;
                _faceShapeDeformationsValues_idx++;
            }

            RootSystem.IntPtr objectPointer = Microsoft_Kinect_Face_FaceModel_ctor(scale, _faceShapeDeformationsKeys, _faceShapeDeformationsValues, faceShapeDeformations.Count);
            Helper.ExceptionHelper.CheckLastError();
            if (objectPointer == RootSystem.IntPtr.Zero)
            {
                return null;
            }

            return Helper.NativeObjectCache.CreateOrGetObject<Microsoft.Kinect.Face.FaceModel>(
                objectPointer, n => new Microsoft.Kinect.Face.FaceModel(n));
        }

        [RootSystem.Runtime.InteropServices.DllImport("KinectFaceUnityAddin", CallingConvention=RootSystem.Runtime.InteropServices.CallingConvention.Cdecl)]
        private static extern RootSystem.IntPtr Microsoft_Kinect_Face_FaceModel_ctor1();
        public static FaceModel Create()
        {
            RootSystem.IntPtr objectPointer = Microsoft_Kinect_Face_FaceModel_ctor1();
            Helper.ExceptionHelper.CheckLastError();
            if (objectPointer == RootSystem.IntPtr.Zero)
            {
                return null;
            }

            return Helper.NativeObjectCache.CreateOrGetObject<Microsoft.Kinect.Face.FaceModel>(
                objectPointer, n => new Microsoft.Kinect.Face.FaceModel(n));
        }

        [RootSystem.Runtime.InteropServices.DllImport("KinectFaceUnityAddin", CallingConvention=RootSystem.Runtime.InteropServices.CallingConvention.Cdecl)]
        private static extern RootSystem.IntPtr Microsoft_Kinect_Face_FaceModel_get_HairColor(RootSystem.IntPtr pNative);
        public  Microsoft.Kinect.Face.Color HairColor
        {
            get
            {
                if (_pNative == RootSystem.IntPtr.Zero)
                {
                    throw new RootSystem.ObjectDisposedException("FaceModel");
                }

                var objectPointer = Microsoft_Kinect_Face_FaceModel_get_HairColor(_pNative);
                Helper.ExceptionHelper.CheckLastError();
                var obj = (Microsoft.Kinect.Face.Color)RootSystem.Runtime.InteropServices.Marshal.PtrToStructure(objectPointer, typeof(Microsoft.Kinect.Face.Color));
                Microsoft.Kinect.Face.KinectFaceUnityAddinUtils.FreeMemory(objectPointer);
                return obj;
            }
        }

        [RootSystem.Runtime.InteropServices.DllImport("KinectFaceUnityAddin", CallingConvention=RootSystem.Runtime.InteropServices.CallingConvention.Cdecl)]
        private static extern RootSystem.IntPtr Microsoft_Kinect_Face_FaceModel_get_SkinColor(RootSystem.IntPtr pNative);
        public  Microsoft.Kinect.Face.Color SkinColor
        {
            get
            {
                if (_pNative == RootSystem.IntPtr.Zero)
                {
                    throw new RootSystem.ObjectDisposedException("FaceModel");
                }

                var objectPointer = Microsoft_Kinect_Face_FaceModel_get_SkinColor(_pNative);
                Helper.ExceptionHelper.CheckLastError();
                var obj = (Microsoft.Kinect.Face.Color)RootSystem.Runtime.InteropServices.Marshal.PtrToStructure(objectPointer, typeof(Microsoft.Kinect.Face.Color));
                Microsoft.Kinect.Face.KinectFaceUnityAddinUtils.FreeMemory(objectPointer);
                return obj;
            }
        }
    }

    //
    // Microsoft.Kinect.Face.FaceFrameResult
    //
    public sealed partial class FaceFrameResult
    {
        [RootSystem.Runtime.InteropServices.DllImport("KinectFaceUnityAddin", CallingConvention=RootSystem.Runtime.InteropServices.CallingConvention.Cdecl)]
        private static extern int Microsoft_Kinect_Face_FaceFrameResult_get_FacePointsInColorSpace(RootSystem.IntPtr pNative, [RootSystem.Runtime.InteropServices.Out] Microsoft.Kinect.Face.FacePointType[] outKeys, [RootSystem.Runtime.InteropServices.Out] Microsoft.Kinect.Face.Point[] outValues, int outCollectionSize);
        [RootSystem.Runtime.InteropServices.DllImport("KinectFaceUnityAddin", CallingConvention=RootSystem.Runtime.InteropServices.CallingConvention.Cdecl)]
        private static extern int Microsoft_Kinect_Face_FaceFrameResult_get_FacePointsInColorSpace_Length(RootSystem.IntPtr pNative);
        public  RootSystem.Collections.Generic.Dictionary<Microsoft.Kinect.Face.FacePointType, Microsoft.Kinect.Face.Point> FacePointsInColorSpace
        {
            get
            {
                if (_pNative == RootSystem.IntPtr.Zero)
                {
                    throw new RootSystem.ObjectDisposedException("FaceFrameResult");
                }

                int outCollectionSize = Microsoft_Kinect_Face_FaceFrameResult_get_FacePointsInColorSpace_Length(_pNative);
                var outKeys = new Microsoft.Kinect.Face.FacePointType[outCollectionSize];
                var outValues = new Microsoft.Kinect.Face.Point[outCollectionSize];
                var managedDictionary = new RootSystem.Collections.Generic.Dictionary<Microsoft.Kinect.Face.FacePointType, Microsoft.Kinect.Face.Point>();

                outCollectionSize = Microsoft_Kinect_Face_FaceFrameResult_get_FacePointsInColorSpace(_pNative, outKeys, outValues, outCollectionSize);
                Helper.ExceptionHelper.CheckLastError();
                for(int i=0;i<outCollectionSize;i++)
                {
                    managedDictionary.Add(outKeys[i], outValues[i]);
                }
                return managedDictionary;
            }
        }

        [RootSystem.Runtime.InteropServices.DllImport("KinectFaceUnityAddin", CallingConvention=RootSystem.Runtime.InteropServices.CallingConvention.Cdecl)]
        private static extern int Microsoft_Kinect_Face_FaceFrameResult_get_FacePointsInInfraredSpace(RootSystem.IntPtr pNative, [RootSystem.Runtime.InteropServices.Out] Microsoft.Kinect.Face.FacePointType[] outKeys, [RootSystem.Runtime.InteropServices.Out] Microsoft.Kinect.Face.Point[] outValues, int outCollectionSize);
        [RootSystem.Runtime.InteropServices.DllImport("KinectFaceUnityAddin", CallingConvention=RootSystem.Runtime.InteropServices.CallingConvention.Cdecl)]
        private static extern int Microsoft_Kinect_Face_FaceFrameResult_get_FacePointsInInfraredSpace_Length(RootSystem.IntPtr pNative);
        public  RootSystem.Collections.Generic.Dictionary<Microsoft.Kinect.Face.FacePointType, Microsoft.Kinect.Face.Point> FacePointsInInfraredSpace
        {
            get
            {
                if (_pNative == RootSystem.IntPtr.Zero)
                {
                    throw new RootSystem.ObjectDisposedException("FaceFrameResult");
                }

                int outCollectionSize = Microsoft_Kinect_Face_FaceFrameResult_get_FacePointsInInfraredSpace_Length(_pNative);
                var outKeys = new Microsoft.Kinect.Face.FacePointType[outCollectionSize];
                var outValues = new Microsoft.Kinect.Face.Point[outCollectionSize];
                var managedDictionary = new RootSystem.Collections.Generic.Dictionary<Microsoft.Kinect.Face.FacePointType, Microsoft.Kinect.Face.Point>();

                outCollectionSize = Microsoft_Kinect_Face_FaceFrameResult_get_FacePointsInInfraredSpace(_pNative, outKeys, outValues, outCollectionSize);
                Helper.ExceptionHelper.CheckLastError();
                for(int i=0;i<outCollectionSize;i++)
                {
                    managedDictionary.Add(outKeys[i], outValues[i]);
                }
                return managedDictionary;
            }
        }
    }

    //
    // Microsoft.Kinect.Face.FaceAlignment
    //
    public sealed partial class FaceAlignment
    {
        [RootSystem.Runtime.InteropServices.DllImport("KinectFaceUnityAddin", CallingConvention = RootSystem.Runtime.InteropServices.CallingConvention.Cdecl)]
        private static extern RootSystem.IntPtr Microsoft_Kinect_Face_FaceAlignment_ctor();
        public static FaceAlignment Create()
        {
            RootSystem.IntPtr objectPointer = Microsoft_Kinect_Face_FaceAlignment_ctor();
            Helper.ExceptionHelper.CheckLastError();
            if (objectPointer == RootSystem.IntPtr.Zero)
            {
                return null;
            }

            return Helper.NativeObjectCache.CreateOrGetObject<Microsoft.Kinect.Face.FaceAlignment>(
                objectPointer, n => new Microsoft.Kinect.Face.FaceAlignment(n));
        }
    }

    //
    // Microsoft.Kinect.Face.FaceFrameSource
    //
    public sealed partial class FaceFrameSource
    {
        [RootSystem.Runtime.InteropServices.DllImport("KinectFaceUnityAddin", CallingConvention = RootSystem.Runtime.InteropServices.CallingConvention.Cdecl)]
        private static extern RootSystem.IntPtr Microsoft_Kinect_Face_FaceFrameSource_ctor(RootSystem.IntPtr sensor, ulong initialTrackingId, Microsoft.Kinect.Face.FaceFrameFeatures initialFaceFrameFeatures);
        public static FaceFrameSource Create(Windows.Kinect.KinectSensor sensor, ulong initialTrackingId, Microsoft.Kinect.Face.FaceFrameFeatures initialFaceFrameFeatures)
        {
            RootSystem.IntPtr objectPointer = Microsoft_Kinect_Face_FaceFrameSource_ctor(Helper.NativeWrapper.GetNativePtr(sensor), initialTrackingId, initialFaceFrameFeatures);
            Helper.ExceptionHelper.CheckLastError();
            if (objectPointer == RootSystem.IntPtr.Zero)
            {
                return null;
            }

            return Helper.NativeObjectCache.CreateOrGetObject<Microsoft.Kinect.Face.FaceFrameSource>(
                objectPointer, n => new Microsoft.Kinect.Face.FaceFrameSource(n));
        }
    }

    //
    // Microsoft.Kinect.Face.HighDefinitionFaceFrameSource
    //
    public sealed partial class HighDefinitionFaceFrameSource
    {
        [RootSystem.Runtime.InteropServices.DllImport("KinectFaceUnityAddin", CallingConvention = RootSystem.Runtime.InteropServices.CallingConvention.Cdecl)]
        private static extern RootSystem.IntPtr Microsoft_Kinect_Face_HighDefinitionFaceFrameSource_ctor(RootSystem.IntPtr sensor);
        public static HighDefinitionFaceFrameSource Create(Windows.Kinect.KinectSensor sensor)
        {
            RootSystem.IntPtr objectPointer = Microsoft_Kinect_Face_HighDefinitionFaceFrameSource_ctor(Helper.NativeWrapper.GetNativePtr(sensor));
            Helper.ExceptionHelper.CheckLastError();
            if (objectPointer == RootSystem.IntPtr.Zero)
            {
                return null;
            }

            return Helper.NativeObjectCache.CreateOrGetObject<Microsoft.Kinect.Face.HighDefinitionFaceFrameSource>(
                objectPointer, n => new Microsoft.Kinect.Face.HighDefinitionFaceFrameSource(n));
        }
    }

    //
    // Microsoft.Kinect.Face.FaceModelBuilder
    //
    public sealed partial class FaceModelBuilder
    {
        [RootSystem.Runtime.InteropServices.UnmanagedFunctionPointer(RootSystem.Runtime.InteropServices.CallingConvention.Cdecl)]
        private delegate void Microsoft_Kinect_Face_FaceModelData_Delegate_Indexed(RootSystem.IntPtr result, [RootSystem.Runtime.InteropServices.MarshalAs(RootSystem.Runtime.InteropServices.UnmanagedType.LPWStr)] string guid);
        private static Helper.ThreadSafeDictionary<string, RootSystem.Action<Microsoft.Kinect.Face.FaceModelData>> Microsoft_Kinect_Face_FaceModelData_Delegate_SuccessCallbacks = new Helper.ThreadSafeDictionary<string, RootSystem.Action<Microsoft.Kinect.Face.FaceModelData>>();
        [AOT.MonoPInvokeCallback(typeof(Microsoft_Kinect_Face_FaceModelData_Delegate_Indexed))]
        private static void Microsoft_Kinect_Face_FaceModelData_Delegate_Success(RootSystem.IntPtr result, string guid)
        {
            List<Helper.SmartGCHandle> pins;
            if(PinnedObjects.TryGetValue(guid, out pins))
            {
                foreach(var pin in pins)
                {
                    pin.Dispose();
                }
                PinnedObjects.Remove(guid);
            }
            RootSystem.Action<Microsoft.Kinect.Face.FaceModelData> callback = null;
            if(Microsoft_Kinect_Face_FaceModelData_Delegate_SuccessCallbacks.TryGetValue(guid, out callback))
            {
                var faceModelData = Helper.NativeObjectCache.CreateOrGetObject<Microsoft.Kinect.Face.FaceModelData>(result, n => new Microsoft.Kinect.Face.FaceModelData(n));
                Helper.EventPump.Instance.Enqueue(() => callback(faceModelData));
            }
            ErrorCallbacks.Remove(guid);
            Microsoft_Kinect_Face_FaceModelData_Delegate_SuccessCallbacks.Remove(guid);
        }
        [RootSystem.Runtime.InteropServices.UnmanagedFunctionPointer(RootSystem.Runtime.InteropServices.CallingConvention.Cdecl)]
        private delegate void Error_Delegate_Indexed(int err, [RootSystem.Runtime.InteropServices.MarshalAs(RootSystem.Runtime.InteropServices.UnmanagedType.LPWStr)] string guid);
        private static Helper.ThreadSafeDictionary<string, RootSystem.Action<int>> ErrorCallbacks = new Helper.ThreadSafeDictionary<string, RootSystem.Action<int>>();
        private static Helper.CollectionMap<string, RootSystem.Collections.Generic.List<Helper.SmartGCHandle>> PinnedObjects = new Helper.CollectionMap<string, RootSystem.Collections.Generic.List<Helper.SmartGCHandle>>();
        [AOT.MonoPInvokeCallback(typeof(Error_Delegate_Indexed))]
        private static void Error_Delegate_Failure(int err, [RootSystem.Runtime.InteropServices.MarshalAs(RootSystem.Runtime.InteropServices.UnmanagedType.LPWStr)] string guid)
        {
            RootSystem.Action<int> callback = null;
            if(ErrorCallbacks.TryGetValue(guid, out callback))
            {
                Helper.EventPump.Instance.Enqueue(() => callback(err));
            }
            ErrorCallbacks.Remove(guid);
            Microsoft_Kinect_Face_FaceModelData_Delegate_SuccessCallbacks.Remove(guid);
        }

        private static Microsoft_Kinect_Face_FaceModelData_Delegate_Indexed CollectAsyncSuccessDelegate = Microsoft_Kinect_Face_FaceModelData_Delegate_Success;
        private static Error_Delegate_Indexed CollectAsyncFailureDelegate = Error_Delegate_Failure;

        [RootSystem.Runtime.InteropServices.DllImport("KinectFaceUnityAddin", CallingConvention=RootSystem.Runtime.InteropServices.CallingConvention.Cdecl, SetLastError=true)]
        private static extern void Microsoft_Kinect_Face_FaceModelBuilder_CollectFaceDataAsync_Indexed(RootSystem.IntPtr pNative, Microsoft_Kinect_Face_FaceModelData_Delegate_Indexed success, Error_Delegate_Indexed failure, [RootSystem.Runtime.InteropServices.MarshalAs(RootSystem.Runtime.InteropServices.UnmanagedType.LPWStr)] string guid);
        public void CollectFaceDataAsync(RootSystem.Action<Microsoft.Kinect.Face.FaceModelData> success, RootSystem.Action<int> failure)
        {
            if (_pNative == RootSystem.IntPtr.Zero)
            {
                throw new RootSystem.ObjectDisposedException("FaceModelBuilder");
            }

            RootSystem.Guid g = RootSystem.Guid.NewGuid();
            if(success != null)
            {
                Microsoft_Kinect_Face_FaceModelData_Delegate_SuccessCallbacks.Add(g.ToString(), success);
            }
            if(failure != null)
            {
                ErrorCallbacks.Add(g.ToString(), failure);
            }
            Microsoft_Kinect_Face_FaceModelBuilder_CollectFaceDataAsync_Indexed(_pNative, CollectAsyncSuccessDelegate, CollectAsyncFailureDelegate, g.ToString());
            Helper.ExceptionHelper.CheckLastError();
        }
    }
}