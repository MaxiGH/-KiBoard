using System;
using System.Threading;
using Microsoft.Kinect;
using System.Numerics;

namespace KiBoard
{
    class Program
    {
        private static KinectSensor sensor;
        private static MultiSourceFrameReader multiReader;

        static void Main(string[] args)
        {
            bool isRunning = true;
            setupKinect();
            Tracker3D tracker = new Tracker3D(sensor, multiReader);
            while (isRunning) {
                Vector3 trackedData = tracker.Coordinates;
                Thread.Sleep(200);
            }
        }

        private static void setupKinect()
        {
            sensor = KinectSensor.GetDefault();
            multiReader = sensor.OpenMultiSourceFrameReader(FrameSourceTypes.Body);
            if (sensor != null)
            {
                multiReader = sensor.OpenMultiSourceFrameReader(FrameSourceTypes.Body);
                if (!sensor.IsOpen)
                {
                    sensor.Open();
                }
            }
        }
    }
}
