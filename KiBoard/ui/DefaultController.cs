using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KiBoard.inputManager;

namespace KiBoard.ui
{
    class DefaultController : UIController
    {
        public DefaultController(InputManager m) : base(m)
        {
        }

        public override void onClick(string elementName)
        {
            switch (elementName)
            {
                case "Pen":
                    manager.createTestMessagebox();
                    break;
                case "Rubber":
                    break;
                case "Undo":
                    break;
                case "Redo":
                    break;
                case "Save":
                    break;
            }
        }
    }
}
