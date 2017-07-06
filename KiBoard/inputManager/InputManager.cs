using System.Numerics;
using KiBoard.graphics;
using System.Windows.Forms;
using KiBoard.ui;
using System.Drawing;

namespace KiBoard.inputManager
{

    public class InputManager
    {
        private enum InputState
        {
            WRITE, // currentDrawable is set and input is forwarded to this drawable
            AWAIT_LINE, // currentDrawable is NOT set and if input touches wall again, a new line will be created
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

        public InputManager(Form form)
        {
            state = InputState.AWAIT_LINE;

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
            //System.Console.WriteLine("state=" + state.ToString());
            uiManager.showAllElements();
            if (inputTouchesWall(input))
            {
                processTouchingInput(new Vector2(input.X, input.Y));
            }
            else
            {
                processDetachedInput(input);
            }

            renderer.clear();
            renderer.render();
            uiManager.render();

            gfx.DrawImage(frame.Bitmap, 0, 0);
            System.Console.WriteLine("xyz");
        }

        private void processTouchingInput(Vector2 input)
        {
            gfx.DrawEllipse(new System.Drawing.Pen(new System.Drawing.SolidBrush(System.Drawing.Color.White)),
                new System.Drawing.Rectangle(10, 10, 20, 20));
            switch (state)
            {
                case InputState.WRITE:
                    currentDrawable.nextPoint(input);
                    uiManager.hideHoveredElements(input);
                    break;
                case InputState.AWAIT_LINE:

                    if (uiManager.isTouchingElement(input))
                    {
                        clickedElement = uiManager.getTouchingElement(input);
                        clickedElement.onClick();
                        controller.onClick(clickedElement.Name);
                        state = InputState.CLICKING;
                    } else
                    {
                        currentDrawable = new Line();
                        currentDrawable.nextPoint(input);
                        renderer.Stack.push(currentDrawable);
                        state = InputState.WRITE;
                    }
                    break;
                case InputState.CLICKING:
                    if (!clickedElement.touches(input))
                    {
                        clickedElement.onClickReleased();
                        state = InputState.CLICK_UNHOVERED;
                    }

                    currentDrawable = new Line();
                    currentDrawable.nextPoint(input);
                    renderer.Stack.push(currentDrawable);
                    state = InputState.WRITE;

                    break;
            }
        }

        private void processDetachedInput(Vector3 input)
        {
            switch (state)
            {
                case InputState.CLICK_UNHOVERED:
                case InputState.WRITE:
                    state = InputState.AWAIT_LINE;
                    break;
                default:
                    break;
            }
        }

        public void updateFormSize(System.Drawing.Size s)
        {
            frame.Size = new Vector2(s.Width, s.Height);
        }

        public void createTestDrawable()
        {
            gfx.DrawEllipse(new System.Drawing.Pen(new System.Drawing.SolidBrush(System.Drawing.Color.Red)),
                new System.Drawing.Rectangle(20, 20, 20, 20));
        }
    }
}
