using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace KiBoard.ui
{
    class LinkedToggleButton : ToggleButton
    {
        public LinkedToggleButton Link { get; private set; }

        public LinkedToggleButton(string name, Vector2 pos, Vector2 s, Image btmUc, Image btmC, bool isClicked = false) : base(name, pos, s, btmUc, btmC, isClicked)
        {
        }

        public static void link(LinkedToggleButton btn1, LinkedToggleButton btn2)
        {
            btn1.Link = btn2;
            btn2.Link = btn1;
        }

        public override void onClick()
        {
            if (!isClicked) { 
                if (Link != null) { 
                    Link.isClicked = false;
                }
                isClicked = true;
            }
        }
    }
}
