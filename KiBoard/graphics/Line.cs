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

        public Line(List<Vector2> points, bool smooth)
        {
            this.points = points;
            this.smooth = smooth;
        }

        public void draw()
        {
            
        }
    }
}