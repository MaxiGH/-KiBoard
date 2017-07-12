using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace KiBoard.ui
{
    class ExpandToggleButton : ToggleButton
    {
        public List<LinkedToggleButton> Kiddies { get; private set; }

        public ExpandToggleButton(string name, Vector2 pos, Vector2 s, Image btmUc, Image btmC, bool isClicked = false, bool vsbl = true) : base(name, pos, s, btmUc, btmC, isClicked, vsbl)
        { 
        }

        public void setKiddies(List<LinkedToggleButton> kiddies)
        {
            Kiddies = kiddies;
            foreach(LinkedToggleButton btn in Kiddies)
            {
                btn.Visible = false;
            }
        }

        public override void onClick()
        {
            base.onClick();
            foreach(LinkedToggleButton btn in Kiddies){
                btn.Visible = isClicked;
            }
        }
    }
}
