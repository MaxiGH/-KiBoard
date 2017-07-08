using KiBoard.inputManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KiBoard.ui
{
    public abstract class UIController
    {
        protected InputManager manager;

        public UIController(InputManager m)
        {
            manager = m;
        }

        public abstract void onClick(string elementName);
    }
}
