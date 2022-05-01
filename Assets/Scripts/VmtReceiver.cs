/*
 * VmtReceiver
 * https://twitter.com/KenjiASABA
 *
 * MIT License
 * 
 * Copyright (c) 2021 K. Asaba
 * 
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 * 
 * The above copyright notice and this permission notice shall be included in all
 * copies or substantial portions of the Software.
 * 
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
 * SOFTWARE.
 */

using UnityEngine;
using UnityEngine.Events;

public class VmtReceiver : MonoBehaviour
{
    public uOSC.uOscServer server = null;

    // On data updated, these objects are moved. Delete them if you implement your own event.
    public GameObject[] gameObjectsToBeTransformed;

    public class DataUpdateEvent : UnityEvent<int, Vector3, Quaternion> { };

    public DataUpdateEvent OnDataUpdated { get; private set; }
    public VNectModel VNectModel;

    /// <summary>
    /// Coordinates of joint points
    /// </summary>
    private VNectModel.JointPoint[] jointPoints;
    private Quaternion[] bodyLimbRotation = new Quaternion[5];


    void Awake()
    {
        OnDataUpdated = new DataUpdateEvent();
    }

    void Start()
    {
        // Start listening
        server = GetComponent<uOSC.uOscServer>();
        if (server)
        {
            server.onDataReceived.AddListener(OnMessageReceived);
        }

        // Bind an event to OnDataUpdated
        this.OnDataUpdated.AddListener(SetTransforms);

        jointPoints = VNectModel.Initialize();
    }

    private void OnMessageReceived(uOSC.Message message)
    {
        if (message.address == null || message.values == null)
        {
            return;
        }

        if (message.address == "/VMT/Room/Unity"
            && (message.values[0] is int) // tracker index
            && (message.values[1] is int) // enable
            && (message.values[2] is float) // time offset
            && (message.values[3] is float) // position.x
            && (message.values[4] is float) // position.y
            && (message.values[5] is float) // position.z
            && (message.values[6] is float) // rotation.x
            && (message.values[7] is float) // rotation.y
            && (message.values[8] is float) // rotation.z
            && (message.values[9] is float) // rotation.w
            )
        {
            int trackerIndex;
            Vector3 pos;
            Quaternion rot;

            trackerIndex = (int)message.values[0];
            pos.x = (float)message.values[3];
            pos.y = (float)message.values[4];
            pos.z = (float)message.values[5];
            rot.x = (float)message.values[6];
            rot.y = (float)message.values[7];
            rot.z = (float)message.values[8];
            rot.w = (float)message.values[9];

            OnDataUpdated.Invoke(trackerIndex, pos, rot);

            if ((int)message.values[0] == 0)
                jointPoints[PositionIndex.hips.Int()].Pos3D = GetPositionFromMessage(message);

            if ((int)message.values[0] == 1)
            {
                jointPoints[PositionIndex.lAnkle.Int()].Pos3D = GetPositionFromMessage(message);
                bodyLimbRotation[3] = GetRotationFromMessage(message);
            }

            if ((int)message.values[0] == 2)
            {
                jointPoints[PositionIndex.rAnkle.Int()].Pos3D = GetPositionFromMessage(message);
                bodyLimbRotation[4] = GetRotationFromMessage(message);
            }
                
            if ((int)message.values[0] == 3)
                jointPoints[PositionIndex.lKnee.Int()].Pos3D = GetPositionFromMessage(message);

            if ((int)message.values[0] == 4)
                jointPoints[PositionIndex.rKnee.Int()].Pos3D = GetPositionFromMessage(message);

            if ((int)message.values[0] == 5)
                jointPoints[PositionIndex.lHip.Int()].Pos3D = GetPositionFromMessage(message);

            if ((int)message.values[0] == 6)
                jointPoints[PositionIndex.rHip.Int()].Pos3D = GetPositionFromMessage(message);

            if ((int)message.values[0] == 7)
                jointPoints[PositionIndex.spine.Int()].Pos3D = GetPositionFromMessage(message);

            if ((int)message.values[0] == 8)
                jointPoints[PositionIndex.neck.Int()].Pos3D = GetPositionFromMessage(message);

            if ((int)message.values[0] == 9)
            {
                jointPoints[PositionIndex.head.Int()].Pos3D = GetPositionFromMessage(message);
                bodyLimbRotation[0] = GetRotationFromMessage(message);
            }

            if ((int)message.values[0] == 10)
            {
                jointPoints[PositionIndex.lWrist.Int()].Pos3D = GetPositionFromMessage(message);
                bodyLimbRotation[1] = GetRotationFromMessage(message);
            }

            if ((int)message.values[0] == 11)
            {
                jointPoints[PositionIndex.rWrist.Int()].Pos3D = GetPositionFromMessage(message);
                bodyLimbRotation[2] = GetRotationFromMessage(message);
            }

            if ((int)message.values[0] == 12)
                jointPoints[PositionIndex.lElbow.Int()].Pos3D = GetPositionFromMessage(message);

            if ((int)message.values[0] == 13)
                jointPoints[PositionIndex.rElbow.Int()].Pos3D = GetPositionFromMessage(message);

            if ((int)message.values[0] == 14)
                jointPoints[PositionIndex.lShoulder.Int()].Pos3D = GetPositionFromMessage(message);

            if ((int)message.values[0] == 15)
                jointPoints[PositionIndex.rShoulder.Int()].Pos3D = GetPositionFromMessage(message);

            jointPoints[PositionIndex.chest.Int()].Pos3D = Vector3.Lerp(jointPoints[PositionIndex.spine.Int()].Pos3D, jointPoints[PositionIndex.neck.Int()].Pos3D, 0.5f);
        }
    }

    // To implement your own method, modify this, or implement a new method and bind it to OnDataUpdated
    private void SetTransforms(int trackerIndex, Vector3 pos, Quaternion rot)
    {
        if (trackerIndex >= 0 && gameObjectsToBeTransformed.Length > trackerIndex)
        {
            if (gameObjectsToBeTransformed[trackerIndex] != null)
            {
                gameObjectsToBeTransformed[trackerIndex].transform.position = pos;
                gameObjectsToBeTransformed[trackerIndex].transform.rotation = rot;
            }
        }
    }

    private Vector3 GetPositionFromMessage(uOSC.Message message)
    {
        return new Vector3((float)message.values[3], (float)message.values[4], (float)message.values[5]);
    }

    private Quaternion GetRotationFromMessage(uOSC.Message message)
    {
        return new Quaternion((float)message.values[6], (float)message.values[7], (float)message.values[8], (float)message.values[9]);
    }

    public Quaternion GetBodyLimbRotation(int index)
    {
        return bodyLimbRotation[index];
    }
}