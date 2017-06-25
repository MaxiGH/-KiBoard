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

        private static STATE CURRENT_STATE = STATE.CALIBRATION_STATE;
        private static Calibrator calibrator;

        static void Main(string[] args)
        {
            calibrator = new InitialCalibrator();

            bool isRunning = true;
            tracker.Tracker3D tracker = new tracker.Tracker3D(sensor, multiReader);
            while (isRunning) {
                Vector3 trackedData = tracker.Coordinates;
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

        private void tick()
        {
            if (CURRENT_STATE == STATE.CALIBRATION_STATE)
            {
                calibrator.tick();
                if (calibrator.hasCalibrationPoints())
                {
                    spaceTranslator.processCalibrationPoints(calibrator.getCalibrationPoints());
                    CURRENT_STATE = STATE.RUNNING_STATE;
                }
            }
            if (CURRENT_STATE == STATE.RUNNING_STATE)
            {
                inputManager.processPoint(spaceTranslator.translate(tracker.Coordinates));
            }
        }
    }
}
