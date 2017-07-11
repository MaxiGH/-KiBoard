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

            LinkedToggleButton pen = new LinkedToggleButton("Pen", new Vector2(0.3f, 0.1f),
                     new Vector2(0.1f, 0.1f),
                     ResourceLoader.loadBitmap("pen.png"),
                     ResourceLoader.loadBitmap("pen_chosen.png"), true);
            LinkedToggleButton rubber = new LinkedToggleButton("Rubber", new Vector2(0.4f, 0.1f),
                     new Vector2(0.1f, 0.1f),
                     ResourceLoader.loadBitmap("rubber.png"),
                     ResourceLoader.loadBitmap("rubber_chosen.png"));

            ExpandToggleButton shapes = new ExpandToggleButton("Shapes", new Vector2(0.5f, 0.1f),
                     new Vector2(0.1f, 0.1f),
                     ResourceLoader.loadBitmap("rubber.png"),
                     ResourceLoader.loadBitmap("rubber_chosen.png"));

            LinkedToggleButton line = new LinkedToggleButton("Line", new Vector2(0.5f, 0.2f),
                     new Vector2(0.1f, 0.1f),
                     ResourceLoader.loadBitmap("line.png"),
                     ResourceLoader.loadBitmap("line_chosen.png"));
            line.Visible = false;
            LinkedToggleButton ellipse = new LinkedToggleButton("Ellipse", new Vector2(0.5f, 0.3f),
                     new Vector2(0.1f, 0.1f),
                     ResourceLoader.loadBitmap("ellipse.png"),
                     ResourceLoader.loadBitmap("ellipse_chosen.png"));
            ellipse.Visible = false;
            List<LinkedToggleButton> links = new List<LinkedToggleButton>();
            links.Add(pen);
            links.Add(rubber);
            links.Add(line);
            links.Add(ellipse);
            LinkedToggleButton.link(links);

            List<LinkedToggleButton> shapesKiddies = new List<LinkedToggleButton>();
            shapesKiddies.Add(line);
            shapesKiddies.Add(ellipse);
            shapes.setKiddies(shapesKiddies);

            list.Add(pen);
            list.Add(rubber);
            list.Add(shapes);
            list.Add(line);
            list.Add(ellipse);
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
