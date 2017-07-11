using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace KiBoard.ui
{
    class ToggleButton : Button
    {
        public ToggleButton(string name, Vector2 pos, Vector2 s, Image btmUc, Image btmC, bool isClicked = false)
            : base(name, pos, s, btmUc, btmC, isClicked)
        { }

        public override void onClick()
        {
            isClicked = !isClicked;
        }

        public override void onClickReleased() {}
    }
}
