using System.Numerics;
using KiBoard.graphics;

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

        public const float TOUCH_THRESHOLD = 0.01f;

        private Drawable currentDrawable;

        public InputManager(System.Drawing.Graphics gfx)
        {
            state = InputState.AWAIT_LINE;
            graphics = new Graphics(gfx);
        }

        public bool inputTouchesWall(Vector3 input)
        {
            return (input.Z < TOUCH_THRESHOLD);
        }

        public void processInput(Vector3 input)
        {
            if (inputTouchesWall(input))
            {
                processTouchingInput(new Vector2(input.X, input.Y));
            }
            else
            {
                processDetachedInput(input);
            }
            graphics.render();
        }

        private void processTouchingInput(Vector2 input)
        {
            switch (state)
            {
                case InputState.WRITE:
                    currentDrawable.nextPoint(input);
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
    }
}
