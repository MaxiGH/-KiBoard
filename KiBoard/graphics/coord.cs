using System;
using System.Numerics;
using System.Drawing;
using KiBoard.math;

namespace KiBoard.graphics
{
    public class Coord : Drawable
    {
        public float width;
        public Vector2 from;
        public Vector2 to;
        public Color color;
        int numPointsSet;

        public Coord()
        {
            width = 1.0f;
            from = new Vector2(0, 0);
            to = new Vector2(0, 0);
            color = Color.White;
            numPointsSet = 0;
        }

        public Coord(Vector2 from, Vector2 to, float width = 1.0f)
        {
            this.width = width;
            this.from = from;
            this.to = to;
            numPointsSet = 2;
            this.color = Color.White;
        }

        public void nextPoint(Vector2 point)
        {
            if (numPointsSet == 0)
                from = point;
            else
                to = point;
            if (numPointsSet < 2)
                numPointsSet++;
        }

        public void draw(System.Drawing.Graphics g, Matrix3x3 mat)
        {
            if (numPointsSet == 2)
            {
                Vector2 v0 = mat.transform(new Vector2(from.X, from.Y));
                Vector2 v1 = mat.transform(new Vector2(to.X, to.Y));

                // make v0 be the bottom left corner and v1 top right
                if (v0.X > v1.X && v0.Y < v1.Y)
                {
                    float temp = v0.X;
                    v0.X = v1.X;
                    v1.X = temp;
                }
                else if (v0.X < v1.X && v0.Y > v1.Y)
                {
                    float temp = v0.Y;
                    v0.Y = v1.Y;
                    v1.Y = temp;
                }
                else if (v0.X > v1.X && v0.Y > v1.Y)
                {
                    Vector2 temp = v0;
                    v0 = v1;
                    v1 = temp;
                }

                // System.Console.WriteLine("{0}|{1} {2}|{3}", v0.X, v0.Y, v1.X, v1.Y);
                Pen pen = new Pen(new SolidBrush(color), width);

                int centerX = (int)(v0.X / 2 + v1.X / 2);
                g.DrawLine(pen,
                    new Point(centerX, (int)v0.Y),
                    new Point(centerX, (int)v1.Y));

                int centerY = (int)(v0.Y / 2 + v1.Y / 2);
                g.DrawLine(pen,
                    new Point((int)v0.X, centerY),
                    new Point((int)v1.X, centerY));
            }
        }
    }
}