﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// Position index of joint point
/// </summary>
public enum PositionIndex : int
{
    Nose = 0,
    lEyeInner,
    lEye,
    lEyeOuter,
    rEyeInner,
    rEye,
    rEyeOuter,
    lEar,
    rEar,
    mouthL,
    mouthR,
    lShoulder,
    rShoulder,
    lElbow,
    rElbow,
    lWrist,
    rWrist,
    lPinky,
    rPinky,
    lIndex,
    rIndex,
    lThumb,
    rThumb,
    lHip,
    rHip,
    lKnee,
    rKnee,
    lAnkle,
    rAnkle,
    lHeel,
    rHeel,
    lFootIndex,
    rFootIndex,
    humanVisible,

    //Calculated coordinates
    head,
    neck,
    chest,
    spine,
    hips,
    lController,
    rController,
    lPhantomElbow,
    rPhantomElbow,
    centerHead,
    phantomNose,

    Count,
    None,
}

public static partial class EnumExtend
{
    public static int Int(this PositionIndex i)
    {
        return (int)i;
    }
}

public class VNectModel : MonoBehaviour
{

    public class JointPoint
    {
        public Vector3 Pos3D = new Vector3();
        public float score3D;

        // Bones
        public Transform Transform = null;
        public Quaternion InitRotation;
        public Quaternion Inverse;
        public Quaternion InverseRotation;

        public JointPoint Child = null;
        public JointPoint Parent = null;
    }

    public class Skeleton
    {
        public GameObject LineObject;
        public LineRenderer Line;
        public JointPoint start = null;
        public JointPoint end = null;
    }

    private List<Skeleton> Skeletons = new List<Skeleton>();
    public Material SkeletonMaterial;
    public bool ShowSkeleton;
    private bool useSkeleton;
    public float SkeletonX;
    public float SkeletonY;
    public float SkeletonZ;
    public float SkeletonScale;

    // Joint position and bone
    private JointPoint[] jointPoints;
    public JointPoint[] JointPoints { get { return jointPoints; } }
    private Vector3 initPosition; // Initial center position
    private Vector3 jointPositionOffset = Vector3.zero;

    // Avatar
    public GameObject ModelObject;
    public GameObject Nose;
    private Animator anim;
    private Vector3 avatarDimensions;
    private Vector3 avatarCenter;

    // HMD
    private InputDevice hmdDevice;
    private InputDevice leftController;
    private InputDevice rightController;
    private bool lastPrimaryButtonValue = false;
    private bool vrRunning = false;
    private Vector3 hmdPosition;
    private Vector3 leftControllerPosition;
    private Vector3 rightControllerPosition;
    private Quaternion hmdRotation;
    private Quaternion leftControllerRotation;
    private Quaternion rightControllerRotation;

    private string sceneName;
    private bool kinectScene = false;
    public PoseVisualizer3D PoseVisualizer3D;
    public FaceManager FaceManager;
    public BodySourceView BodySourceView;
    public GameObject Instruction;
    private bool displayText = false;


    void Awake()
    {
        sceneName = SceneManager.GetActiveScene().name;
        
        if (sceneName == "KinectScene")
            kinectScene = true;

        Instruction.SetActive(displayText);
    }

    private void Update()
    {
        if (kinectScene)
        {
            if (!vrRunning)
            {
		        // Head rotation
                jointPoints[PositionIndex.head.Int()].Transform.rotation = FaceManager.GetFaceRotation();
            }
            else
            {
                // Phantom nose position
                jointPoints[PositionIndex.phantomNose.Int()].Pos3D = new Vector3(jointPoints[PositionIndex.centerHead.Int()].Pos3D.x, jointPoints[PositionIndex.centerHead.Int()].Pos3D.y, jointPoints[PositionIndex.centerHead.Int()].Pos3D.z + 0.1f);
            }
        }
        
        if (hmdDevice.isValid)
        {
            hmdDevice.TryGetFeatureValue(CommonUsages.devicePosition, out hmdPosition);
            hmdDevice.TryGetFeatureValue(CommonUsages.deviceRotation, out hmdRotation);
        }
        if (leftController.isValid)
        {
            leftController.TryGetFeatureValue(CommonUsages.devicePosition, out leftControllerPosition);
            leftController.TryGetFeatureValue(CommonUsages.deviceRotation, out leftControllerRotation);
        }
        if (rightController.isValid)
        {
			rightController.TryGetFeatureValue(CommonUsages.devicePosition, out rightControllerPosition);
            rightController.TryGetFeatureValue(CommonUsages.deviceRotation, out rightControllerRotation);
        }

        if(vrRunning)
        {
            // Head rotation
            jointPoints[PositionIndex.head.Int()].Transform.rotation = hmdRotation;
            // Wrists positions
            if (!kinectScene)
            {
                jointPoints[PositionIndex.lController.Int()].Pos3D = leftControllerPosition - hmdPosition + jointPoints[PositionIndex.Nose.Int()].Pos3D;
                jointPoints[PositionIndex.rController.Int()].Pos3D = rightControllerPosition - hmdPosition + jointPoints[PositionIndex.Nose.Int()].Pos3D;
            }
            else
            {
                jointPoints[PositionIndex.lController.Int()].Pos3D = leftControllerPosition - hmdPosition + jointPoints[PositionIndex.phantomNose.Int()].Pos3D;
                jointPoints[PositionIndex.rController.Int()].Pos3D = rightControllerPosition - hmdPosition + jointPoints[PositionIndex.phantomNose.Int()].Pos3D;
            }
            // Wrists rotations
            jointPoints[PositionIndex.lController.Int()].Transform.rotation = leftControllerRotation * Quaternion.Euler(new Vector3(-180f, -90f, -80f));
            jointPoints[PositionIndex.rController.Int()].Transform.rotation = rightControllerRotation * Quaternion.Euler(new Vector3(0f, 90f, -80f));
            // Left elbow position
            Vector3 lElbowProjection = Vector3.Project((jointPoints[PositionIndex.lElbow.Int()].Pos3D - jointPoints[PositionIndex.lShoulder.Int()].Pos3D),
            (jointPoints[PositionIndex.lController.Int()].Pos3D - jointPoints[PositionIndex.lShoulder.Int()].Pos3D)) + jointPoints[PositionIndex.lShoulder.Int()].Pos3D;
            Vector3 lElbowProjectionElbow = jointPoints[PositionIndex.lElbow.Int()].Pos3D - lElbowProjection;
            Vector3 lPhantomElbowProjection = Vector3.Lerp(jointPoints[PositionIndex.lShoulder.Int()].Pos3D, jointPoints[PositionIndex.lController.Int()].Pos3D, 0.5f);
            jointPoints[PositionIndex.lPhantomElbow.Int()].Pos3D = lPhantomElbowProjection + lElbowProjectionElbow;
            // Right elbow position
            Vector3 rElbowProjection = Vector3.Project((jointPoints[PositionIndex.rElbow.Int()].Pos3D - jointPoints[PositionIndex.rShoulder.Int()].Pos3D),
            (jointPoints[PositionIndex.rController.Int()].Pos3D - jointPoints[PositionIndex.rShoulder.Int()].Pos3D)) + jointPoints[PositionIndex.rShoulder.Int()].Pos3D;
            Vector3 rElbowProjectionElbow = jointPoints[PositionIndex.rElbow.Int()].Pos3D - rElbowProjection;
            Vector3 rPhantomElbowProjection = Vector3.Lerp(jointPoints[PositionIndex.rShoulder.Int()].Pos3D, jointPoints[PositionIndex.rController.Int()].Pos3D, 0.5f);
            jointPoints[PositionIndex.rPhantomElbow.Int()].Pos3D = rPhantomElbowProjection + rElbowProjectionElbow;
        }

        // Primary button on left controller triggers calibration
        bool primaryButtonValue = false;
        if (UnityEngine.InputSystem.Keyboard.current.spaceKey.wasPressedThisFrame || (leftController.TryGetFeatureValue(CommonUsages.primaryButton, out primaryButtonValue) && primaryButtonValue != lastPrimaryButtonValue && primaryButtonValue))
            RunCalibration();
        lastPrimaryButtonValue = primaryButtonValue;

        if (jointPoints != null)
            PoseUpdate();
    }

    /// <summary>
    /// Initialize joint points
    /// </summary>
    /// <returns></returns>
    public JointPoint[] Initialize()
    {
        vrRunning = isVrRunning();
        hmdDevice = InputDevices.GetDeviceAtXRNode(XRNode.CenterEye);
        leftController = InputDevices.GetDeviceAtXRNode(XRNode.LeftHand);
        rightController = InputDevices.GetDeviceAtXRNode(XRNode.RightHand);

        jointPoints = new JointPoint[PositionIndex.Count.Int()];
        for (var i = 0; i < PositionIndex.Count.Int(); i++)
            jointPoints[i] = new JointPoint();

        anim = ModelObject.GetComponent<Animator>();

        avatarDimensions.x = Vector3.Distance(anim.GetBoneTransform(HumanBodyBones.RightHand).position, anim.GetBoneTransform(HumanBodyBones.LeftHand).position);
        avatarDimensions.y = Nose.transform.position.y;
        avatarCenter = GetCenter(gameObject);

        // Right Arm
        jointPoints[PositionIndex.rShoulder.Int()].Transform = anim.GetBoneTransform(HumanBodyBones.RightUpperArm);
        if(!vrRunning)
        {
            jointPoints[PositionIndex.rElbow.Int()].Transform = anim.GetBoneTransform(HumanBodyBones.RightLowerArm);
            jointPoints[PositionIndex.rWrist.Int()].Transform = anim.GetBoneTransform(HumanBodyBones.RightHand);
            jointPoints[PositionIndex.rThumb.Int()].Transform = anim.GetBoneTransform(HumanBodyBones.RightThumbIntermediate);
            jointPoints[PositionIndex.rPinky.Int()].Transform = anim.GetBoneTransform(HumanBodyBones.RightLittleIntermediate);
        }
        else
        {
            jointPoints[PositionIndex.rPhantomElbow.Int()].Transform = anim.GetBoneTransform(HumanBodyBones.RightLowerArm);
            jointPoints[PositionIndex.rController.Int()].Transform = anim.GetBoneTransform(HumanBodyBones.RightHand);
        }

        // Left Arm
        jointPoints[PositionIndex.lShoulder.Int()].Transform = anim.GetBoneTransform(HumanBodyBones.LeftUpperArm);
        if(!vrRunning)
        {
            jointPoints[PositionIndex.lElbow.Int()].Transform = anim.GetBoneTransform(HumanBodyBones.LeftLowerArm);
            jointPoints[PositionIndex.lWrist.Int()].Transform = anim.GetBoneTransform(HumanBodyBones.LeftHand);
            jointPoints[PositionIndex.lThumb.Int()].Transform = anim.GetBoneTransform(HumanBodyBones.LeftThumbIntermediate);
            jointPoints[PositionIndex.lPinky.Int()].Transform = anim.GetBoneTransform(HumanBodyBones.LeftLittleIntermediate); 
        }
        else
        {
            jointPoints[PositionIndex.lPhantomElbow.Int()].Transform = anim.GetBoneTransform(HumanBodyBones.LeftLowerArm);
            jointPoints[PositionIndex.lController.Int()].Transform = anim.GetBoneTransform(HumanBodyBones.LeftHand);
        }

        // Head
        if (kinectScene && vrRunning)
            jointPoints[PositionIndex.phantomNose.Int()].Transform = Nose.transform;        
        else
        {
            jointPoints[PositionIndex.lEar.Int()].Transform = anim.GetBoneTransform(HumanBodyBones.Head);
            jointPoints[PositionIndex.lEye.Int()].Transform = anim.GetBoneTransform(HumanBodyBones.LeftEye);
            jointPoints[PositionIndex.rEar.Int()].Transform = anim.GetBoneTransform(HumanBodyBones.Head);
            jointPoints[PositionIndex.rEye.Int()].Transform = anim.GetBoneTransform(HumanBodyBones.RightEye);
            jointPoints[PositionIndex.Nose.Int()].Transform = Nose.transform;
        }

        // Right Leg
        jointPoints[PositionIndex.rHip.Int()].Transform = anim.GetBoneTransform(HumanBodyBones.RightUpperLeg);
        jointPoints[PositionIndex.rKnee.Int()].Transform = anim.GetBoneTransform(HumanBodyBones.RightLowerLeg);
        jointPoints[PositionIndex.rAnkle.Int()].Transform = anim.GetBoneTransform(HumanBodyBones.RightFoot);
        jointPoints[PositionIndex.rFootIndex.Int()].Transform = anim.GetBoneTransform(HumanBodyBones.RightToes);
        // Left Leg
        jointPoints[PositionIndex.lHip.Int()].Transform = anim.GetBoneTransform(HumanBodyBones.LeftUpperLeg);
        jointPoints[PositionIndex.lKnee.Int()].Transform = anim.GetBoneTransform(HumanBodyBones.LeftLowerLeg);
        jointPoints[PositionIndex.lAnkle.Int()].Transform = anim.GetBoneTransform(HumanBodyBones.LeftFoot);
        jointPoints[PositionIndex.lFootIndex.Int()].Transform = anim.GetBoneTransform(HumanBodyBones.LeftToes);

        // Spine
        jointPoints[PositionIndex.head.Int()].Transform = anim.GetBoneTransform(HumanBodyBones.Head);
        jointPoints[PositionIndex.neck.Int()].Transform = anim.GetBoneTransform(HumanBodyBones.Neck);
        jointPoints[PositionIndex.chest.Int()].Transform = anim.GetBoneTransform(HumanBodyBones.Chest);
        jointPoints[PositionIndex.spine.Int()].Transform = anim.GetBoneTransform(HumanBodyBones.Spine);
        jointPoints[PositionIndex.hips.Int()].Transform = anim.GetBoneTransform(HumanBodyBones.Hips);

        // Parent-Child Setup
        // Right Arm
        if(!vrRunning)
        {
            jointPoints[PositionIndex.rShoulder.Int()].Child = jointPoints[PositionIndex.rElbow.Int()];
            jointPoints[PositionIndex.rElbow.Int()].Child = jointPoints[PositionIndex.rWrist.Int()];
            jointPoints[PositionIndex.rElbow.Int()].Parent = jointPoints[PositionIndex.rShoulder.Int()];
        }
        else
        {
            jointPoints[PositionIndex.rShoulder.Int()].Child = jointPoints[PositionIndex.rPhantomElbow.Int()];
            jointPoints[PositionIndex.rPhantomElbow.Int()].Child = jointPoints[PositionIndex.rController.Int()];
            jointPoints[PositionIndex.rPhantomElbow.Int()].Parent = jointPoints[PositionIndex.rShoulder.Int()];
        }

        // Left Arm
        if(!vrRunning)
        {
            jointPoints[PositionIndex.lShoulder.Int()].Child = jointPoints[PositionIndex.lElbow.Int()];
            jointPoints[PositionIndex.lElbow.Int()].Child = jointPoints[PositionIndex.lWrist.Int()];
            jointPoints[PositionIndex.lElbow.Int()].Parent = jointPoints[PositionIndex.lShoulder.Int()];
        }
        else
        {
            jointPoints[PositionIndex.lShoulder.Int()].Child = jointPoints[PositionIndex.lPhantomElbow.Int()];
            jointPoints[PositionIndex.lPhantomElbow.Int()].Child = jointPoints[PositionIndex.lController.Int()];
            jointPoints[PositionIndex.lPhantomElbow.Int()].Parent = jointPoints[PositionIndex.lShoulder.Int()];
        }

        // Right Leg
        jointPoints[PositionIndex.rHip.Int()].Child = jointPoints[PositionIndex.rKnee.Int()];
        jointPoints[PositionIndex.rKnee.Int()].Child = jointPoints[PositionIndex.rAnkle.Int()];
        jointPoints[PositionIndex.rAnkle.Int()].Child = jointPoints[PositionIndex.rFootIndex.Int()];
        jointPoints[PositionIndex.rAnkle.Int()].Parent = jointPoints[PositionIndex.rKnee.Int()];
        // Left Leg
        jointPoints[PositionIndex.lHip.Int()].Child = jointPoints[PositionIndex.lKnee.Int()];
        jointPoints[PositionIndex.lKnee.Int()].Child = jointPoints[PositionIndex.lAnkle.Int()];
        jointPoints[PositionIndex.lAnkle.Int()].Child = jointPoints[PositionIndex.lFootIndex.Int()];
        jointPoints[PositionIndex.lAnkle.Int()].Parent = jointPoints[PositionIndex.lKnee.Int()];

        // Spine
        jointPoints[PositionIndex.spine.Int()].Child = jointPoints[PositionIndex.chest.Int()];
        jointPoints[PositionIndex.chest.Int()].Child = jointPoints[PositionIndex.neck.Int()];
        jointPoints[PositionIndex.neck.Int()].Child = jointPoints[PositionIndex.head.Int()];

        useSkeleton = ShowSkeleton;
        if (useSkeleton)
        {
            // Skeleton Lines
            // Right Arm
            if(!vrRunning)
            {
                AddSkeleton(PositionIndex.rShoulder, PositionIndex.rElbow);
                AddSkeleton(PositionIndex.rElbow, PositionIndex.rWrist);
                AddSkeleton(PositionIndex.rWrist, PositionIndex.rThumb);
                AddSkeleton(PositionIndex.rWrist, PositionIndex.rPinky);
            }
            else
            {
                AddSkeleton(PositionIndex.rShoulder, PositionIndex.rPhantomElbow);
                AddSkeleton(PositionIndex.rPhantomElbow, PositionIndex.rController);
            }

            // Left Arm
            if(!vrRunning)
            {
                AddSkeleton(PositionIndex.lShoulder, PositionIndex.lElbow);
                AddSkeleton(PositionIndex.lElbow, PositionIndex.lWrist);
                AddSkeleton(PositionIndex.lWrist, PositionIndex.lThumb);
                AddSkeleton(PositionIndex.lWrist, PositionIndex.lPinky);
            }
            else
            {
                AddSkeleton(PositionIndex.lShoulder, PositionIndex.lPhantomElbow);
                AddSkeleton(PositionIndex.lPhantomElbow, PositionIndex.lController);
            }

            // Head
            if (kinectScene && vrRunning)
                AddSkeleton(PositionIndex.centerHead, PositionIndex.phantomNose);
            else
            {
                AddSkeleton(PositionIndex.lEar, PositionIndex.lEye);
                AddSkeleton(PositionIndex.lEye, PositionIndex.Nose);
                AddSkeleton(PositionIndex.rEar, PositionIndex.rEye);
                AddSkeleton(PositionIndex.rEye, PositionIndex.Nose);
            }

            // Right Leg
            AddSkeleton(PositionIndex.rHip, PositionIndex.rKnee);
            AddSkeleton(PositionIndex.rKnee, PositionIndex.rAnkle);
            AddSkeleton(PositionIndex.rAnkle, PositionIndex.rFootIndex);
            // Left Leg
            AddSkeleton(PositionIndex.lHip, PositionIndex.lKnee);
            AddSkeleton(PositionIndex.lKnee, PositionIndex.lAnkle);
            AddSkeleton(PositionIndex.lAnkle, PositionIndex.lFootIndex);

            // Torso
            AddSkeleton(PositionIndex.hips, PositionIndex.spine);
            AddSkeleton(PositionIndex.spine, PositionIndex.chest);
            AddSkeleton(PositionIndex.chest, PositionIndex.neck);
            AddSkeleton(PositionIndex.neck, PositionIndex.head);
            AddSkeleton(PositionIndex.chest, PositionIndex.rShoulder);
            AddSkeleton(PositionIndex.chest, PositionIndex.lShoulder);
            AddSkeleton(PositionIndex.hips, PositionIndex.rHip);
            AddSkeleton(PositionIndex.hips, PositionIndex.lHip);
        }

        // Set Inverse
        var forward = TriangleNormal(jointPoints[PositionIndex.hips.Int()].Transform.position, jointPoints[PositionIndex.lHip.Int()].Transform.position, jointPoints[PositionIndex.rHip.Int()].Transform.position);
        foreach (var jointPoint in jointPoints)
        {
            if (jointPoint != null)
            {
                if (jointPoint.Transform != null)
                {
                    jointPoint.InitRotation = jointPoint.Transform.rotation;
                }
                if (jointPoint.Child != null && jointPoint.Child.Transform != null && jointPoint.Child.Transform.position != null)
                {
                    jointPoint.Inverse = GetInverse(jointPoint, jointPoint.Child, forward);
                    jointPoint.InverseRotation = jointPoint.Inverse * jointPoint.InitRotation;
                }  
            }
        }
        
        // Hips Rotation
        var hips = jointPoints[PositionIndex.hips.Int()];
        initPosition = jointPoints[PositionIndex.hips.Int()].Transform.position;
        hips.Inverse = Quaternion.Inverse(Quaternion.LookRotation(forward));
        hips.InverseRotation = hips.Inverse * hips.InitRotation;

        // Head Rotation
        var head = jointPoints[PositionIndex.head.Int()];
        head.InitRotation = jointPoints[PositionIndex.head.Int()].Transform.rotation;
        if(kinectScene && vrRunning)
        {
            var gaze = jointPoints[PositionIndex.phantomNose.Int()].Transform.position - jointPoints[PositionIndex.head.Int()].Transform.position;
            head.Inverse = Quaternion.Inverse(Quaternion.LookRotation(gaze));
            head.InverseRotation = head.Inverse * head.InitRotation;
        }
        else
        {
            var gaze = jointPoints[PositionIndex.Nose.Int()].Transform.position - jointPoints[PositionIndex.head.Int()].Transform.position;
            head.Inverse = Quaternion.Inverse(Quaternion.LookRotation(gaze));
            head.InverseRotation = head.Inverse * head.InitRotation;
        }

        if(!vrRunning)
        {    
            // Wrists rotation
            var lWrist = jointPoints[PositionIndex.lWrist.Int()];
            var lf = TriangleNormal(lWrist.Pos3D, jointPoints[PositionIndex.lPinky.Int()].Pos3D, jointPoints[PositionIndex.lThumb.Int()].Pos3D);
            lWrist.InitRotation = lWrist.Transform.rotation;
            lWrist.Inverse = Quaternion.Inverse(Quaternion.LookRotation(jointPoints[PositionIndex.lThumb.Int()].Transform.position - jointPoints[PositionIndex.lPinky.Int()].Transform.position, lf));
            lWrist.InverseRotation = lWrist.Inverse * lWrist.InitRotation;

            var rWrist = jointPoints[PositionIndex.rWrist.Int()];
            var rf = TriangleNormal(rWrist.Pos3D, jointPoints[PositionIndex.rThumb.Int()].Pos3D, jointPoints[PositionIndex.rPinky.Int()].Pos3D);
            rWrist.InitRotation = jointPoints[PositionIndex.rWrist.Int()].Transform.rotation;
            rWrist.Inverse = Quaternion.Inverse(Quaternion.LookRotation(jointPoints[PositionIndex.rThumb.Int()].Transform.position - jointPoints[PositionIndex.rPinky.Int()].Transform.position, rf));
            rWrist.InverseRotation = rWrist.Inverse * rWrist.InitRotation;
        }

        return JointPoints;
    }

    public void PoseUpdate()
    {       
        // movement and rotatation of the center
        var forward = TriangleNormal(jointPoints[PositionIndex.hips.Int()].Pos3D, jointPoints[PositionIndex.lHip.Int()].Pos3D, jointPoints[PositionIndex.rHip.Int()].Pos3D);
        if(!vrRunning)
            jointPoints[PositionIndex.hips.Int()].Transform.position = jointPoints[PositionIndex.hips.Int()].Pos3D + initPosition - jointPositionOffset;
        else
        {
            if (kinectScene)
                jointPoints[PositionIndex.hips.Int()].Transform.position = jointPoints[PositionIndex.hips.Int()].Pos3D - jointPoints[PositionIndex.phantomNose.Int()].Pos3D + hmdPosition - jointPositionOffset;
            else
                jointPoints[PositionIndex.hips.Int()].Transform.position = jointPoints[PositionIndex.hips.Int()].Pos3D - jointPoints[PositionIndex.Nose.Int()].Pos3D + hmdPosition - jointPositionOffset;
        }
        jointPoints[PositionIndex.hips.Int()].Transform.rotation = Quaternion.LookRotation(forward) * jointPoints[PositionIndex.hips.Int()].InverseRotation;

        // rotation of each of the bones
        foreach (var jointPoint in jointPoints)
        {
            if (jointPoint.Parent != null)
            {
                var fv = jointPoint.Parent.Pos3D - jointPoint.Pos3D;
                jointPoint.Transform.rotation = Quaternion.LookRotation(jointPoint.Pos3D - jointPoint.Child.Pos3D, fv) * jointPoint.InverseRotation;
            }
            else if (jointPoint.Child != null)
            {
                jointPoint.Transform.rotation = Quaternion.LookRotation(jointPoint.Pos3D - jointPoint.Child.Pos3D, forward) * jointPoint.InverseRotation;
            }
        }

        if(!vrRunning || (!kinectScene && !vrRunning))
        {
            // Head Rotation
            var gaze = jointPoints[PositionIndex.Nose.Int()].Pos3D - jointPoints[PositionIndex.head.Int()].Pos3D;
            var f = TriangleNormal(jointPoints[PositionIndex.Nose.Int()].Pos3D, jointPoints[PositionIndex.rEar.Int()].Pos3D, jointPoints[PositionIndex.lEar.Int()].Pos3D);
            var head = jointPoints[PositionIndex.head.Int()];
            head.Transform.rotation = Quaternion.LookRotation(gaze, f) * head.InverseRotation;
        }

        if(!vrRunning)
        {
            // Wrist rotation
            var lWrist = jointPoints[PositionIndex.lWrist.Int()];
            var lf = TriangleNormal(lWrist.Pos3D, jointPoints[PositionIndex.lPinky.Int()].Pos3D, jointPoints[PositionIndex.lThumb.Int()].Pos3D);
            lWrist.Transform.rotation = Quaternion.LookRotation(jointPoints[PositionIndex.lThumb.Int()].Pos3D - jointPoints[PositionIndex.lPinky.Int()].Pos3D, lf) * lWrist.InverseRotation;

            var rWrist = jointPoints[PositionIndex.rWrist.Int()];
            var rf = TriangleNormal(rWrist.Pos3D, jointPoints[PositionIndex.rThumb.Int()].Pos3D, jointPoints[PositionIndex.rPinky.Int()].Pos3D);
            rWrist.Transform.rotation = Quaternion.LookRotation(jointPoints[PositionIndex.rThumb.Int()].Pos3D - jointPoints[PositionIndex.rPinky.Int()].Pos3D, rf) * rWrist.InverseRotation;
        }

        foreach (var sk in Skeletons)
        {
            var s = sk.start;
            var e = sk.end;

            sk.Line.SetPosition(0, new Vector3(s.Pos3D.x * SkeletonScale + SkeletonX, s.Pos3D.y * SkeletonScale + SkeletonY, s.Pos3D.z * SkeletonScale + SkeletonZ));
            sk.Line.SetPosition(1, new Vector3(e.Pos3D.x * SkeletonScale + SkeletonX, e.Pos3D.y * SkeletonScale + SkeletonY, e.Pos3D.z * SkeletonScale + SkeletonZ));
        }
    }

    Vector3 TriangleNormal(Vector3 a, Vector3 b, Vector3 c)
    {
        Vector3 d1 = a - b;
        Vector3 d2 = a - c;

        Vector3 dd = Vector3.Cross(d1, d2);
        dd.Normalize();

        return dd;
    }

    private Quaternion GetInverse(JointPoint p1, JointPoint p2, Vector3 forward)
    {
        return Quaternion.Inverse(Quaternion.LookRotation(p1.Transform.position - p2.Transform.position, forward));
    }

    /// <summary>
    /// Add skelton from joint points
    /// </summary>
    /// <param name="s">position index</param>
    /// <param name="e">position index</param>
    private void AddSkeleton(PositionIndex s, PositionIndex e)
    {
        var sk = new Skeleton()
        {
            LineObject = new GameObject("Line"),
            start = jointPoints[s.Int()],
            end = jointPoints[e.Int()],
        };

        sk.Line = sk.LineObject.AddComponent<LineRenderer>();
        sk.Line.startWidth = 0.025f;
        sk.Line.endWidth = 0.005f;
        
        // define the number of vertex
        sk.Line.positionCount = 2;
        sk.Line.material = SkeletonMaterial;

        Skeletons.Add(sk);
    }

    private static bool isVrRunning()
    {
        var xrDisplaySubsystems = new List<XRDisplaySubsystem>();
        SubsystemManager.GetInstances<XRDisplaySubsystem>(xrDisplaySubsystems);
        foreach (var xrDisplay in xrDisplaySubsystems)
        {
            if (xrDisplay.running)
            {
                return true;
            }
        }
        return false;
    }

    private Vector3 GetCenter(GameObject obj)
    {
        Vector3 sumVector = Vector3.zero;

        foreach (Transform child in obj.transform)
        {          
            sumVector += child.position;        
        }

        Vector3 groupCenter = sumVector / obj.transform.childCount;
        return sumVector;
    }

    private void RunCalibration()
    {
        Instruction.SetActive(true);
        if (!vrRunning)
        {
            Debug.Log("Avatar calibration will begin in 5 seconds, please stand in T-pose!");
            if (kinectScene)
            {
                StartCoroutine(BodySourceView.KinectCalibrationRoutine(vrRunning, kinectTDimensionsCalculated => {
                    ScaleAvatar(kinectTDimensionsCalculated);
                    Debug.Log("Avatar calibration done!");
                    Instruction.SetActive(false);
                }));
            }
            else
            {
                StartCoroutine(PoseVisualizer3D.PoseCalibrationRoutine(vrRunning, poseTDimensionsCalculated => {
                    ScaleAvatar(poseTDimensionsCalculated);
                    Debug.Log("Avatar calibration done!");
                    Instruction.SetActive(false);
                }));
            }
        }
        else
        {
            Debug.Log("VR calibration will begin in 5 seconds, please stand in T-pose!");
            StartCoroutine(VrCalibrationRoutine(vrTDimensionsCalculated => {
                Vector3 vrTDimensions = vrTDimensionsCalculated;
                if (kinectScene)
                {
                    StartCoroutine(BodySourceView.KinectCalibrationRoutine(vrRunning, kinectTDimensionsCalculated => {
                        BodySourceView.ScaleKinect(vrTDimensions, kinectTDimensionsCalculated);
                        Debug.Log("VR calibration done!");
                        Instruction.SetActive(false);
                    }));
                }
                else
                {
                    StartCoroutine(PoseVisualizer3D.PoseCalibrationRoutine(vrRunning, poseTDimensionsCalculated => {
                        PoseVisualizer3D.ScalePose(vrTDimensions, poseTDimensionsCalculated);
                        Debug.Log("VR calibration done!");
                        Instruction.SetActive(false);
                    }));
                }
            }));
        }
    }

    private IEnumerator VrCalibrationRoutine(System.Action<Vector3> callback = null)
    {
        yield return new WaitForSeconds(5);
        Vector3 vrTDimensions = Vector3.zero;
        vrTDimensions.x = Vector3.Distance(leftControllerPosition, rightControllerPosition);
        vrTDimensions.y = hmdPosition.y;
        ScaleAvatar(vrTDimensions);
        callback (vrTDimensions);
    }

    /// <summary>
    /// Scale the avatar based on the physical dimensions of the user's body
    /// </summary>
    private void ScaleAvatar(Vector3 bodyTDimensions)
    {
        Vector3 scaling;
        scaling.x = bodyTDimensions.x / avatarDimensions.x;
        scaling.y = bodyTDimensions.y / avatarDimensions.y;
        scaling.z = (scaling.x + scaling.y) / 2f;
        transform.localScale = scaling;
        jointPositionOffset.y = avatarCenter.y - avatarCenter.y * scaling.y;
        Debug.Log("Avatar scaling done");
    }
}
