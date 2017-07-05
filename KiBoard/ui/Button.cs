using System;
using System.Drawing;
using System.Numerics;

namespace KiBoard.ui
{
    class Button : UIElement
    {
        private Vector2 position;
        private Vector2 size;
        private Image bitmap;
        private bool visible;

        public Button(Vector2 pos, Vector2 s, Image btm)
        {
            position = pos;
            size = s;
            bitmap = btm;
        }

        public void render(Graphics gfx, Size windowSize)
        {
            if (visible)
            {
                    Rectangle renderRect = new Rectangle(
                        new Point((int)(windowSize.Width * position.X), (int)(windowSize.Height - (windowSize.Height * position.Y))),
                        new Size((int)(windowSize.Width * size.X), (int)(windowSize.Height * size.Y))
                        );
                gfx.DrawImage(bitmap, renderRect);
            }
        }

        public void setVisible(bool v)
        {
            visible = v;
        }

        public bool touches(Vector2 vec)
        {
            if ((position.X < vec.X) && (position.Y < vec.Y))
            {
                return ((position.X + size.X > vec.X) && (position.Y + size.Y > vec.Y));
            }
            return false;
        }
    }
}
