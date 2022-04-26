using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Kinect = Windows.Kinect;

public class BodySourceView : MonoBehaviour 
{
    public Material BoneMaterial;
    public GameObject BodySourceManager;
    public GameObject FaceManager;
    public VNectModel VNectModel;

    /// <summary>
    /// Coordinates of joint points
    /// </summary>
    private VNectModel.JointPoint[] jointPoints;
    private Vector3 spineBase;
    private Vector3 spineMid;
    private Vector3 spineShoulder;
    private Vector3 handLeft;
    private Vector3 handRight;
    private Vector3 handTipLeft;
    private Vector3 handTipRight;
    private Dictionary<ulong, GameObject> _Bodies = new Dictionary<ulong, GameObject>();
    private BodySourceManager _BodyManager;
    private FaceManager _FaceManager;
    
    private Dictionary<Kinect.JointType, Kinect.JointType> _BoneMap = new Dictionary<Kinect.JointType, Kinect.JointType>()
    {
        { Kinect.JointType.FootLeft, Kinect.JointType.AnkleLeft },
        { Kinect.JointType.AnkleLeft, Kinect.JointType.KneeLeft },
        { Kinect.JointType.KneeLeft, Kinect.JointType.HipLeft },
        { Kinect.JointType.HipLeft, Kinect.JointType.SpineBase },
        
        { Kinect.JointType.FootRight, Kinect.JointType.AnkleRight },
        { Kinect.JointType.AnkleRight, Kinect.JointType.KneeRight },
        { Kinect.JointType.KneeRight, Kinect.JointType.HipRight },
        { Kinect.JointType.HipRight, Kinect.JointType.SpineBase },
        
        { Kinect.JointType.HandTipLeft, Kinect.JointType.HandLeft },
        { Kinect.JointType.ThumbLeft, Kinect.JointType.HandLeft },
        { Kinect.JointType.HandLeft, Kinect.JointType.WristLeft },
        { Kinect.JointType.WristLeft, Kinect.JointType.ElbowLeft },
        { Kinect.JointType.ElbowLeft, Kinect.JointType.ShoulderLeft },
        { Kinect.JointType.ShoulderLeft, Kinect.JointType.SpineShoulder },
        
        { Kinect.JointType.HandTipRight, Kinect.JointType.HandRight },
        { Kinect.JointType.ThumbRight, Kinect.JointType.HandRight },
        { Kinect.JointType.HandRight, Kinect.JointType.WristRight },
        { Kinect.JointType.WristRight, Kinect.JointType.ElbowRight },
        { Kinect.JointType.ElbowRight, Kinect.JointType.ShoulderRight },
        { Kinect.JointType.ShoulderRight, Kinect.JointType.SpineShoulder },
        
        { Kinect.JointType.SpineBase, Kinect.JointType.SpineMid },
        { Kinect.JointType.SpineMid, Kinect.JointType.SpineShoulder },
        { Kinect.JointType.SpineShoulder, Kinect.JointType.Neck },
        { Kinect.JointType.Neck, Kinect.JointType.Head },
    };
    
    void Start()
    {
        jointPoints = VNectModel.Initialize();
    }
    
    void Update () 
    {
        if (BodySourceManager == null)
        {
            return;
        }

        if (FaceManager == null)
        {
            return;
        }
        
        _BodyManager = BodySourceManager.GetComponent<BodySourceManager>();
        if (_BodyManager == null)
        {
            return;
        }

        _FaceManager = FaceManager.GetComponent<FaceManager>();
        if (_FaceManager == null)
        {
            return;
        }
        
        Kinect.Body[] data = _BodyManager.GetData();
        if (data == null)
        {
            return;
        }
        
        List<ulong> trackedIds = new List<ulong>();
        foreach(var body in data)
        {
            if (body == null)
            {
                continue;
              }
                
            if(body.IsTracked)
            {
                trackedIds.Add (body.TrackingId);
            }
        }
        
        List<ulong> knownIds = new List<ulong>(_Bodies.Keys);
        
        // First delete untracked bodies
        foreach(ulong trackingId in knownIds)
        {
            if(!trackedIds.Contains(trackingId))
            {
                Destroy(_Bodies[trackingId]);
                _Bodies.Remove(trackingId);
            }
        }

        foreach(var body in data)
        {
            if (body == null)
            {
                continue;
            }
            
            if(body.IsTracked)
            {
                if(!_Bodies.ContainsKey(body.TrackingId))
                {
                    _Bodies[body.TrackingId] = CreateBodyObject(body.TrackingId);
                }
                
                RefreshBodyObject(body, _Bodies[body.TrackingId]);

                // Calculations of the missing joints
                jointPoints[PositionIndex.neck.Int()].Pos3D = Vector3.Lerp(spineShoulder, jointPoints[PositionIndex.neck.Int()].Pos3D, 0.5f);
                jointPoints[PositionIndex.hips.Int()].Pos3D = Vector3.Lerp(spineBase, spineMid, 0.25f);
                jointPoints[PositionIndex.chest.Int()].Pos3D = Vector3.Lerp(spineMid, spineShoulder, 0.5f);
                jointPoints[PositionIndex.spine.Int()].Pos3D = Vector3.Lerp(jointPoints[PositionIndex.hips.Int()].Pos3D, spineMid, 0.5f);
                Vector3 lMiddleProximal = Vector3.Lerp(handLeft, handTipLeft, 0.5f);
                Vector3 lThumbMiddleProximal = lMiddleProximal - jointPoints[PositionIndex.lThumb.Int()].Pos3D;
                jointPoints[PositionIndex.lPinky.Int()].Pos3D = jointPoints[PositionIndex.lThumb.Int()].Pos3D + 1.5f * lThumbMiddleProximal;
                Vector3 rMiddleProximal = Vector3.Lerp(handRight, handTipRight, 0.5f);
                Vector3 rThumbMiddleProximal = rMiddleProximal - jointPoints[PositionIndex.rThumb.Int()].Pos3D;
                jointPoints[PositionIndex.rPinky.Int()].Pos3D = jointPoints[PositionIndex.rThumb.Int()].Pos3D + 1.5f * rThumbMiddleProximal;
            }
        }

        // Face points
        jointPoints[PositionIndex.Nose.Int()].Pos3D = _FaceManager.GetFacePointsPosition(0);
        jointPoints[PositionIndex.lEye.Int()].Pos3D = _FaceManager.GetFacePointsPosition(1);
        jointPoints[PositionIndex.rEye.Int()].Pos3D = _FaceManager.GetFacePointsPosition(2);
        jointPoints[PositionIndex.lEar.Int()].Pos3D = _FaceManager.GetFacePointsPosition(3);
        jointPoints[PositionIndex.rEar.Int()].Pos3D = _FaceManager.GetFacePointsPosition(4);
    }
    
    private GameObject CreateBodyObject(ulong id)
    {
        GameObject body = new GameObject("Body:" + id);
        
        for (Kinect.JointType jt = Kinect.JointType.SpineBase; jt <= Kinect.JointType.ThumbRight; jt++)
        {
            GameObject jointObj = GameObject.CreatePrimitive(PrimitiveType.Cube);

            LineRenderer lr = jointObj.AddComponent<LineRenderer>();
            lr.positionCount = 2;
            lr.material = BoneMaterial;
            lr.startWidth = 0.05f;
            lr.endWidth = 0.05f;
            
            jointObj.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
            jointObj.name = jt.ToString();
            jointObj.transform.parent = body.transform;
        }
        
        return body;
    }
    
    private void RefreshBodyObject(Kinect.Body body, GameObject bodyObject)
    {
        for (Kinect.JointType jt = Kinect.JointType.SpineBase; jt <= Kinect.JointType.ThumbRight; jt++)
        {
            Kinect.Joint sourceJoint = body.Joints[jt];
            Kinect.Joint? targetJoint = null;
            
            if(_BoneMap.ContainsKey(jt))
            {
                targetJoint = body.Joints[_BoneMap[jt]];
            }
            
            Transform jointObj = bodyObject.transform.Find(jt.ToString());
            jointObj.localPosition = GetVector3FromJoint(sourceJoint);
            
            // Skeleton visualizer
            LineRenderer lr = jointObj.GetComponent<LineRenderer>();
            if(targetJoint.HasValue)
            {
                lr.SetPosition(0, jointObj.localPosition);
                lr.SetPosition(1, GetVector3FromJoint(targetJoint.Value));
                lr.startColor = GetColorForState (sourceJoint.TrackingState);
                lr.endColor = GetColorForState(targetJoint.Value.TrackingState);
            }
            else
            {
                lr.enabled = false;
            }
            
            // Mapping of Kinect joints to VNectModel joints
            if (jt.ToString().Equals("SpineBase"))
            {
                spineBase = GetVector3FromJoint(sourceJoint);
            }

            if (jt.ToString().Equals("SpineMid"))
            {
                spineMid = GetVector3FromJoint(sourceJoint);
            }

            if (jt.ToString().Equals("Neck"))
            {
                jointPoints[PositionIndex.head.Int()].Pos3D = GetVector3FromJoint(sourceJoint);
            }

            if (jt.ToString().Equals("ShoulderLeft"))
            {
                jointPoints[PositionIndex.lShoulder.Int()].Pos3D = GetVector3FromJoint(sourceJoint);
            }

            if (jt.ToString().Equals("ElbowLeft"))
            {
                jointPoints[PositionIndex.lElbow.Int()].Pos3D = GetVector3FromJoint(sourceJoint);
            }

            if (jt.ToString().Equals("WristLeft"))
            {
                jointPoints[PositionIndex.lWrist.Int()].Pos3D = GetVector3FromJoint(sourceJoint);
            }

            if (jt.ToString().Equals("HandLeft"))
            {
                handLeft = GetVector3FromJoint(sourceJoint);
            }
            
            if (jt.ToString().Equals("ShoulderRight"))
            {
                jointPoints[PositionIndex.rShoulder.Int()].Pos3D = GetVector3FromJoint(sourceJoint);
            }

            if (jt.ToString().Equals("ElbowRight"))
            {
                jointPoints[PositionIndex.rElbow.Int()].Pos3D = GetVector3FromJoint(sourceJoint);
            }

            if (jt.ToString().Equals("WristRight"))
            {
                jointPoints[PositionIndex.rWrist.Int()].Pos3D = GetVector3FromJoint(sourceJoint);
            }

            if (jt.ToString().Equals("HandRight"))
            {
                handRight = GetVector3FromJoint(sourceJoint);
            }

            if (jt.ToString().Equals("HipLeft"))
            {
                jointPoints[PositionIndex.lHip.Int()].Pos3D = GetVector3FromJoint(sourceJoint);
            }

            if (jt.ToString().Equals("KneeLeft"))
            {
                jointPoints[PositionIndex.lKnee.Int()].Pos3D = GetVector3FromJoint(sourceJoint);
            }

            if (jt.ToString().Equals("AnkleLeft"))
            {
                jointPoints[PositionIndex.lAnkle.Int()].Pos3D = GetVector3FromJoint(sourceJoint);
            }

            if (jt.ToString().Equals("FootLeft"))
            {
                jointPoints[PositionIndex.lFootIndex.Int()].Pos3D = GetVector3FromJoint(sourceJoint);
            }

            if (jt.ToString().Equals("HipRight"))
            {
                jointPoints[PositionIndex.rHip.Int()].Pos3D = GetVector3FromJoint(sourceJoint);
            }

            if (jt.ToString().Equals("KneeRight"))
            {
                jointPoints[PositionIndex.rKnee.Int()].Pos3D = GetVector3FromJoint(sourceJoint);
            }

            if (jt.ToString().Equals("AnkleRight"))
            {
                jointPoints[PositionIndex.rAnkle.Int()].Pos3D = GetVector3FromJoint(sourceJoint);
            }

            if (jt.ToString().Equals("FootRight"))
            {
                jointPoints[PositionIndex.rFootIndex.Int()].Pos3D = GetVector3FromJoint(sourceJoint);
            }

            if (jt.ToString().Equals("SpineShoulder"))
            {
                spineShoulder = GetVector3FromJoint(sourceJoint);
            }

            if (jt.ToString().Equals("HandTipLeft"))
            {
                handTipLeft = GetVector3FromJoint(sourceJoint);
            }

            if (jt.ToString().Equals("ThumbLeft"))
            {
                jointPoints[PositionIndex.lThumb.Int()].Pos3D = GetVector3FromJoint(sourceJoint);
            }

            if (jt.ToString().Equals("HandTipRight"))
            {
                handTipRight = GetVector3FromJoint(sourceJoint);
            }

            if (jt.ToString().Equals("ThumbRight"))
            {
                jointPoints[PositionIndex.rThumb.Int()].Pos3D = GetVector3FromJoint(sourceJoint);
            }
        }
    }
    
    private static Color GetColorForState(Kinect.TrackingState state)
    {
        switch (state)
        {
        case Kinect.TrackingState.Tracked:
            return Color.green;

        case Kinect.TrackingState.Inferred:
            return Color.red;

        default:
            return Color.black;
        }
    }
    
    private static Vector3 GetVector3FromJoint(Kinect.Joint joint)
    {
        return new Vector3(joint.Position.X, joint.Position.Y, (joint.Position.Z * -1f) + 2f);
    }
}
