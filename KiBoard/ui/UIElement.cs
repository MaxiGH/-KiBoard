using System.Drawing;
using System.Numerics;

namespace KiBoard.ui
{
    interface UIElement
    {
        void render(Graphics gfx, Size WindowSize);
        bool touches(Vector2 vec);
        void setVisible(bool visible);
        void onClick();
        void onClickReleased();
    }
}
