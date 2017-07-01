using System;
using System.Numerics;
using System.Drawing;
using KiBoard.math;

namespace KiBoard.graphics
{
    public class Segment : Drawable
    {
        public float width;
        public Vector2 from;
        public Vector2 to;

        public Segment()
        {
            width = 1.0f;
            from = new Vector2(0, 0);
            to = new Vector2(0, 0);
        }

        public Segment(Vector2 from, Vector2 to, float width = 1.0f)
        {
            this.width = width;
            this.from = from;
            this.to = to;
        }

        public void nextPoint(Vector2 point)
        {
            if (from == null)
                from = point;
            else
                to = point;
        }

        public void draw(System.Drawing.Graphics g, Matrix3x3 mat)
        {
            Vector2 v0 = mat.transform(from);
            Vector2 v1 = mat.transform(to);

            g.DrawLine(
                new Pen(Brushes.Black, width),
                new PointF(v0.X, v0.Y),
                new PointF(v1.X, v1.Y));
        }
    }
}