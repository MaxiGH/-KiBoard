using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Kinect;

namespace KiBoard.tracker
{
    class FingerTracker : Tracker
    {
        private KinectSensor sensor;
        private MultiSourceFrameReader multiReader;
        private DepthFrameReader depthReader;
        private Body[] bodyData;
        private Vector3 vec3;

        public FingerTracker(KinectSensor sensor, MultiSourceFrameReader multiReader, DepthFrameReader depthReader)
        {
            this.sensor = sensor;
            this.multiReader = multiReader;
            this.depthReader = depthReader;
            Console.WriteLine("FingerTracker created!");
        }

        public Vector3 Coordinates
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
                        float xPos = bodyData[index].Joints[JointType.HandRight].Position.X;
                        float yPos = bodyData[index].Joints[JointType.HandRight].Position.Y;
                        float zPos = bodyData[index].Joints[JointType.HandRight].Position.Z;
                        //Console.WriteLine("Rechte Hand {0}, {1}, {2}", xPos, yPos, zPos);
                        vec3 = new Vector3(xPos, yPos, zPos);
                    }
                    else
                    {
                        System.Console.WriteLine("no body found");
                    }
                }
                return getFingerTipPoint(vec3, frame);
            }
        }

        private Vector3 getFingerTipPoint(Vector3 handjoint, MultiSourceFrame frame)
        {

            return handjoint;
        } 
    }
}
