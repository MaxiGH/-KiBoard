using System.Drawing;
using System.Numerics;

namespace KiBoard.ui
{
    public abstract class UIElement
    {
        private Vector2 position;
        private Vector2 size;
        private bool visible;

        public bool Visible
        {
            get { return visible; }
            set { visible = value;  }
        }

        private string name;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public UIElement(string n, Vector2 pos, Vector2 si)
        {
            name = n;
            position = pos;
            size = si;
            visible = false;
        }

        public abstract void render(Graphics gfx, Size windowSize);

        protected void draw(Graphics gfx, Image bitmap, Size windowSize)
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

        public bool touches(Vector2 vec)
        {
            if ((vec.X > position.X) && (vec.Y < position.Y))
            {
                if ((vec.X < position.X + size.X) && (vec.Y > position.Y - size.Y))
                {
                    return true;
                }
            }
            return false;
        }

        public abstract void onClick();
        public abstract void onClickReleased();
    }
}
