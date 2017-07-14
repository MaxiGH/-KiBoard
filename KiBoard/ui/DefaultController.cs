using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KiBoard.inputManager;
using System.Drawing;

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
                    manager.activatePen();
                    break;
                case "Rubber":
                    manager.activateRubber();
                    break;
                case "Shapes":
                    break;
                case "Line":
                    manager.activateSegmentDrawing();
                    break;
                case "Ellipse":
                    manager.activateEllipseDrawing();
                    break;
                case "Coords":
                    manager.activateCoordsDrawing();
                    break;
                case "Colors":
                    break;
                case "White":
                    manager.changePenColor(Color.White);
                    break;
                case "Yellow":
                    manager.changePenColor(Color.Yellow);
                    break;
                case "Red":
                    manager.changePenColor(Color.Red);
                    break;
                case "Green":
                    manager.changePenColor(Color.Green);
                    break;
                case "Blue":
                    manager.changePenColor(Color.Blue);
                    break;
                case "Clear":
                    manager.clear();
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
