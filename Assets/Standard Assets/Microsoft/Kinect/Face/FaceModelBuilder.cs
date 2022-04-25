using RootSystem = System;
using System.Linq;
using System.Collections.Generic;
namespace Microsoft.Kinect.Face
{
    //
    // Microsoft.Kinect.Face.FaceModelBuilder
    //
    public sealed partial class FaceModelBuilder : RootSystem.IDisposable, Helper.INativeWrapper

    {
        internal RootSystem.IntPtr _pNative;
        RootSystem.IntPtr Helper.INativeWrapper.nativePtr { get { return _pNative; } }

        // Constructors and Finalizers
        internal FaceModelBuilder(RootSystem.IntPtr pNative)
        {
            _pNative = pNative;
            Microsoft_Kinect_Face_FaceModelBuilder_AddRefObject(ref _pNative);
        }

        ~FaceModelBuilder()
        {
            Dispose(false);
        }

        [RootSystem.Runtime.InteropServices.DllImport("KinectFaceUnityAddin", CallingConvention=RootSystem.Runtime.InteropServices.CallingConvention.Cdecl, SetLastError=true)]
        private static extern void Microsoft_Kinect_Face_FaceModelBuilder_ReleaseObject(ref RootSystem.IntPtr pNative);
        [RootSystem.Runtime.InteropServices.DllImport("KinectFaceUnityAddin", CallingConvention=RootSystem.Runtime.InteropServices.CallingConvention.Cdecl, SetLastError=true)]
        private static extern void Microsoft_Kinect_Face_FaceModelBuilder_AddRefObject(ref RootSystem.IntPtr pNative);
        private void Dispose(bool disposing)
        {
            if (_pNative == RootSystem.IntPtr.Zero)
            {
                return;
            }

            __EventCleanup();

            Helper.NativeObjectCache.RemoveObject<FaceModelBuilder>(_pNative);

            if (disposing)
            {
                Microsoft_Kinect_Face_FaceModelBuilder_Dispose(_pNative);
            }
                Microsoft_Kinect_Face_FaceModelBuilder_ReleaseObject(ref _pNative);

            _pNative = RootSystem.IntPtr.Zero;
        }


        // Public Properties
        [RootSystem.Runtime.InteropServices.DllImport("KinectFaceUnityAddin", CallingConvention=RootSystem.Runtime.InteropServices.CallingConvention.Cdecl, SetLastError=true)]
        private static extern Microsoft.Kinect.Face.FaceModelBuilderCaptureStatus Microsoft_Kinect_Face_FaceModelBuilder_get_CaptureStatus(RootSystem.IntPtr pNative);
        public  Microsoft.Kinect.Face.FaceModelBuilderCaptureStatus CaptureStatus
        {
            get
            {
                if (_pNative == RootSystem.IntPtr.Zero)
                {
                    throw new RootSystem.ObjectDisposedException("FaceModelBuilder");
                }

                return Microsoft_Kinect_Face_FaceModelBuilder_get_CaptureStatus(_pNative);
            }
        }

        [RootSystem.Runtime.InteropServices.DllImport("KinectFaceUnityAddin", CallingConvention=RootSystem.Runtime.InteropServices.CallingConvention.Cdecl, SetLastError=true)]
        private static extern Microsoft.Kinect.Face.FaceModelBuilderCollectionStatus Microsoft_Kinect_Face_FaceModelBuilder_get_CollectionStatus(RootSystem.IntPtr pNative);
        public  Microsoft.Kinect.Face.FaceModelBuilderCollectionStatus CollectionStatus
        {
            get
            {
                if (_pNative == RootSystem.IntPtr.Zero)
                {
                    throw new RootSystem.ObjectDisposedException("FaceModelBuilder");
                }

                return Microsoft_Kinect_Face_FaceModelBuilder_get_CollectionStatus(_pNative);
            }
        }


        // Events
        private static RootSystem.Runtime.InteropServices.GCHandle _Microsoft_Kinect_Face_CaptureStatusChangedEventArgs_Delegate_Handle;
        [RootSystem.Runtime.InteropServices.UnmanagedFunctionPointer(RootSystem.Runtime.InteropServices.CallingConvention.Cdecl)]
        private delegate void _Microsoft_Kinect_Face_CaptureStatusChangedEventArgs_Delegate(RootSystem.IntPtr args, RootSystem.IntPtr pNative);
        private static Helper.CollectionMap<RootSystem.IntPtr, List<RootSystem.EventHandler<Microsoft.Kinect.Face.CaptureStatusChangedEventArgs>>> Microsoft_Kinect_Face_CaptureStatusChangedEventArgs_Delegate_callbacks = new Helper.CollectionMap<RootSystem.IntPtr, List<RootSystem.EventHandler<Microsoft.Kinect.Face.CaptureStatusChangedEventArgs>>>();
        [AOT.MonoPInvokeCallbackAttribute(typeof(_Microsoft_Kinect_Face_CaptureStatusChangedEventArgs_Delegate))]
        private static void Microsoft_Kinect_Face_CaptureStatusChangedEventArgs_Delegate_Handler(RootSystem.IntPtr result, RootSystem.IntPtr pNative)
        {
            List<RootSystem.EventHandler<Microsoft.Kinect.Face.CaptureStatusChangedEventArgs>> callbackList = null;
            Microsoft_Kinect_Face_CaptureStatusChangedEventArgs_Delegate_callbacks.TryGetValue(pNative, out callbackList);
            lock(callbackList)
            {
                var objThis = Helper.NativeObjectCache.GetObject<FaceModelBuilder>(pNative);
                var args = new Microsoft.Kinect.Face.CaptureStatusChangedEventArgs(result);
                foreach(var func in callbackList)
                {
                    Helper.EventPump.Instance.Enqueue(() => { try { func(objThis, args); } catch { } });
                }
            }
        }
        [RootSystem.Runtime.InteropServices.DllImport("KinectFaceUnityAddin", CallingConvention=RootSystem.Runtime.InteropServices.CallingConvention.Cdecl, SetLastError=true)]
        private static extern void Microsoft_Kinect_Face_FaceModelBuilder_add_CaptureStatusChanged(RootSystem.IntPtr pNative, _Microsoft_Kinect_Face_CaptureStatusChangedEventArgs_Delegate eventCallback, bool unsubscribe);
        public  event RootSystem.EventHandler<Microsoft.Kinect.Face.CaptureStatusChangedEventArgs> CaptureStatusChanged
        {
            add
            {
                Helper.EventPump.EnsureInitialized();

                Microsoft_Kinect_Face_CaptureStatusChangedEventArgs_Delegate_callbacks.TryAddDefault(_pNative);
                var callbackList = Microsoft_Kinect_Face_CaptureStatusChangedEventArgs_Delegate_callbacks[_pNative];
                lock (callbackList)
                {
                    callbackList.Add(value);
                    if(callbackList.Count == 1)
                    {
                        var del = new _Microsoft_Kinect_Face_CaptureStatusChangedEventArgs_Delegate(Microsoft_Kinect_Face_CaptureStatusChangedEventArgs_Delegate_Handler);
                        _Microsoft_Kinect_Face_CaptureStatusChangedEventArgs_Delegate_Handle = RootSystem.Runtime.InteropServices.GCHandle.Alloc(del);
                        Microsoft_Kinect_Face_FaceModelBuilder_add_CaptureStatusChanged(_pNative, del, false);
                    }
                }
            }
            remove
            {
                if (_pNative == RootSystem.IntPtr.Zero)
                {
                    return;
                }

                Microsoft_Kinect_Face_CaptureStatusChangedEventArgs_Delegate_callbacks.TryAddDefault(_pNative);
                var callbackList = Microsoft_Kinect_Face_CaptureStatusChangedEventArgs_Delegate_callbacks[_pNative];
                lock (callbackList)
                {
                    callbackList.Remove(value);
                    if(callbackList.Count == 0)
                    {
                        Microsoft_Kinect_Face_FaceModelBuilder_add_CaptureStatusChanged(_pNative, Microsoft_Kinect_Face_CaptureStatusChangedEventArgs_Delegate_Handler, true);
                        _Microsoft_Kinect_Face_CaptureStatusChangedEventArgs_Delegate_Handle.Free();
                    }
                }
            }
        }

        private static RootSystem.Runtime.InteropServices.GCHandle _Microsoft_Kinect_Face_CollectionStatusChangedEventArgs_Delegate_Handle;
        [RootSystem.Runtime.InteropServices.UnmanagedFunctionPointer(RootSystem.Runtime.InteropServices.CallingConvention.Cdecl)]
        private delegate void _Microsoft_Kinect_Face_CollectionStatusChangedEventArgs_Delegate(RootSystem.IntPtr args, RootSystem.IntPtr pNative);
        private static Helper.CollectionMap<RootSystem.IntPtr, List<RootSystem.EventHandler<Microsoft.Kinect.Face.CollectionStatusChangedEventArgs>>> Microsoft_Kinect_Face_CollectionStatusChangedEventArgs_Delegate_callbacks = new Helper.CollectionMap<RootSystem.IntPtr, List<RootSystem.EventHandler<Microsoft.Kinect.Face.CollectionStatusChangedEventArgs>>>();
        [AOT.MonoPInvokeCallbackAttribute(typeof(_Microsoft_Kinect_Face_CollectionStatusChangedEventArgs_Delegate))]
        private static void Microsoft_Kinect_Face_CollectionStatusChangedEventArgs_Delegate_Handler(RootSystem.IntPtr result, RootSystem.IntPtr pNative)
        {
            List<RootSystem.EventHandler<Microsoft.Kinect.Face.CollectionStatusChangedEventArgs>> callbackList = null;
            Microsoft_Kinect_Face_CollectionStatusChangedEventArgs_Delegate_callbacks.TryGetValue(pNative, out callbackList);
            lock(callbackList)
            {
                var objThis = Helper.NativeObjectCache.GetObject<FaceModelBuilder>(pNative);
                var args = new Microsoft.Kinect.Face.CollectionStatusChangedEventArgs(result);
                foreach(var func in callbackList)
                {
                    Helper.EventPump.Instance.Enqueue(() => { try { func(objThis, args); } catch { } });
                }
            }
        }
        [RootSystem.Runtime.InteropServices.DllImport("KinectFaceUnityAddin", CallingConvention=RootSystem.Runtime.InteropServices.CallingConvention.Cdecl, SetLastError=true)]
        private static extern void Microsoft_Kinect_Face_FaceModelBuilder_add_CollectionStatusChanged(RootSystem.IntPtr pNative, _Microsoft_Kinect_Face_CollectionStatusChangedEventArgs_Delegate eventCallback, bool unsubscribe);
        public  event RootSystem.EventHandler<Microsoft.Kinect.Face.CollectionStatusChangedEventArgs> CollectionStatusChanged
        {
            add
            {
                Helper.EventPump.EnsureInitialized();

                Microsoft_Kinect_Face_CollectionStatusChangedEventArgs_Delegate_callbacks.TryAddDefault(_pNative);
                var callbackList = Microsoft_Kinect_Face_CollectionStatusChangedEventArgs_Delegate_callbacks[_pNative];
                lock (callbackList)
                {
                    callbackList.Add(value);
                    if(callbackList.Count == 1)
                    {
                        var del = new _Microsoft_Kinect_Face_CollectionStatusChangedEventArgs_Delegate(Microsoft_Kinect_Face_CollectionStatusChangedEventArgs_Delegate_Handler);
                        _Microsoft_Kinect_Face_CollectionStatusChangedEventArgs_Delegate_Handle = RootSystem.Runtime.InteropServices.GCHandle.Alloc(del);
                        Microsoft_Kinect_Face_FaceModelBuilder_add_CollectionStatusChanged(_pNative, del, false);
                    }
                }
            }
            remove
            {
                if (_pNative == RootSystem.IntPtr.Zero)
                {
                    return;
                }

                Microsoft_Kinect_Face_CollectionStatusChangedEventArgs_Delegate_callbacks.TryAddDefault(_pNative);
                var callbackList = Microsoft_Kinect_Face_CollectionStatusChangedEventArgs_Delegate_callbacks[_pNative];
                lock (callbackList)
                {
                    callbackList.Remove(value);
                    if(callbackList.Count == 0)
                    {
                        Microsoft_Kinect_Face_FaceModelBuilder_add_CollectionStatusChanged(_pNative, Microsoft_Kinect_Face_CollectionStatusChangedEventArgs_Delegate_Handler, true);
                        _Microsoft_Kinect_Face_CollectionStatusChangedEventArgs_Delegate_Handle.Free();
                    }
                }
            }
        }

        private static RootSystem.Runtime.InteropServices.GCHandle _Windows_Data_PropertyChangedEventArgs_Delegate_Handle;
        [RootSystem.Runtime.InteropServices.UnmanagedFunctionPointer(RootSystem.Runtime.InteropServices.CallingConvention.Cdecl)]
        private delegate void _Windows_Data_PropertyChangedEventArgs_Delegate(RootSystem.IntPtr args, RootSystem.IntPtr pNative);
        private static Helper.CollectionMap<RootSystem.IntPtr, List<RootSystem.EventHandler<Windows.Data.PropertyChangedEventArgs>>> Windows_Data_PropertyChangedEventArgs_Delegate_callbacks = new Helper.CollectionMap<RootSystem.IntPtr, List<RootSystem.EventHandler<Windows.Data.PropertyChangedEventArgs>>>();
        [AOT.MonoPInvokeCallbackAttribute(typeof(_Windows_Data_PropertyChangedEventArgs_Delegate))]
        private static void Windows_Data_PropertyChangedEventArgs_Delegate_Handler(RootSystem.IntPtr result, RootSystem.IntPtr pNative)
        {
            List<RootSystem.EventHandler<Windows.Data.PropertyChangedEventArgs>> callbackList = null;
            Windows_Data_PropertyChangedEventArgs_Delegate_callbacks.TryGetValue(pNative, out callbackList);
            lock(callbackList)
            {
                var objThis = Helper.NativeObjectCache.GetObject<FaceModelBuilder>(pNative);
                var args = new Windows.Data.PropertyChangedEventArgs(result);
                foreach(var func in callbackList)
                {
                    Helper.EventPump.Instance.Enqueue(() => { try { func(objThis, args); } catch { } });
                }
            }
        }
        [RootSystem.Runtime.InteropServices.DllImport("KinectFaceUnityAddin", CallingConvention=RootSystem.Runtime.InteropServices.CallingConvention.Cdecl, SetLastError=true)]
        private static extern void Microsoft_Kinect_Face_FaceModelBuilder_add_PropertyChanged(RootSystem.IntPtr pNative, _Windows_Data_PropertyChangedEventArgs_Delegate eventCallback, bool unsubscribe);
        public  event RootSystem.EventHandler<Windows.Data.PropertyChangedEventArgs> PropertyChanged
        {
            add
            {
                Helper.EventPump.EnsureInitialized();

                Windows_Data_PropertyChangedEventArgs_Delegate_callbacks.TryAddDefault(_pNative);
                var callbackList = Windows_Data_PropertyChangedEventArgs_Delegate_callbacks[_pNative];
                lock (callbackList)
                {
                    callbackList.Add(value);
                    if(callbackList.Count == 1)
                    {
                        var del = new _Windows_Data_PropertyChangedEventArgs_Delegate(Windows_Data_PropertyChangedEventArgs_Delegate_Handler);
                        _Windows_Data_PropertyChangedEventArgs_Delegate_Handle = RootSystem.Runtime.InteropServices.GCHandle.Alloc(del);
                        Microsoft_Kinect_Face_FaceModelBuilder_add_PropertyChanged(_pNative, del, false);
                    }
                }
            }
            remove
            {
                if (_pNative == RootSystem.IntPtr.Zero)
                {
                    return;
                }

                Windows_Data_PropertyChangedEventArgs_Delegate_callbacks.TryAddDefault(_pNative);
                var callbackList = Windows_Data_PropertyChangedEventArgs_Delegate_callbacks[_pNative];
                lock (callbackList)
                {
                    callbackList.Remove(value);
                    if(callbackList.Count == 0)
                    {
                        Microsoft_Kinect_Face_FaceModelBuilder_add_PropertyChanged(_pNative, Windows_Data_PropertyChangedEventArgs_Delegate_Handler, true);
                        _Windows_Data_PropertyChangedEventArgs_Delegate_Handle.Free();
                    }
                }
            }
        }


        // Public Methods
        [RootSystem.Runtime.InteropServices.DllImport("KinectFaceUnityAddin", CallingConvention=RootSystem.Runtime.InteropServices.CallingConvention.Cdecl, SetLastError=true)]
        private static extern void Microsoft_Kinect_Face_FaceModelBuilder_Dispose(RootSystem.IntPtr pNative);
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
            {
                Microsoft_Kinect_Face_CaptureStatusChangedEventArgs_Delegate_callbacks.TryAddDefault(_pNative);
                var callbackList = Microsoft_Kinect_Face_CaptureStatusChangedEventArgs_Delegate_callbacks[_pNative];
                lock (callbackList)
                {
                    if (callbackList.Count > 0)
                    {
                        callbackList.Clear();
                        if (_pNative != RootSystem.IntPtr.Zero)
                        {
                            Microsoft_Kinect_Face_FaceModelBuilder_add_CaptureStatusChanged(_pNative, Microsoft_Kinect_Face_CaptureStatusChangedEventArgs_Delegate_Handler, true);
                        }
                        _Microsoft_Kinect_Face_CaptureStatusChangedEventArgs_Delegate_Handle.Free();
                    }
                }
            }
            {
                Microsoft_Kinect_Face_CollectionStatusChangedEventArgs_Delegate_callbacks.TryAddDefault(_pNative);
                var callbackList = Microsoft_Kinect_Face_CollectionStatusChangedEventArgs_Delegate_callbacks[_pNative];
                lock (callbackList)
                {
                    if (callbackList.Count > 0)
                    {
                        callbackList.Clear();
                        if (_pNative != RootSystem.IntPtr.Zero)
                        {
                            Microsoft_Kinect_Face_FaceModelBuilder_add_CollectionStatusChanged(_pNative, Microsoft_Kinect_Face_CollectionStatusChangedEventArgs_Delegate_Handler, true);
                        }
                        _Microsoft_Kinect_Face_CollectionStatusChangedEventArgs_Delegate_Handle.Free();
                    }
                }
            }
            {
                Windows_Data_PropertyChangedEventArgs_Delegate_callbacks.TryAddDefault(_pNative);
                var callbackList = Windows_Data_PropertyChangedEventArgs_Delegate_callbacks[_pNative];
                lock (callbackList)
                {
                    if (callbackList.Count > 0)
                    {
                        callbackList.Clear();
                        if (_pNative != RootSystem.IntPtr.Zero)
                        {
                            Microsoft_Kinect_Face_FaceModelBuilder_add_PropertyChanged(_pNative, Windows_Data_PropertyChangedEventArgs_Delegate_Handler, true);
                        }
                        _Windows_Data_PropertyChangedEventArgs_Delegate_Handle.Free();
                    }
                }
            }
        }
    }

}
