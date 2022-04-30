using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SharedMemoryToTransform : SharedMemoryReader
{
    // output from shared memory
    public Transform[] data;
    public VNectModel VNectModel;

    /// <summary>
    /// Coordinates of joint points
    /// </summary>
    private VNectModel.JointPoint[] jointPoints;
    private Quaternion headRotation;

    void Start()
    {
        jointPoints = VNectModel.Initialize();
    }

    // set as Transform
    public override void OutputData(float[] buffer)
    {
        // Joints visualizer
        for (int i = 0; i < data.Length && i * 7 + 6 < buffer.Length; i++)
        {
            data[i].rotation = GetRotationFromBuffer(buffer, i);
            data[i].position = GetPositionFromBuffer(buffer, i);
        }

        headRotation = GetRotationFromBuffer(buffer, 3);

        // Mapping of joints from buffer to VNectModel joints
        jointPoints[PositionIndex.hips.Int()].Pos3D = GetPositionFromBuffer(buffer, 0);
        jointPoints[PositionIndex.spine.Int()].Pos3D = GetPositionFromBuffer(buffer, 1);
        jointPoints[PositionIndex.neck.Int()].Pos3D = GetPositionFromBuffer(buffer, 2);
        jointPoints[PositionIndex.chest.Int()].Pos3D = Vector3.Lerp(jointPoints[PositionIndex.spine.Int()].Pos3D, jointPoints[PositionIndex.neck.Int()].Pos3D, 0.5f);
        jointPoints[PositionIndex.head.Int()].Pos3D = GetPositionFromBuffer(buffer, 3);
        jointPoints[PositionIndex.lShoulder.Int()].Pos3D = GetPositionFromBuffer(buffer, 4);
        jointPoints[PositionIndex.lElbow.Int()].Pos3D = GetPositionFromBuffer(buffer, 5);
        jointPoints[PositionIndex.lWrist.Int()].Pos3D = GetPositionFromBuffer(buffer, 6);
        jointPoints[PositionIndex.rShoulder.Int()].Pos3D = GetPositionFromBuffer(buffer, 7);
        jointPoints[PositionIndex.rElbow.Int()].Pos3D = GetPositionFromBuffer(buffer, 8);
        jointPoints[PositionIndex.rWrist.Int()].Pos3D = GetPositionFromBuffer(buffer, 9);
        jointPoints[PositionIndex.lHip.Int()].Pos3D = GetPositionFromBuffer(buffer, 10);
        jointPoints[PositionIndex.lKnee.Int()].Pos3D = GetPositionFromBuffer(buffer, 11);
        jointPoints[PositionIndex.lAnkle.Int()].Pos3D = GetPositionFromBuffer(buffer, 12);
        jointPoints[PositionIndex.rHip.Int()].Pos3D = GetPositionFromBuffer(buffer, 13);
        jointPoints[PositionIndex.rKnee.Int()].Pos3D = GetPositionFromBuffer(buffer, 14);
        jointPoints[PositionIndex.rAnkle.Int()].Pos3D = GetPositionFromBuffer(buffer, 15);
    }

    private Quaternion GetRotationFromBuffer(float[] buffer, int index)
    {
        index = index * 7;
        return new Quaternion(buffer[index], buffer[index + 2], -buffer[index + 1], buffer[index + 3]) * Quaternion.Euler(new Vector3(0f, 180f, 0f));
    }

    private Vector3 GetPositionFromBuffer(float[] buffer, int index)
    {
        index = index * 7;
        return new Vector3(-buffer[index + 4], buffer[index + 6], buffer[index + 5]) / 100.0f;
    }

    public Quaternion GetHeadRotation()
    {
        return headRotation;
    }
}
