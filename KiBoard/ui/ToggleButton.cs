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
        public ToggleButton(Vector2 pos, Vector2 s, Image btmUc, Image btmC) : base(pos, s, btmUc, btmC)
        {
        }

        new public void onClick()
        {
            isClicked = !isClicked;
        }

        new public void onClickReleased()
        {

        }
    }
}
