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
        private List<LinkedToggleButton> Links { get; set; }

        public LinkedToggleButton(string name, Vector2 pos, Vector2 s, Image btmUc, Image btmC, bool isClicked = false, bool vsbl = true) : base(name, pos, s, btmUc, btmC, isClicked, vsbl)
        {
        }

        public static void link(List<LinkedToggleButton> btns)
        {
            foreach (LinkedToggleButton btn in btns)
            {
                btn.Links = btns;
            }
        }

        public override void onClick()
        {
            if (!isClicked) {
                if (Links != null) {
                    foreach (LinkedToggleButton btn in Links)
                    {
                        btn.IsClicked = false;
                    }
                }
                isClicked = true;
            }
        }
    }
}
