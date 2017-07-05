using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Kinect;
using LightBuzz.Vitruvius;

namespace KiBoard
{
    class FingerTracker : Tracker
    {
        private KinectSensor sensor;
        private MultiSourceFrameReader multiReader;
        private Body[] bodyData;
        private CameraSpacePoint joint;
        private ushort[] depthData;
        private const int SEARCH_FIELD = 10;
        private System.Numerics.Vector3 vec3;

        public FingerTracker(KinectSensor sensor, MultiSourceFrameReader multiReader, DepthFrameReader depthReader)
        {
            this.sensor = sensor;
            this.multiReader = multiReader;
            Console.WriteLine("FingerTracker created!");
        }

        public System.Numerics.Vector3 Coordinates
        {
            get
            {
                MultiSourceFrame frame = null;
                if (multiReader != null)
                {
                    // Getting the latest Frameset
                    frame = multiReader.AcquireLatestFrame();
                    if (frame != null)
                    {
                        // Getting the latest bodyFrame
                        var bodyFrame = frame.BodyFrameReference.AcquireFrame();
                        var depthFrame = frame.DepthFrameReference.AcquireFrame();
                        if (bodyFrame != null)
                        {
                            if (bodyData == null)
                            {
                                // Creates the the array with the number of tracked Bodyies
                                bodyData = new Body[bodyFrame.BodyFrameSource.BodyCount];
                            }
                            // Save the Body-Jointpositions to bodyData
                            bodyFrame.GetAndRefreshBodyData(bodyData);
                            bodyFrame.Dispose();
                            bodyFrame = null;
                        }
                        if (depthFrame != null)
                        {
                            int width = depthFrame.FrameDescription.Width;
                            int height = depthFrame.FrameDescription.Height;

                            depthData = new ushort[width * height];

                            depthFrame.CopyFrameDataToArray(depthData);
                        }
                    }
                    frame = null;
                    int index = -1;
                    for (int i = 0; i < sensor.BodyFrameSource.BodyCount; i++)
                    {
                        if (bodyData == null || bodyData[i] == null)
                        {
                            continue;
                        }
                        if (bodyData[i].IsTracked)
                        {
                            index = i;
                        }
                    }
                    if (index > -1)
                    {
                        // We use the right Hand
                        CameraSpacePoint joint = new CameraSpacePoint();
                        joint.X = bodyData[index].Joints[JointType.HandRight].Position.X;
                        joint.Y = bodyData[index].Joints[JointType.HandRight].Position.Y;
                        joint.Z = bodyData[index].Joints[JointType.HandRight].Position.Z;
                        vec3 = new System.Numerics.Vector3(joint.X, joint.Y, joint.Z);
                    }
                    else
                    {
                        System.Console.WriteLine("no body found");
                    }
                }
                getFingerTipPoint(joint);
                return vec3;
            }
        }

        private System.Numerics.Vector3 getFingerTipPoint(CameraSpacePoint joint)
        {
            DepthSpacePoint depthPoint = sensor.CoordinateMapper.MapCameraPointToDepthSpace(joint);

            Console.WriteLine(depthData[(int)depthPoint.Y * 512 + (int)depthPoint.X]);
            
            return new System.Numerics.Vector3();
        } 
    }
}
