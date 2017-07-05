using System.Collections.Generic;
using System.Numerics;
using System.Threading;
using System.Windows.Forms;

namespace KiBoard.inputManager
{
    public class TestInputManager
    {
        private List<Vector3> inputs;
        private InputManager inputManager;

        public TestInputManager(Form form)
        {
            inputManager = new InputManager(form);
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
            Thread.Sleep(1000);
            foreach (Vector3 vec in inputs)
            {
                inputManager.processInput(vec);
            }
        }
    }
}