using System;
using System.Collections.Generic;
using System.Numerics;
using System.Drawing;
using KiBoard.math;

namespace KiBoard.graphics
{
    public class Line : Drawable
    {
        public List<Vector2> points;
        public bool smooth;
        public float width;

        public Line(float width = 1.0f, bool smooth = true)
        {
            this.width = width;
            this.smooth = smooth;
            this.points = new List<Vector2>();
        }

        public void nextPoint(Vector2 point)
        {
            points.Add(point);
        }

        public void draw(System.Drawing.Graphics g, Matrix3x3 mat)
        {
            System.Drawing.Drawing2D.GraphicsPath path = new System.Drawing.Drawing2D.GraphicsPath();
            for (int i = 0; i < points.Count - 1; i++)
            {
                Vector2 from = mat.transform(points[i]);
                Vector2 to = mat.transform(points[i + 1]);
                path.AddLine(new PointF(from.X, from.Y), new PointF(to.X, to.Y));
            }

            Pen pen = new Pen(Brushes.Black);
            pen.Width = width;
            if (smooth)
                pen.LineJoin = System.Drawing.Drawing2D.LineJoin.Round;
            g.DrawPath(pen, path);
        }
    }
}