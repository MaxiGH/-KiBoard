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
                     ResourceLoader.loadBitmap("shapes.png"),
                     ResourceLoader.loadBitmap("shapes_chosen.png"));

            LinkedToggleButton line = new LinkedToggleButton("Line", new Vector2(0.5f, 0.2f),
                     new Vector2(0.1f, 0.1f),
                     ResourceLoader.loadBitmap("line.png"),
                     ResourceLoader.loadBitmap("line_chosen.png"));
            LinkedToggleButton ellipse = new LinkedToggleButton("Ellipse", new Vector2(0.5f, 0.3f),
                     new Vector2(0.1f, 0.1f),
                     ResourceLoader.loadBitmap("ellipse.png"),
                     ResourceLoader.loadBitmap("ellipse_chosen.png"));
            LinkedToggleButton coords = new LinkedToggleButton("Coords", new Vector2(0.5f, 0.4f),
                     new Vector2(0.1f, 0.1f),
                     ResourceLoader.loadBitmap("coords.png"),
                     ResourceLoader.loadBitmap("coords_chosen.png"));
            List<LinkedToggleButton> toolLinks = new List<LinkedToggleButton>();
            toolLinks.Add(pen);
            toolLinks.Add(rubber);
            toolLinks.Add(line);
            toolLinks.Add(ellipse);
            toolLinks.Add(coords);
            LinkedToggleButton.link(toolLinks);

            List<LinkedToggleButton> shapesKiddies = new List<LinkedToggleButton>();
            shapesKiddies.Add(line);
            shapesKiddies.Add(ellipse);
            shapesKiddies.Add(coords);
            shapes.setKiddies(shapesKiddies);

            ExpandToggleButton colors = new ExpandToggleButton("Colors", new Vector2(0.1f, 0.8f),
                     new Vector2(0.1f, 0.1f),
                     ResourceLoader.loadBitmap("shapes.png"),
                     ResourceLoader.loadBitmap("shapes_chosen.png"));
            LinkedToggleButton white = new LinkedToggleButton("White", new Vector2(0.2f, 0.8f),
                     new Vector2(0.1f, 0.1f),
                     ResourceLoader.loadBitmap("ellipse.png"),
                     ResourceLoader.loadBitmap("ellipse_chosen.png"), true);
            LinkedToggleButton yellow = new LinkedToggleButton("Yellow", new Vector2(0.3f, 0.8f),
                     new Vector2(0.1f, 0.1f),
                     ResourceLoader.loadBitmap("ellipse.png"),
                     ResourceLoader.loadBitmap("ellipse_chosen.png"));
            LinkedToggleButton red = new LinkedToggleButton("Red", new Vector2(0.4f, 0.8f),
                     new Vector2(0.1f, 0.1f),
                     ResourceLoader.loadBitmap("ellipse.png"),
                     ResourceLoader.loadBitmap("ellipse_chosen.png"));
            LinkedToggleButton green = new LinkedToggleButton("Green", new Vector2(0.5f, 0.8f),
                     new Vector2(0.1f, 0.1f),
                     ResourceLoader.loadBitmap("ellipse.png"),
                     ResourceLoader.loadBitmap("ellipse_chosen.png"));
            LinkedToggleButton blue = new LinkedToggleButton("Blue", new Vector2(0.6f, 0.8f),
                     new Vector2(0.1f, 0.1f),
                     ResourceLoader.loadBitmap("ellipse.png"),
                     ResourceLoader.loadBitmap("ellipse_chosen.png"));

            List<LinkedToggleButton> colorLinks = new List<LinkedToggleButton>();
            colorLinks.Add(white);
            colorLinks.Add(yellow);
            colorLinks.Add(red);
            colorLinks.Add(green);
            colorLinks.Add(blue);
            LinkedToggleButton.link(colorLinks);

            List<LinkedToggleButton> colorsKiddies = new List<LinkedToggleButton>();
            colorsKiddies = colorLinks;
            colors.setKiddies(colorsKiddies);

            ExpandToggleButton widths = new ExpandToggleButton("Widths", new Vector2(0.1f, 0.7f),
                     new Vector2(0.1f, 0.1f),
                     ResourceLoader.loadBitmap("shapes.png"),
                     ResourceLoader.loadBitmap("shapes_chosen.png"));
            LinkedToggleButton slim = new LinkedToggleButton("Slim", new Vector2(0.2f, 0.7f),
                     new Vector2(0.1f, 0.1f),
                     ResourceLoader.loadBitmap("ellipse.png"),
                     ResourceLoader.loadBitmap("ellipse_chosen.png"));
            LinkedToggleButton normal = new LinkedToggleButton("Normal", new Vector2(0.3f, 0.7f),
                     new Vector2(0.1f, 0.1f),
                     ResourceLoader.loadBitmap("ellipse.png"),
                     ResourceLoader.loadBitmap("ellipse_chosen.png"), true);
            LinkedToggleButton big = new LinkedToggleButton("Big", new Vector2(0.4f, 0.7f),
                     new Vector2(0.1f, 0.1f),
                     ResourceLoader.loadBitmap("ellipse.png"),
                     ResourceLoader.loadBitmap("ellipse_chosen.png"));

            List<LinkedToggleButton> widthLinks = new List<LinkedToggleButton>();
            widthLinks.Add(slim);
            widthLinks.Add(normal);
            widthLinks.Add(big);
            LinkedToggleButton.link(widthLinks);

            List<LinkedToggleButton> widthsKiddies = new List<LinkedToggleButton>();
            widthsKiddies = widthLinks;
            widths.setKiddies(widthsKiddies);

            list.Add(pen);
            list.Add(rubber);

            list.Add(shapes);
            list.Add(line);
            list.Add(ellipse);
            list.Add(coords);

            list.Add(colors);
            list.Add(white);
            list.Add(yellow);
            list.Add(red);
            list.Add(green);
            list.Add(blue);

            list.Add(widths);
            list.Add(slim);
            list.Add(normal);
            list.Add(big);

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
