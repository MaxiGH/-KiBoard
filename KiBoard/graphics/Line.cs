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
        public Color color;

        public Line()
        {
            width = 1.0f;
            smooth = true;
            color = Color.White;
            this.points = new List<Vector2>();
        }

        public Line(float width = 1.0f, bool smooth = true)
        {
            this.width = width;
            this.smooth = smooth;
            this.color = Color.White;
            this.points = new List<Vector2>();
        }

        public void nextPoint(Vector2 point)
        {
            points.Add(point);
        }

        public void draw(System.Drawing.Graphics g, Matrix3x3 mat)
        {
            if (points.Count < 2)
                return;
            PointF[] path = new PointF[points.Count];
            for (int i = 0; i < points.Count; i++)
            {
                Vector2 point = mat.transform(new Vector2(points[i].X, points[i].Y));
                path[i] = new PointF(point.X, point.Y);
            }

            Pen pen = new Pen(new SolidBrush(color));
            pen.LineJoin = System.Drawing.Drawing2D.LineJoin.Round;
            pen.Width = width;
            if (smooth)
                g.DrawCurve(pen, path);
            else
                g.DrawLines(pen, path);
        }
    }
}