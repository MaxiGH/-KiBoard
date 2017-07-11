using System;
using System.Numerics;
using System.Drawing;
using KiBoard.math;

namespace KiBoard.graphics
{
    public class Ellipse : Drawable
    {
        public float width;
        public Vector2 from;
        public Vector2 to;
        public Color color;
        int numPointsSet;

        public Ellipse()
        {
            width = 1.0f;
            from = new Vector2(0, 0);
            to = new Vector2(0, 0);
            color = Color.White;
            numPointsSet = 0;
        }

        public Ellipse(Vector2 from, Vector2 to, float width = 1.0f)
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

                // System.Console.WriteLine("{0}|{1} {2}|{3}", v0.X, v0.Y, v1.X, v1.Y);
                g.DrawEllipse(
                    new Pen(new SolidBrush(color), width),
                    new Rectangle(
                        new Point((int)v0.X, (int)v0.Y),
                        new Size(
                            (int)(v1.X - v0.X),
                            (int)(v1.Y - v0.Y)))
                    );
            }
        }
    }
}