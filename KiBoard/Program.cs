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
        private static Tracker3D tracker;
        private static SpaceTranslator spaceTranslator;
        //private static InputManager inputManager;

        static void Main(string[] args)
        {
            setupKinect();
            calibrator = new InitialCalibrator();
            tracker = new Tracker3D(sensor, multiReader);
            spaceTranslator = new SpaceTranslator();
            //inputManager = new InputManager();

            bool isRunning = true;
            System.Diagnostics.Stopwatch stopwatch = new System.Diagnostics.Stopwatch();

            while (isRunning) {
                stopwatch.Start();

                tick();

                //Thread.Sleep(40);
                long elapsedMilliseconds = stopwatch.ElapsedMilliseconds;
                //System.Console.WriteLine("elapsed Milliseconds nach tick: " + elapsedMilliseconds);
                stopwatch.Reset();

                long waitTime = 0;
                if (elapsedMilliseconds < 34)
                {
                    waitTime = 34 - elapsedMilliseconds;
                    Thread.Sleep((int)waitTime);
                }
                //System.Console.WriteLine("waitTime berechnet: " + waitTime);

                //double cpuAusl = (double)elapsedMilliseconds / 0.34;
                //System.Console.WriteLine("CPU Auslastung: " + cpuAusl + " %");

                if (System.Console.KeyAvailable)
                    isRunning = false;
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

        private static void tick()
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
                //inputManager.processPoint(spaceTranslator.translate(tracker.Coordinates));
                System.Console.WriteLine(spaceTranslator.translate(tracker.Coordinates));
            }
        }
    }
}
