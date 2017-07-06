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
            System.Drawing.Drawing2D.GraphicsPath path = new System.Drawing.Drawing2D.GraphicsPath();
            for (int i = 0; i < points.Count - 1; i++)
            {
                Vector2 from = mat.transform(new Vector2(points[i].X, 1.0f - points[i].Y));
                Vector2 to = mat.transform(new Vector2(points[i + 1].X, 1.0f - points[i + 1].Y));
                path.AddLine(new PointF(from.X, from.Y), new PointF(to.X, to.Y));
            }

            Pen pen = new Pen(new SolidBrush(color));
            pen.LineJoin = System.Drawing.Drawing2D.LineJoin.Round;
            pen.Width = width;
            if (smooth)
                try { g.DrawCurve(pen, path.PathPoints); }catch(Exception e) { }
            else
                g.DrawPath(pen, path);
        }
    }
}