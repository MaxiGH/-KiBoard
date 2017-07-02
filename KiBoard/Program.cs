using System.Threading;
using Microsoft.Kinect;
using System.Numerics;
using System.Windows.Forms;
using KiBoard.ui;
using KiBoard.inputManager;
using KiBoard.calibration;

namespace KiBoard
{
    class Program
    {
        private static KinectSensor sensor;
        private static MultiSourceFrameReader multiReader;

        private static ProgramState currentState;
        private static Calibrator calibrator;
        private static Tracker3D tracker;
        private static SpaceTranslator spaceTranslator;
        private static bool isRunning = true;
        private static InputManager inputManager;

        private static KiForm form;

        public static int FRAME_INTERVAL = 100;

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

            while (!form.IsHandleCreated)
            {
                Thread.Sleep(10);
            }

            while (isRunning && !form.shouldClose())
            {
                tick();
                Thread.Sleep(FRAME_INTERVAL);
            }

            System.Console.ReadKey();
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
                inputManager.processInput(spaceTranslator.translate(tracker.Coordinates));
                Vector3 vec = tracker.Coordinates;
                Vector3 translatedVec = spaceTranslator.translate(vec);
                inputManager.processInput(translatedVec);
            }
        }
    }
}
