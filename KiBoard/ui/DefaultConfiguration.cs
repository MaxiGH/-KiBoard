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

            list.Add(new ToggleButton("Pen", new Vector2(0.6f, 0.1f),
                     new Vector2(0.1f, 0.1f),
                     ResourceLoader.loadBitmap("pen.jpg"),
                     ResourceLoader.loadBitmap("pen_chosen.jpg")));
            list.Add(new ToggleButton("Rubber", new Vector2(0.7f, 0.1f),
                     new Vector2(0.1f, 0.1f),
                     ResourceLoader.loadBitmap("rubber.jpg"),
                     ResourceLoader.loadBitmap("rubber_chosen.jpg")));
            list.Add(new Button("Undo", new Vector2(0.8f, 0.1f),
                     new Vector2(0.1f, 0.1f),
                     ResourceLoader.loadBitmap("undo.jpg"),
                     ResourceLoader.loadBitmap("undo.jpg")));
            list.Add(new Button("Redo", new Vector2(0.9f, 0.1f),
                     new Vector2(0.1f, 0.1f),
                     ResourceLoader.loadBitmap("redo.jpg"),
                     ResourceLoader.loadBitmap("redo.jpg")));
            list.Add(new Button("Save", new Vector2(0.0f, 0.1f),
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
