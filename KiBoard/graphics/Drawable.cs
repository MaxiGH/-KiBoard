using System;
using System.Numerics;
using KiBoard.math;

namespace KiBoard.graphics
{
    public interface Drawable
    {
        //Vector2 FirstPoint { get; set; }
        //Vector2 LastPoint { get; set; }
        void nextPoint(Vector2 v);
        void draw(System.Drawing.Graphics g, Matrix3x3 scale);
    }
}