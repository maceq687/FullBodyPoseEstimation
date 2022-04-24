[![License: GPL v3](https://img.shields.io/badge/License-GPLv3-blue.svg)](https://www.gnu.org/licenses/gpl-3.0)&nbsp;
<img src="https://img.shields.io/badge/Unity-2019.4.28f1-blue.svg?style=flat-round" alt="Unity 2019.4.28f1">

# Full Body Pose Estimation

Aim of the project is to achieve a full body controllable avatar in the VR environment (Unity). Head and hands are tracked using HMD (Quest2) and two controllers while tracking of the rest of the body parts is achieved using different 3D pose estimation techniques.  
Those techniques include:  
- Neural network based pose estimation using a single webcam ([BlazePoseBarracuda](https://github.com/creativeIKEP/BlazePoseBarracuda))
- Depth map based pose estimation using [Kinect v2](https://developer.microsoft.com/en-us/windows/kinect/)

## How to start

**PoseScene** - BlazePose based pose estimation integrated with HMD and controllers tracking  
**KinectScene** - Kinect v2 based pose estimation integrated with HMD and controllers tracking  
(Both scenes suppport VR but they also work fine without VR)

### Prerequisites

When running the application you should see the mirrored camera preview in fron of you. In order for the pose estimation algorithm to work corectly the camera need to see your entire body at all times.  
Make sure that there is only one person present within the camera's field of view.  
When working with VR it is crucial that you align your VR 'front' with the position of the physical camera (**PoseScene** - your webcam, **KinectScene** - Kinect)  

### Calibration

To compensate for different height and arms span of different users using the application a calibration routine has been implemented.  
Calibration can be triggered in either scene by pressing spacebar (or left controller primary button - in VR).  
After triggering the calibration you should stand in the T-pose for 6 seconds.  
Calibration have to be redone after each application restart or scene change.

### Acknowledgments
Full body rig solution created based on the solution from [ThreeDPoseUnityBarracuda](https://github.com/digital-standard/ThreeDPoseUnityBarracuda) project.
