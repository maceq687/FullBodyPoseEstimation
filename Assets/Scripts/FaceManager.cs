using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Windows.Kinect;
using Microsoft.Kinect.Face;
using System.Linq;

public static partial class EnumExtend
	{
		public static int Int(this HighDetailFacePoints i)
		{
			return (int)i;
		}
	}

public class FaceManager : MonoBehaviour
{
	private KinectSensor kinectSensor;
	private int bodyCount;
	private Body[] bodies;
	private BodySourceManager bodySourceManager;
	private FaceFrameSource[] faceFrameSources;
	private FaceFrameReader[] faceFrameReaders;
	private BodyFrameSource _bodySource = null;
	private BodyFrameReader _bodyReader = null;
	private HighDefinitionFaceFrameSource _hdFaceFrameSources = null;
	private HighDefinitionFaceFrameReader _hdFaceFrameReaders = null;
	public BodySourceManager bodyManager;
	private FaceAlignment _faceAlignment = null;
	private FaceModel _faceModel = null;
	private Quaternion faceRotation;
	private Vector3 leftEyeMidTop;
	private Vector3 leftEyeMidBottom;
	private Vector3 rightEyeMidTop;
	private Vector3 rightEyeMidBottom;
	private Vector3 leftCheekCenter;
	private Vector3 rightCheekCenter;
	private Vector3 leftCheekBone;
	private Vector3 rightCheekBone;
	private Vector3[] facePointsPosition = new Vector3[5];

	void Start()
	{
		// one sensor is currently supported
		kinectSensor = KinectSensor.GetDefault();
		
		// set the maximum number of bodies that would be tracked by Kinect
		bodyCount = kinectSensor.BodyFrameSource.BodyCount;
		
		// allocate storage to store body objects
		bodies = new Body[bodyCount];

		// get bodies either from BodySourceManager object get them from a BodyReader
		bodySourceManager = bodyManager.GetComponent<BodySourceManager>();
		
		// specify the required face frame results
		FaceFrameFeatures faceFrameFeatures =
			FaceFrameFeatures.BoundingBoxInColorSpace
				| FaceFrameFeatures.PointsInColorSpace
				| FaceFrameFeatures.BoundingBoxInInfraredSpace
				| FaceFrameFeatures.PointsInInfraredSpace
				| FaceFrameFeatures.RotationOrientation
				| FaceFrameFeatures.FaceEngagement
				| FaceFrameFeatures.Glasses
				| FaceFrameFeatures.Happy
				| FaceFrameFeatures.LeftEyeClosed
				| FaceFrameFeatures.RightEyeClosed
				| FaceFrameFeatures.LookingAway
				| FaceFrameFeatures.MouthMoved
				| FaceFrameFeatures.MouthOpen;
		
		// create a face frame source + reader to track each face in the FOV
		faceFrameSources = new FaceFrameSource[bodyCount];
		faceFrameReaders = new FaceFrameReader[bodyCount];
		for (int i = 0; i < bodyCount; i++)
		{
			// create the face frame source with the required face frame features and an initial tracking Id of 0
			faceFrameSources[i] = FaceFrameSource.Create(kinectSensor, 0, faceFrameFeatures);
			
			// open the corresponding reader
			faceFrameReaders[i] = faceFrameSources[i].OpenReader();
		}

		_bodySource = kinectSensor.BodyFrameSource;
        _bodyReader = _bodySource.OpenReader();
    	_bodyReader.FrameArrived += BodyReader_FrameArrived;

		_hdFaceFrameSources = HighDefinitionFaceFrameSource.Create(kinectSensor);

		_hdFaceFrameReaders = _hdFaceFrameSources.OpenReader();
		_hdFaceFrameReaders.FrameArrived += FaceReader_FrameArrived;

		_faceModel = FaceModel.Create();
		_faceAlignment = FaceAlignment.Create();
	}
	
	void Update()
	{
		bodies = bodySourceManager.GetData();

		if (bodies == null)
		{
			return;
		}
		
		// iterate through each body and update face source
		for (int i = 0; i < bodyCount; i++)
		{
			// check if a valid face is tracked in this face source				
			if (faceFrameSources[i].IsTrackingIdValid)
			{
				using(FaceFrame frame = faceFrameReaders[i].AcquireLatestFrame())
				{
					if(frame != null)
					{
						if (frame.TrackingId == 0)
						{
							continue;
						}

						// do something with the result
						var result = frame.FaceFrameResult;

						var faceRotationVector4 = result.FaceRotationQuaternion;
						faceRotation.Set(faceRotationVector4.X, faceRotationVector4.Y, faceRotationVector4.Z, faceRotationVector4.W);
					}
				}
			}
			else
			{
				if (bodies[i] == null)
					return;
				
				// check if the corresponding body is tracked 
				if (bodies[i].IsTracked)
				{
					// update the face frame source to track this body
					faceFrameSources[i].TrackingId = bodies[i].TrackingId;
				}
			}
		}	

		RetriveFacePoints();	
	}

	public Quaternion GetFaceRotation()
	{
		return faceRotation;
	}

	private void BodyReader_FrameArrived(object sender, BodyFrameArrivedEventArgs e)
    {
        using (var frame = e.FrameReference.AcquireFrame())
        {
            if (frame != null)
            {
                Body[] bodies = new Body[frame.BodyCount];
                frame.GetAndRefreshBodyData(bodies);

                Body body = bodies.Where(b => b.IsTracked).FirstOrDefault();

                if (!_hdFaceFrameSources.IsTrackingIdValid)
                {
                    if (body != null)
                    {
                        _hdFaceFrameSources.TrackingId = body.TrackingId;
                    }
                }
            }
        }
    }

	private void FaceReader_FrameArrived(object sender, HighDefinitionFaceFrameArrivedEventArgs e)
    {
        using (var frame = e.FrameReference.AcquireFrame())
        {
            if (frame != null && frame.IsFaceTracked)
            {
                frame.GetAndRefreshFaceAlignmentResult(_faceAlignment);
            }
        }
    }

	private void RetriveFacePoints()
	{
		if (_faceModel == null) return;
		
		var vertices = _faceModel.CalculateVerticesForAlignment(_faceAlignment);

		if (vertices.Count > 0)
        {
			for (int index = 0; index < vertices.Count; index++)
			{
				// Get face points positions

				if (index == HighDetailFacePoints.NoseTip.Int())
					facePointsPosition[0] = GetVerticePosition(vertices[index]);

				if (index == HighDetailFacePoints.LefteyeMidtop.Int())
					leftEyeMidTop = GetVerticePosition(vertices[index]);

				if (index == HighDetailFacePoints.LefteyeMidbottom.Int())
					leftEyeMidBottom = GetVerticePosition(vertices[index]);

				facePointsPosition[1] = Vector3.Lerp(leftEyeMidTop, leftEyeMidBottom, 0.5f);

				if (index == HighDetailFacePoints.RighteyeMidtop.Int())
					rightEyeMidTop = GetVerticePosition(vertices[index]);

				if (index == HighDetailFacePoints.RighteyeMidbottom.Int())
					rightEyeMidBottom = GetVerticePosition(vertices[index]);

				facePointsPosition[2] = Vector3.Lerp(rightEyeMidTop, rightEyeMidBottom, 0.5f);

				if(index == HighDetailFacePoints.LeftcheekCenter.Int())
					leftCheekCenter = GetVerticePosition(vertices[index]);
				
				if(index == HighDetailFacePoints.Leftcheekbone.Int())
					leftCheekBone = GetVerticePosition(vertices[index]);

				Vector3 leftCheek = Vector3.Lerp(leftCheekCenter, leftCheekBone, 0.5f);
				Vector3 noseLeftCheek = leftCheek - facePointsPosition[0];
				facePointsPosition[3] = leftCheek + noseLeftCheek;

				if(index == HighDetailFacePoints.RightcheekCenter.Int())
					rightCheekCenter = GetVerticePosition(vertices[index]);

				if(index == HighDetailFacePoints.Rightcheekbone.Int())
					rightCheekBone = GetVerticePosition(vertices[index]);

				Vector3 rightCheek = Vector3.Lerp(rightCheekCenter, rightCheekBone, 0.5f);
				Vector3 noseRightCheek = rightCheek - facePointsPosition[0];
				facePointsPosition[4] = rightCheek + noseRightCheek;
			}	
		}
	}

	private Vector3 GetVerticePosition(Windows.Kinect.CameraSpacePoint vertice)
	{
		return new Vector3(vertice.X, vertice.Y, (vertice.Z * -1f) + 2f);
	}

	public Vector3 GetFacePointsPosition(int index)
	{
		return facePointsPosition[index];
	}
}
