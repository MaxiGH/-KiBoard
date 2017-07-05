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
            AWAIT_LINE // currentDrawable is NOT set and if input touches wall again, a new line will be created
        }

        private InputState state;
        private Graphics graphics;
        private UIManager uiManager;

        public const float TOUCH_THRESHOLD = 0.01f;

        private Drawable currentDrawable;

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
                    currentDrawable = new Line();
                    currentDrawable.nextPoint(input);
                    graphics.push(currentDrawable);
                    state = InputState.WRITE;
                    break;
            }
        }

        private void processDetachedInput(Vector3 input)
        {
            switch (state)
            {
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
