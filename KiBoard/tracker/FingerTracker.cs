using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Kinect;
using LightBuzz.Vitruvius;

namespace KiBoard
{
    class FingerTracker : Tracker
    {
        private System.Drawing.Graphics drawer;
        private KinectSensor sensor;
        private MultiSourceFrameReader multiReader;
        private Body[] bodyData;
        private CameraSpacePoint joint = new CameraSpacePoint();
        private ushort[] depthData;
        private const int SEARCH_FIELD = 10;
        private System.Numerics.Vector3 vec3;
        private System.Drawing.Bitmap bitmap = new System.Drawing.Bitmap(512, 424);
        private System.Drawing.Pen defaultPen = new System.Drawing.Pen(new System.Drawing.SolidBrush(System.Drawing.Color.White));


        public FingerTracker(KinectSensor sensor, MultiSourceFrameReader multiReader, KiBoard.ui.KiForm f)
        {
            this.drawer = f.CreateGraphics();
            this.sensor = sensor;
            this.multiReader = multiReader;
            Console.WriteLine("FingerTracker created!");
        }

        public System.Numerics.Vector3 Coordinates
        {
            get
            {
                if (multiReader != null)
                {
                    multiReader.MultiSourceFrameArrived += OnMultiSourceFrameArrived;
                    // Getting the latest Frameset
                }   
                //getFingerTipPoint(joint);
                return vec3;
            }
        }

        private System.Numerics.Vector3 getFingerTipPoint(CameraSpacePoint joint)
        {
            DepthSpacePoint depthPoint = sensor.CoordinateMapper.MapCameraPointToDepthSpace(joint);

            /*Console.WriteLine("count = " + depthData.Count());
            Console.WriteLine("access = " + (ushort)depthPoint.Y * 512 + (ushort)depthPoint.X);
            Console.WriteLine("accessX = " + depthPoint.X);
            Console.WriteLine("accessY = " + depthPoint.Y);
            */
            Console.WriteLine(depthData[1]);
            return new System.Numerics.Vector3();
        }

        private void OnMultiSourceFrameArrived(object sender, MultiSourceFrameArrivedEventArgs e)
        {
            MultiSourceFrameReference multiRef = e.FrameReference;
            if(multiRef == null)
            {
                Console.WriteLine("MultiRef is null!");
                return;
            }
            var frame = multiRef.AcquireFrame();
            if (frame != null)
            {
                BodyFrameReference bodyRef = frame.BodyFrameReference;
                if (bodyRef == null)
                {
                    //Console.WriteLine("no body");
                    return;
                }
                // Getting the latest bodyFrame
                var bodyFrame = bodyRef.AcquireFrame();
                if (bodyFrame != null)
                {
                    // Creates the the array with the number of tracked Bodyies
                    bodyData = new Body[bodyFrame.BodyFrameSource.BodyCount];

                    // Save the Body-Jointpositions to bodyData
                    bodyFrame.GetAndRefreshBodyData(bodyData);
                    bodyFrame.Dispose();
                    bodyFrame = null;
                }
                else { Console.WriteLine("BodyFrame is null!"); }
                DepthFrameReference depthRef = frame.DepthFrameReference;
                if (depthRef == null)
                {
                    Console.WriteLine("DepthREF is null!");
                    return;
                }
                DepthFrame depthFrame = depthRef.AcquireFrame();
                if (depthFrame == null)
                {
                    Console.WriteLine("DepthFrame is null!");
                    return;
                }
                int width = depthFrame.FrameDescription.Width;
                int height = depthFrame.FrameDescription.Height;

                depthData = new ushort[width * height];

                depthFrame.CopyFrameDataToArray(depthData);

                depthFrame.Dispose();
                depthFrame = null;
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

                joint.X = bodyData[index].Joints[JointType.HandRight].Position.X;
                joint.Y = bodyData[index].Joints[JointType.HandRight].Position.Y;
                joint.Z = bodyData[index].Joints[JointType.HandRight].Position.Z;
                drawFrame(depthData, joint);
            }
            else
            {
                System.Console.WriteLine("no body found");
            }
        
        }

        private int counter = 0;

        private void drawFrame(ushort[] dephData, CameraSpacePoint joint)
        {
            const int MAX_DISTANCE = 2000;
            const float MAX_DISTANCE_COLOR = 255.0f / 2400.0f;
            int count = 0;

            DepthSpacePoint depthPoint = sensor.CoordinateMapper.MapCameraPointToDepthSpace(joint);

            counter++;

            if (counter % 1000 == 0)
            {
                System.Console.WriteLine(counter);
                
                for (int i = 0; i < 424; i++)
                {
                    for (int j = 0; j < 512; j++)
                    {
                        int colorIdenticator = dephData[count];
                        int color = (int)(colorIdenticator * MAX_DISTANCE_COLOR);
                        if (color > 255) color = 255;
                        defaultPen.Color = System.Drawing.Color.FromArgb(255, color, color, color);
                        //drawer.DrawLine(defaultPen, new System.Drawing.Point(j, i), new System.Drawing.Point(j + 1, i));
                        bitmap.SetPixel(j, i, System.Drawing.Color.FromArgb(255, color, color, color));
                        count++;
                    }
                }
                if (depthPoint != null)
                {
                    bitmap.SetPixel((int)depthPoint.X, (int)depthPoint.Y, System.Drawing.Color.Red);
                    bitmap.SetPixel((int)depthPoint.X+1, (int)depthPoint.Y, System.Drawing.Color.Red);
                    bitmap.SetPixel((int)depthPoint.X, (int)depthPoint.Y+1, System.Drawing.Color.Red);
                    bitmap.SetPixel((int)depthPoint.X+1, (int)depthPoint.Y+1, System.Drawing.Color.Red);
                    bitmap.SetPixel((int)depthPoint.X+2, (int)depthPoint.Y, System.Drawing.Color.Red);
                    bitmap.SetPixel((int)depthPoint.X+2, (int)depthPoint.Y+1, System.Drawing.Color.Red);

                }

                drawer.DrawImage(bitmap, 0, 0);
            }
        }
    }
}
