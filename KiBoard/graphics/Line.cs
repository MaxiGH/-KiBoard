using System;
using System.Collections.Generic;
using System.Numerics;

namespace KiBoard
{
    public class Line : Drawable
    {
        public List<Vector2> points;
        public bool smooth;

        public Line()
        {
            points = new List<Vector2>();
            this.smooth = false;
        }

        public Line(bool smooth)
        {
            this.points = new List<Vector2>();
            this.smooth = smooth;
        }

        public void nextPoint(Vector2 point)
        {
            points.Add(point);
        }

        public void draw()
        {
            
        }
    }
}