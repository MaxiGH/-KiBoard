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
            try {
                list.Add(new Button(new Vector2(0.15f, 0.15f), new Vector2(0.1f, 0.1f), Bitmap.FromFile("resources/Pen.png")));
            }
            catch (System.IO.FileNotFoundException e)
            {
                Console.WriteLine("couldnt load \"resources/Pen.png\"");
            }
            return list;
        }
    }
}
