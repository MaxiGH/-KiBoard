using System;
using System.Numerics;

namespace KiBoard
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

        public void draw()
        {

        }
    }
}