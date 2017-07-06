using System.Collections.Generic;

namespace KiBoard.ui
{
    interface UIConfiguration
    {
        List<UIElement> createConfiguration();
        UIController createController(inputManager.InputManager manager);
    }
}
