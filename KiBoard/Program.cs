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

        private static bool shouldStop;
        private static ProgramState currentState;
        private static Calibrator calibrator;
        private static tracker.Tracker tracker;
        private static SpaceTranslator spaceTranslator;
        private static InputManager inputManager;
        private static Size formSize;
        private const int FRAME_INTERVAL = 35;
        private static System.Drawing.Graphics gfx;

        private static KiForm form;

        static void Main(string[] args)
        {
            shouldStop = false;

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            currentState = ProgramState.CALIBRATION_STATE;

            form = new KiForm();
            form.FormBorderStyle = FormBorderStyle.None;
            form.WindowState = FormWindowState.Maximized;

            Thread applicationThread = new Thread(runApplication);
            applicationThread.Start();

            Application.Run(form);

            shouldStop = true;
        }

        private static void runApplication()
        {
            setupKinect();
            drawer = new System.Drawing.Bitmap(512, 424);
            tracker = new tracker.FingerTracker(sensor, multiReader);
            calibrator = new KeyCalibrator(tracker);
            spaceTranslator = new SpaceTranslator();

            // wait for window-creation
            while (!form.IsHandleCreated)
            {
                Thread.Sleep(50);
            }

            gfx = form.CreateGraphics();

            formSize = form.Size;

            shouldStop = false;
            System.Diagnostics.Stopwatch stopwatch = new System.Diagnostics.Stopwatch();

            while (!shouldStop && !form.ShouldClose)
            {
                stopwatch.Start();

                tick();
                graphics.MessageBox.tick();

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
        }

        private static void setupKinect()
        {
            sensor = KinectSensor.GetDefault();
            if (sensor != null)
            {
                multiReader = sensor.OpenMultiSourceFrameReader(FrameSourceTypes.Depth | FrameSourceTypes.Body);
                if (!sensor.IsOpen)
                {
                    sensor.Open();
                }
            }
            else
            {
                throw new System.Exception("sensor == null");
            }
        }

        private static void tick()
        {
            if (currentState == ProgramState.CALIBRATION_STATE)
            {
                if (form.isKeyPressed())
                {
                    calibrator.keyPressed();
                }
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
                Vector3 vec = tracker.getHandCollection().right.jointCoordinate;
                Vector3 translatedVec = spaceTranslator.translate(vec);
                inputManager.processInput(translatedVec);
                System.Console.WriteLine("vec=" + vec.ToString() + "     translatedVec=" + translatedVec.ToString());
            }

            //System.Numerics.Vector3 vec = tracker.getHandCollection().right.jointCoordinate;
            gfx.DrawImage(drawer, 0, 0);
        }
    }
}
