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
                if (width * y + x >= 512 * 424)
                {
                    throw new Exception("x=" + x + " y=" + y + " w=" + width);
                }
                if (width * y + x < 0)
                {
                    throw new Exception("x=" + x + " y=" + y + " w=" + width);
                }
                return buffer[y * width + x];
            }

            public ushort getPoint(DepthSpacePoint joint)
            {
                if ((int)joint.Y * width + (int)joint.X < 0)
                    throw new Exception("y=" + joint.Y + " x=" + joint.X);
                if ((int)joint.Y * width + (int)joint.X >= 512*424)
                    throw new Exception("y=" + joint.Y + " x=" + joint.X);
                return buffer[(int)joint.Y * width + (int)joint.X];
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

        private KinectSensor sensor;
        private MultiSourceFrameReader multiReader;
        private Body[] bodyData;
        private CameraSpacePoint joint = new CameraSpacePoint();
        public const int MAX_TOP_RANGE = 100;
        public const int MIN_EDGE_DIF = 15;
        public const int PIXEL_RANGE = 5;
        public const int DIRECTION = -1;    // negative for Kinect on the left | positive for Kinect on the right
        private int counter = 0;

        private HandCollection hands;

        public FingerTracker(KinectSensor sensor, MultiSourceFrameReader multiReader)
        {
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
            if (multiRef == null)
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

                DepthSpacePoint depthPoint = sensor.CoordinateMapper.MapCameraPointToDepthSpace(joint);
                counter++;
                hands = scanForHands(buffer, depthPoint);
            }
            else
            {
                hands = new HandCollection(new Hand(), new Hand());
                System.Console.WriteLine("no body found");
            }
        }

        private HandCollection scanForHands(DepthBuffer buffer, DepthSpacePoint depthPoint)
        {
            if ((depthPoint.Y < 0.0f) || (depthPoint.Y >= 424.0f))
            {
                return new HandCollection(new Hand(), new Hand());
            }

            int jointEdgeY = getJointEdge(buffer, (int)depthPoint.X, (int)depthPoint.Y);
            if (jointEdgeY == -1)
            {
                return new HandCollection(new Hand(), new Hand());
            }
            DepthSpacePoint tip = searchFingerTip(buffer, (int)(depthPoint.X), jointEdgeY);
            CameraSpacePoint p = sensor.CoordinateMapper.MapDepthPointToCameraSpace(tip, buffer.getPoint(tip));
            return new HandCollection(new Hand(), new Hand(new System.Numerics.Vector3(p.X, p.Y, p.Z)));
        }

        private DepthSpacePoint searchFingerTip(DepthBuffer buffer, int x, int y)
        {
            DepthSpacePoint point = new DepthSpacePoint();
            DepthSpacePoint sparePoint = new DepthSpacePoint();
            point.X = x;
            point.Y = y;
            int count = 0;
            while (count<120) {
                sparePoint = nextLeftPoint(buffer, point);
                if (sparePoint.X == -1.0f)
                {
                    break;
                }
                point = sparePoint;
                count++;
            }
            point.X += DIRECTION * 2.0f;
            return point;
        }

        private DepthSpacePoint nextLeftPoint(DepthBuffer buffer, DepthSpacePoint point)
        {
            int y = Math.Max((int)point.Y - PIXEL_RANGE, 0);
            int x = Math.Min(Math.Max((int)point.X - DIRECTION, 0), buffer.getWidth());
            ushort depth = buffer.getPoint(x,y);
            for(int i = 1; i < Math.Min(PIXEL_RANGE * 2, 424-y); i++)
            {
                if (Math.Abs(buffer.getPoint(x, y + i) - depth) > 20)
                {
                    point.X = x;
                    point.Y = y+i;
                    return point;
                }
            }
            point.X = -1.0f;
            return point; 
        }

        private int getJointEdge(DepthBuffer buffer, int x, int y)
        {
            ushort depth = buffer.getPoint(x, y);

            int result = -1;
            for (int i = 10; i < MAX_TOP_RANGE; i++)
            {
                if (y - i >= 0)
                {
                    if (Math.Abs(buffer.getPoint(x, y - i) - depth) > MIN_EDGE_DIF) // Kante gefunden
                    {
                        result = y - i;
                        break;
                    }
                    depth = buffer.getPoint(x, y - i);
                }
            }
            return result;
        }
    }
}
