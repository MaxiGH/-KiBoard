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

            LinkedToggleButton pen = new LinkedToggleButton("Pen", new Vector2(0.5f, 0.16f),
                     new Vector2(0.1f, 0.1f),
                     ResourceLoader.loadBitmap("pen.jpg"),
                     ResourceLoader.loadBitmap("pen_chosen.jpg"), true);
            LinkedToggleButton rubber = new LinkedToggleButton("Rubber", new Vector2(0.6f, 0.16f),
                     new Vector2(0.1f, 0.1f),
                     ResourceLoader.loadBitmap("rubber.jpg"),
                     ResourceLoader.loadBitmap("rubber_chosen.jpg"));
            LinkedToggleButton.link(pen, rubber);

            list.Add(pen);
            list.Add(rubber);
            list.Add(new Button("Clear", new Vector2(0.7f, 0.16f),
                     new Vector2(0.1f, 0.1f),
                     ResourceLoader.loadBitmap("undo.png"),
                     ResourceLoader.loadBitmap("undo_chosen.png")));
            list.Add(new Button("Undo", new Vector2(0.8f, 0.16f),
                     new Vector2(0.1f, 0.1f),
                     ResourceLoader.loadBitmap("undo.png"),
                     ResourceLoader.loadBitmap("undo_chosen.png")));
            list.Add(new Button("Redo", new Vector2(0.9f, 0.16f),
                     new Vector2(0.1f, 0.1f),
                     ResourceLoader.loadBitmap("redo.png"),
                     ResourceLoader.loadBitmap("redo_chosen.png")));
            list.Add(new Button("Save", new Vector2(0.0f, 0.16f),
                     new Vector2(0.1f, 0.1f),
                     ResourceLoader.loadBitmap("save.jpg"),
                     ResourceLoader.loadBitmap("save.jpg")));
            return list;
        }

        public UIController createController(inputManager.InputManager manager)
        {
            return (UIController) new DefaultController(manager);
        }
    }
}
