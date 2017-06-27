using System;
using System.Numerics;

namespace KiBoard
{
    public interface Drawable
    {
        //Vector2 FirstPoint { get; set; }
        //Vector2 LastPoint { get; set; }
        void nextPoint(Vector2 v);
        void draw();
    }
}