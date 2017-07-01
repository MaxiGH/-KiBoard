using System;
using System.Threading;
using Microsoft.Kinect;
using System.Numerics;
using System.Windows.Forms;
using KiBoard.ui;
using KiBoard.graphics;
using KiBoard.inputManager;

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
        private static bool isRunning = true;
        //private static InputManager inputManager;

        private static Form form;

        public static int FRAME_INTERVAL = 100;

        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            form = new Form1();

            Thread applicationThread = new Thread(runApplication);
            applicationThread.Start();

            Application.Run(form);

            isRunning = false;
        }

        private static void runApplication()
        {
            /*
            setupKinect();
            calibrator = new InitialCalibrator();
            tracker = new Tracker3D(sensor, multiReader);
            spaceTranslator = new SpaceTranslator();
            //inputManager = new InputManager();

            //Graphics g = new Graphics(form.CreateGraphics());
            /*

            while (isRunning)
            {
                tick();
                g.render();
                Thread.Sleep(FRAME_INTERVAL);
                if (System.Console.KeyAvailable)
                    isRunning = false;
            }*/
            TestInputManager test = new TestInputManager(form.CreateGraphics());
            test.test();
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
                Vector3 vec = tracker.Coordinates;
                Vector3 translatedVec = spaceTranslator.translate(vec);
                System.Console.WriteLine("kinectSpace=" + vec.ToString() + "\twallSpace=" + translatedVec.ToString());

                // move into InputManager
                const int WIDTH = 20;
                const int HEIGHT = 10;
                int x = (int)(WIDTH * translatedVec.X);
                int y = 10 - (int)(HEIGHT * translatedVec.Y);

                for (int iy = 0; iy < HEIGHT; iy++)
                {
                    for (int ix = 0; ix < WIDTH; ix++)
                    {
                        if ((ix == x) && (iy == y))
                        {
                            if (translatedVec.Z > 0.06f)
                                System.Console.Write("O");
                            else
                                System.Console.Write("X");
                        }
                        else
                        {
                            System.Console.Write("_");
                        }
                    }
                    System.Console.WriteLine("");
                }
                System.Console.WriteLine("\n");
            }
        }
    }
}
