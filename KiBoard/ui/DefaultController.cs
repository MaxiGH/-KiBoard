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
                    manager.activatePen();
                    break;
                case "Rubber":
                    manager.activateRubber();
                    break;
                case "Undo":
                    manager.undo();
                    break;
                case "Redo":
                    manager.redo();
                    break;
                case "Save":
                    manager.save();
                    break;
            }
        }
    }
}
