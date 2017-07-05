using System.Numerics;
using KiBoard.graphics;
using System.Windows.Forms;
using KiBoard.ui;

namespace KiBoard.inputManager
{

    class InputManager
    {
        private enum InputState
        {
            WRITE, // currentDrawable is set and input is forwarded to this drawable
            AWAIT_LINE, // currentDrawable is NOT set and if input touches wall again, a new line will be created
            CLICKING, // UIElement is clicked
            CLICK_UNHOVERED // UIElement was clicked, Finger is still touching wall, but not Element
        }

        private InputState state;
        private Graphics graphics;
        private UIManager uiManager;

        public const float TOUCH_THRESHOLD = 0.01f;

        private Drawable currentDrawable;

        private UIElement clickedElement;

        public InputManager(Form form)
        {
            state = InputState.AWAIT_LINE;
            System.Drawing.Graphics g = form.CreateGraphics();
            graphics = new Graphics(g, new System.Drawing.Size(form.Size.Width, form.Size.Height));
            uiManager = new UIManager(new DefaultConfiguration(),
                new System.Drawing.Size(form.Size.Width, form.Size.Height),
                g);
        }

        public bool inputTouchesWall(Vector3 input)
        {
            return (input.Z < TOUCH_THRESHOLD);
        }

        public void processInput(Vector3 input)
        {
            uiManager.showAllElements();
            if (inputTouchesWall(input))
            {
                processTouchingInput(new Vector2(input.X, input.Y));
            }
            else
            {
                processDetachedInput(input);
            }
            graphics.renderLast();
            uiManager.render();
        }

        private void processTouchingInput(Vector2 input)
        {
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
                        state = InputState.CLICKING;
                    } else
                    {
                        currentDrawable = new Line();
                        currentDrawable.nextPoint(input);
                        graphics.push(currentDrawable);
                        state = InputState.WRITE;
                    }
                    break;
                case InputState.CLICKING:
                    if (!clickedElement.touches(input))
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
                    state = InputState.AWAIT_LINE;
                    break;
                default:
                    break;
            }
        }

        public void updateFormSize(System.Drawing.Size s)
        {
            graphics.updateFormSize(s);
            uiManager.updateFormSize(s);
        }
    }
}
