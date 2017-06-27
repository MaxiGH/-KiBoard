using System;
using System.Threading;
using Microsoft.Kinect;
using System.Numerics;
using System.Windows.Forms;
using KiBoard.ui;

namespace KiBoard
{
    class Program
    {
        private static KinectSensor sensor;
        private static MultiSourceFrameReader multiReader;

        [STAThread]
        static void Main(string[] args)
        {
            bool isRunning = true;
            setupKinect();
            Tracker3D tracker = new Tracker3D(sensor, multiReader);
            while (isRunning) {
                Vector3 trackedData = tracker.Coordinates;
                Thread.Sleep(200);
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
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
