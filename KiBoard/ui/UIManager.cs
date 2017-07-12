using System;
using System.Collections.Generic;
using System.Drawing;
using System.Numerics;
using KiBoard.graphics;

namespace KiBoard.ui
{
    class UIManager
    {
        private List<UIElement> elements;
        private FrameBuffer frame;

        public UIManager(UIConfiguration conf, FrameBuffer frame)
        {
            elements = conf.createConfiguration();
            this.frame = frame;
        }

        public void render()
        {
            foreach (UIElement element in elements)
            { 
                //System.Console.WriteLine("{0} {1}", frame.Size.X, frame.Size.Y);
                element.render(frame.graphics(), new Size((int)frame.Size.X, (int)frame.Size.Y));
            }
        }

        public bool isTouchingElement(Vector2 vec)
        {
            return getTouchingElement(vec) != null;
        }

        public UIElement getTouchingElement(Vector2 vec)
        {
            foreach (UIElement element in elements)
            {
                if (element.touches(vec))
                {
                    return element;
                }
            }
            return null;
        }

        public void onStartWrite()
        {
            foreach (UIElement e in elements)
            {
                e.onStartWrite();
            }
        }
    }
}
