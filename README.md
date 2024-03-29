[![License: GPL v3](https://img.shields.io/badge/License-GPLv3-blue.svg)](https://www.gnu.org/licenses/gpl-3.0)&nbsp;
<img src="https://img.shields.io/badge/Unity-2019.4.28f1-blue.svg?style=flat-round" alt="Unity 2019.4.28f1">

# Full Body Pose Estimation

Aim of the project is to achieve a full body controllable avatar in the VR environment (Unity). Head and hands are tracked using HMD (Quest2) and two controllers while tracking of the rest of the body parts is achieved using different 3D pose estimation techniques.  
Those techniques include:  
- Neural network based pose estimation using a single webcam ([BlazePoseBarracuda](https://github.com/creativeIKEP/BlazePoseBarracuda))
- Depth map based pose estimation using [Kinect v2](https://developer.microsoft.com/en-us/windows/kinect/)
- Neural network based pose estimation using multiple webcams ([MocapForAll](https://vrlab.akiya-souken.co.jp/en/product))

<img src="bones.gif" width="600" height="338" />  

## How to start

**PoseScene** - BlazePose based pose estimation integrated with HMD and controllers tracking  
**KinectScene** - Kinect v2 based pose estimation integrated with HMD and controllers tracking  
(Both scenes support VR but they also work fine without VR)  
**MocapScene** - MoveNet based pose estimation

### Prerequisites

When running the application you should orient your body so that it will be facing the camera preview screen. In order for the pose estimation algorithm to work correctly your entire body must be present within the camera's field of view at all times.  
Make sure that there is only one person present within the camera's field of view.  
When working with VR it is crucial that you align your VR 'front' with the position of the physical camera (**PoseScene** - your webcam, **KinectScene** - Kinect).  
Kinect implementation (**KinectScene**) requires installation of the [Kinect for Windows SDK 2.0](https://www.microsoft.com/en-us/download/details.aspx?id=44561)  
MocapForAll implementation (**MocapScene**) requires the [MocapForAll application](https://store.steampowered.com/app/1759710/MocapForAll/)

### Calibration

To compensate for different height and arms span of different users using the application a calibration routine has been implemented.  
Calibration can be triggered in either scene by pressing space bar key (or left controller primary button - in VR).  
After triggering the calibration you should stand in T-pose for 5 seconds.  
Calibration have to be redone after each application restart or scene change. If necessary callibration can be repeated while running a single scene, there is no need for application restart.  

### Preview

Preview screen can display either live webcam picture or virtual mirror. It can be toggled by pressing 'g' key (or right controller primary button - in VR). 

<img src="PoseScene_demo.gif" width="600" height="338" />  

### Acknowledgments

Full body rig solution created based on the solution from [ThreeDPoseUnityBarracuda](https://github.com/digital-standard/ThreeDPoseUnityBarracuda) project.
