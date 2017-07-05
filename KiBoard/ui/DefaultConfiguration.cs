using System;
using System.Collections.Generic;
using System.Drawing;
using System.Numerics;

namespace KiBoard.ui
{
    class DefaultConfiguration : UIConfiguration
    {
        public List<UIElement> createConfiguration()
        {
            List<UIElement> list = new List<UIElement>();

            list.Add(new Button(new Vector2(0.85f, 0.1f),
                     new Vector2(0.05f, 0.05f),
                     ResourceLoader.loadBitmap("pen.png")));
            return list;
        }
    }
}
