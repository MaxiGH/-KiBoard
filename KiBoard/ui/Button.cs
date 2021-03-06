﻿using System;
using System.Drawing;
using System.Numerics;

namespace KiBoard.ui
{
    class Button : UIElement
    {
        private Image bitmapUnclicked;
        private Image bitmapClicked;
        protected bool isClicked;

        public bool IsClicked {
            get { return isClicked; }
            set { isClicked = value; }
        }

        public Button(string name, Vector2 pos, Vector2 size, Image btmUc, Image btmC, bool isClicked = false, bool vsbl = true) : base(name, pos, size, vsbl)
        {
            bitmapUnclicked = btmUc;
            bitmapClicked = btmC;
            this.isClicked = isClicked;
        }

        public override void render(Graphics gfx, Size windowSize)
        {
            if (isClicked)
                draw(gfx, bitmapClicked, windowSize);
            else
                draw(gfx, bitmapUnclicked, windowSize);
        }

        public override void onClick()
        {
            isClicked = true;
            System.Console.WriteLine("Button is clicked.");
        }

        public override void onClickReleased()
        {
            isClicked = false;
            System.Console.WriteLine("Button is released.");
        }
    }
}
