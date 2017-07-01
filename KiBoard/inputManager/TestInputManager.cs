using System.Collections.Generic;
using System.Numerics;

namespace KiBoard.inputManager
{
    public class TestInputManager
    {
        private List<Vector3> inputs;
        private InputManager inputManager;

        public TestInputManager(System.Drawing.Graphics g)
        {
            inputManager = new InputManager(g);
            inputs = new List<Vector3>();
            initInputs();
        }

        private void initInputs()
        {
            inputs.Add(new Vector3(0.2f, 0.1f, 0.2f));
            inputs.Add(new Vector3(0.2f, 0.1f, 0.0f));
            inputs.Add(new Vector3(0.2f, 0.2f, 0.0f));
            inputs.Add(new Vector3(0.3f, 0.2f, 0.0f));
            inputs.Add(new Vector3(0.3f, 0.2f, 0.2f));
            inputs.Add(new Vector3(0.5f, 0.2f, 0.2f));
            inputs.Add(new Vector3(0.5f, 0.2f, 0.0f));
            inputs.Add(new Vector3(0.5f, 0.3f, 0.0f));
            inputs.Add(new Vector3(0.6f, 0.3f, 0.0f));
        }

        public void test()
        {
            foreach (Vector3 vec in inputs)
            {
                inputManager.processInput(vec);
            }
        }
    }
}