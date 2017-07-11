using System;
using System.Collections.Generic;
using System.Drawing;
using System.Numerics;

namespace KiBoard.ui
{
    class DefaultConfiguration : UIConfiguration
    {
        public List<UIElement> createConfiguration()
        {
            List<UIElement> list = new List<UIElement>();

            LinkedToggleButton pen = new LinkedToggleButton("Pen", new Vector2(0.5f, 0.1f),
                     new Vector2(0.1f, 0.1f),
                     ResourceLoader.loadBitmap("pen.png"),
                     ResourceLoader.loadBitmap("pen_chosen.png"), true);
            LinkedToggleButton rubber = new LinkedToggleButton("Rubber", new Vector2(0.6f, 0.1f),
                     new Vector2(0.1f, 0.1f),
                     ResourceLoader.loadBitmap("rubber.png"),
                     ResourceLoader.loadBitmap("rubber_chosen.png"));
            LinkedToggleButton.link(pen, rubber);

            list.Add(pen);
            list.Add(rubber);
            list.Add(new Button("Clear", new Vector2(0.7f, 0.1f),
                     new Vector2(0.1f, 0.1f),
                     ResourceLoader.loadBitmap("clear.png"),
                     ResourceLoader.loadBitmap("clear_chosen.png")));
            list.Add(new Button("Undo", new Vector2(0.8f, 0.1f),
                     new Vector2(0.1f, 0.1f),
                     ResourceLoader.loadBitmap("undo.png"),
                     ResourceLoader.loadBitmap("undo_chosen.png")));
            list.Add(new Button("Redo", new Vector2(0.9f, 0.1f),
                     new Vector2(0.1f, 0.1f),
                     ResourceLoader.loadBitmap("redo.png"),
                     ResourceLoader.loadBitmap("redo_chosen.png")));
            list.Add(new Button("Save", new Vector2(0.0f, 0.1f),
                     new Vector2(0.1f, 0.1f),
                     ResourceLoader.loadBitmap("save.png"),
                     ResourceLoader.loadBitmap("save_chosen.png")));
            return list;
        }

        public UIController createController(inputManager.InputManager manager)
        {
            return (UIController) new DefaultController(manager);
        }
    }
}
