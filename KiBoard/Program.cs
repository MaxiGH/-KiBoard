using System;
using System.Threading;
using Microsoft.Kinect;

namespace KiBoard
{
    class Program
    {
        private static KinectSensor sensor;
        private static MultiSourceFrameReader multiReader;

        static void Main(string[] args)
        {
            bool isRunning = true;
            tracker.Tracker3D tracker = new tracker.Tracker3D(sensor, multiReader);
            while (isRunning) {
                float[] trackedData = tracker.Coordinates;
                Thread.Sleep(200);
            }
        }

        private void setupKinect()
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