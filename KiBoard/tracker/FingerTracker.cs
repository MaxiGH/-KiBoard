using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Kinect;
using LightBuzz.Vitruvius;

namespace KiBoard.tracker
{
    class FingerTracker : Tracker
    {
        private class Edge
        {
            public List<System.Drawing.Point> points;
            public void draw(System.Drawing.Bitmap bitmap)
            {
                foreach (System.Drawing.Point p in points)
                    bitmap.SetPixel(p.X, p.Y, System.Drawing.Color.Blue);
            }
        }

        private class DepthBuffer
        {
            private ushort[] buffer;
            private int width;
            private int height;

            public DepthBuffer(int w, int h)
            {
                width = w;
                height = h;
                buffer = new ushort[w * h];
            }

            public ushort getPoint(int x, int y)
            {
                return buffer[y * height + x];
            }

            public ushort getPoint(CameraSpacePoint joint)
            {
                return buffer[(int)joint.Y * height + (int)joint.X];
            }

            public ushort[] getBuffer()
            {
                return buffer;
            }

            public ushort get(int index)
            {
                return buffer[index];
            }

            public int getWidth()
            {
                return width;
            }

            public int getHeight()
            {
                return height;
            }
        }

        private System.Drawing.Graphics drawer;
        private KinectSensor sensor;
        private MultiSourceFrameReader multiReader;
        private Body[] bodyData;
        private CameraSpacePoint joint = new CameraSpacePoint();
        public const int SCAN_EDGE_RANGE = 20;
        public const int MIN_EDGE_DIF = 10;

        private HandCollection hands;
        private DirectBitmap bitmap = new DirectBitmap(512, 424);
        private System.Drawing.Pen defaultPen = new System.Drawing.Pen(new System.Drawing.SolidBrush(System.Drawing.Color.White));

        public FingerTracker(KinectSensor sensor, MultiSourceFrameReader multiReader, KiBoard.ui.KiForm f)
        {
            this.drawer = f.CreateGraphics();
            this.sensor = sensor;
            this.multiReader = multiReader;
            // zuvor in getHandCollection
            if (multiReader != null)
            {
                multiReader.MultiSourceFrameArrived += OnMultiSourceFrameArrived;
            }
            hands = new HandCollection(new Hand(), new Hand());
            Console.WriteLine("FingerTracker created!");
        }

        public HandCollection getHandCollection()
        {
            return hands;
        }

        private void OnMultiSourceFrameArrived(object sender, MultiSourceFrameArrivedEventArgs e)
        {
            MultiSourceFrameReference multiRef = e.FrameReference;
            if(multiRef == null)
            {
                Console.WriteLine("MultiRef is null!");
                return;
            }

            DepthBuffer buffer = null;

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
                buffer = new DepthBuffer(depthFrame.FrameDescription.Width, depthFrame.FrameDescription.Height);

                depthFrame.CopyFrameDataToArray(buffer.getBuffer());

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
                    break;
                }
            }

            if ((index > -1) && (buffer != null))
            {
                // We use the right Hand

                joint.X = bodyData[index].Joints[JointType.HandRight].Position.X;
                joint.Y = bodyData[index].Joints[JointType.HandRight].Position.Y;
                joint.Z = bodyData[index].Joints[JointType.HandRight].Position.Z;

                // hands = scanForHands(buffer, joint);

                drawFrame(buffer, joint);
            }
            else
            {
                System.Console.WriteLine("no body found");
            }
        
        }

        private HandCollection scanForHands(DepthBuffer depthData, CameraSpacePoint joint)
        {
            List<Edge> handEdges = scanHandEdges(depthData, joint);
            throw new NotImplementedException();
        }

        private List<Edge> scanHandEdges(DepthBuffer buffer, CameraSpacePoint joint)
        {
            throw new NotImplementedException();
        }

        private int counter = 0;

        private void drawFrame(DepthBuffer dephData, CameraSpacePoint joint)
        {
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
                        int colorIdenticator = dephData.get(count);
                        int color = (int)(colorIdenticator * MAX_DISTANCE_COLOR);
                        if (color > 255)
                            color = 255;
                        defaultPen.Color = System.Drawing.Color.FromArgb(255, color, color, color);
                        //drawer.DrawLine(defaultPen, new System.Drawing.Point(j, i), new System.Drawing.Point(j + 1, i));
                        bitmap.SetPixel(j, i, System.Drawing.Color.FromArgb(255, color, color, color));
                        count++;
                    }
                }
                if (depthPoint != null)
                {
                    drawPoint(depthPoint, System.Drawing.Color.Red);
                }

                drawer.DrawImage(bitmap.Bitmap, 0, 0);
            }
        }

        private void drawPoint(DepthSpacePoint depthPoint, System.Drawing.Color color)
        {
            bitmap.SetPixel((int)depthPoint.X, (int)depthPoint.Y, color);
            bitmap.SetPixel((int)depthPoint.X + 1, (int)depthPoint.Y, color);
            bitmap.SetPixel((int)depthPoint.X, (int)depthPoint.Y + 1, color);
            bitmap.SetPixel((int)depthPoint.X - 1, (int)depthPoint.Y, color);
            bitmap.SetPixel((int)depthPoint.X, (int)depthPoint.Y - 1, color);
        }
    }
}
