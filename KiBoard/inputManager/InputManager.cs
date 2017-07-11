using System.Numerics;
using KiBoard.graphics;
using System.Windows.Forms;
using KiBoard.ui;
using System.Drawing;
using System;

namespace KiBoard.inputManager
{

    public class InputManager
    {
        private enum InputState
        {
            WRITE, // currentDrawable is set and input is forwarded to this drawable
            AWAIT_PEN, // currentDrawable is NOT set and if input touches wall again, a new line will be created
            CLICKING, // UIElement is clicked
            CLICK_UNHOVERED // UIElement was clicked, Finger is still touching wall, but not Element
        }

        private InputState state;
        private Renderer renderer;
        private UIManager uiManager;
        private FrameBuffer frame;
        private Graphics gfx;

        public const float TOUCH_THRESHOLD = 0.01f;

        private Drawable currentDrawable;

        private UIElement clickedElement;
        private UIController controller;

        private enum PenState
        {
            PEN_RUBBER,
            PEN_LINE,
            PEN_SEGMENT
        }

        private Color penColor;
        private float penWidth;
        private PenState penState;

        public InputManager(Form form)
        {
            state = InputState.AWAIT_PEN;
            penState = PenState.PEN_LINE;
            penWidth = 1.0f;
            penColor = Color.White;

            Vector2 size = new Vector2(form.Size.Width, form.Size.Height);
            frame = new FrameBuffer(size);
            renderer = new Renderer(frame);
            UIConfiguration configuration = new DefaultConfiguration();
            uiManager = new UIManager(configuration, frame);
            controller = configuration.createController(this);
            gfx = form.CreateGraphics();
        }

        public bool inputTouchesWall(Vector3 input)
        {
            return (input.Z < TOUCH_THRESHOLD);
        }

        public void processInput(Vector3 input)
        {
            graphics.MessageBox.print("state = " + state.ToString());
            uiManager.showAllElements();
            if (inputTouchesWall(input))
            {
                processTouchingInput(new Vector2(input.X, input.Y));
            }
            else
            {
                processDetachedInput(input);
            }

            render();
        }

        private void render()
        {
            renderer.clear();
            renderer.render();
            uiManager.render();
            graphics.MessageBox.draw(frame);
            gfx.DrawImage(frame.Bitmap, 0, 0);
        }

        private void processTouchingInput(Vector2 input)
        {
            switch (state)
            {
                case InputState.WRITE:
                    currentDrawable.nextPoint(input);
                    uiManager.hideHoveredElements(input);
                    break;
                case InputState.AWAIT_PEN:
                    if (uiManager.isTouchingElement(input))
                    {
                        clickedElement = uiManager.getTouchingElement(input);
                        graphics.MessageBox.print("element " + clickedElement.Name + " clicked", 10);
                        clickedElement.onClick();
                        controller.onClick(clickedElement.Name);
                        state = InputState.CLICKING;
                    }
                    else
                    {
                        switch (penState)
                        {
                            case PenState.PEN_RUBBER:
                                Line rubber = new Line();
                                rubber.color = Color.Black;
                                rubber.width = penWidth;
                                currentDrawable = rubber;
                                currentDrawable.nextPoint(input);
                                renderer.Stack.push(currentDrawable);
                                state = InputState.WRITE;
                                break;
                            case PenState.PEN_LINE:
                                Line line = new Line();
                                line.color = penColor;
                                line.width = penWidth;
                                currentDrawable = line;
                                currentDrawable.nextPoint(input);
                                renderer.Stack.push(currentDrawable);
                                state = InputState.WRITE;
                                break;
                            case PenState.PEN_SEGMENT:
                                Segment segment = new Segment();
                                segment.color = penColor;
                                segment.width = penWidth;
                                currentDrawable = segment;
                                state = InputState.WRITE;
                                break;
                        }

                        currentDrawable.nextPoint(input);
                        renderer.Stack.push(currentDrawable);
                    }
                    break;
                case InputState.CLICKING:
                    if (!clickedElement.touches(input)) // Wenn wir es nicht mehr berühren
                    {
                        clickedElement.onClickReleased();
                        state = InputState.CLICK_UNHOVERED;
                    }
                    break;
            }
        }

        private void processDetachedInput(Vector3 input)
        {
            switch (state)
            {
                case InputState.CLICK_UNHOVERED:
                case InputState.WRITE:
                    state = InputState.AWAIT_PEN;
                    break;
                case InputState.CLICKING:
                    clickedElement.onClickReleased();
                    state = InputState.AWAIT_PEN;
                    break;
                default:
                    break;
            }

            renderer.renderEllipse(new Vector2(input.X, input.Y), System.Drawing.Color.Gray);
        }

        public void updateFormSize(System.Drawing.Size s)
        {
            frame.Size = new Vector2(s.Width, s.Height);
        }

        public void createTestMessagebox()
        {
            graphics.MessageBox.print("Der Button wurde geklickt", 10);
        }

        public void activatePen()
        {
            penState = PenState.PEN_LINE;
        }

        public void activateRubber()
        {
            penState = PenState.PEN_RUBBER;
        }

        public void undo()
        {
            renderer.Stack.pop();
        }

        public void redo()
        {
            renderer.Stack.repush();
        }

        public void save()
        {
            var dateTime = DateTime.Now;
            var format = "yyyy-mm-dd_HH-mm-ss";
            var filePath = String.Format("./KiBoard_{0}.bmp", dateTime.ToString(format));
            renderer.renderToFile(filePath);
        }
    }
}
