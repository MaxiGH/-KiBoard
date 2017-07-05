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

            list.Add(new ToggleButton(new Vector2(0.75f, 0.16f),
                     new Vector2(0.1f, 0.1f),
                     ResourceLoader.loadBitmap("pen.png"),
                     ResourceLoader.loadBitmap("pen_selected.png")));
            list.Add(new ToggleButton(new Vector2(0.85f, 0.16f),
                     new Vector2(0.1f, 0.1f),
                     ResourceLoader.loadBitmap("rubber.png"),
                     ResourceLoader.loadBitmap("rubber_selected.png")));
            return list;
        }
    }
}
