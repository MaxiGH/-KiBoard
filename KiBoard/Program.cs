using System.Threading;
using Microsoft.Kinect;
using System.Numerics;
using System.Windows.Forms;
using KiBoard.ui;
using KiBoard.inputManager;
using KiBoard.calibration;
using System.Drawing;
using System.Linq;

namespace KiBoard
{
    class Program
    {
        private static KinectSensor sensor;
        private static MultiSourceFrameReader multiReader;

        private static ProgramState currentState;
        private static Calibrator calibrator;
        private static Tracker tracker;
        private static SpaceTranslator spaceTranslator;
        private static bool isRunning = true;
        private static InputManager inputManager;
        private static Size formSize;
        private const int FRAME_INTERVAL = 50;

        private static KiForm form;

        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            currentState = ProgramState.CALIBRATION_STATE;
            form = new KiForm();
            Thread applicationThread = new Thread(runApplication);
            applicationThread.Start();

            Application.Run(form);

            isRunning = false;
        }

        private static void runApplication()
        {
            setupKinect();
            tracker = new Tracker3D(sensor, multiReader);
            calibrator = new KeyCalibrator(tracker);
            spaceTranslator = new SpaceTranslator();

            // wait for window-creation
            while (!form.IsHandleCreated)
            {
                Thread.Sleep(10);
            }

            formSize = form.Size;

            isRunning = true;
            System.Diagnostics.Stopwatch stopwatch = new System.Diagnostics.Stopwatch();

            while (isRunning && !form.shouldClose())
            {
                stopwatch.Start();

                tick();

                long elapsedMilliseconds = stopwatch.ElapsedMilliseconds;
                //System.Console.WriteLine("elapsed Milliseconds nach tick: " + elapsedMilliseconds);
                stopwatch.Reset();

                long waitTime = 0;
                if (elapsedMilliseconds < FRAME_INTERVAL)
                {
                    waitTime = FRAME_INTERVAL - elapsedMilliseconds;
                    Thread.Sleep((int)waitTime);
                }

                //System.Console.WriteLine("waitTime berechnet: " + waitTime);

                //double cpuAusl = (double)(elapsedMilliseconds / FRAME_INTERVAL) * 100;
                //System.Console.WriteLine("CPU Auslastung: " + cpuAusl + " %");
            }
            System.Console.ReadKey();
        }

        private static void setupKinect()
        {
            sensor = KinectSensor.GetDefault();
            multiReader = sensor.OpenMultiSourceFrameReader(FrameSourceTypes.Body | FrameSourceTypes.Depth);
            if (sensor != null)
            {
                multiReader = sensor.OpenMultiSourceFrameReader(FrameSourceTypes.Body | FrameSourceTypes.Depth);
                if (!sensor.IsOpen)
                {
                    sensor.Open();
                }
            }
        }

        private static void tick()
        {
            if (currentState == ProgramState.CALIBRATION_STATE)
            {
                calibrator.tick();
                if (calibrator.hasCalibrationPoints())
                {
                    spaceTranslator.processCalibrationPoints(calibrator.getCalibrationPoints());
                    inputManager = new InputManager(form);
                    currentState = ProgramState.RUNNING_STATE;
                }
            }
            else if (currentState == ProgramState.RUNNING_STATE)
            {
                if (!form.Size.Equals(formSize))
                {
                    formSize = form.Size;
                    inputManager.updateFormSize(formSize);
                }
                inputManager.processInput(spaceTranslator.translate(tracker.Coordinates));
            }
        }
    }
}
