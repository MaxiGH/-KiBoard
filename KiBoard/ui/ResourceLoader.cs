using System.Drawing;

namespace KiBoard.ui
{
    class ResourceLoader
    {
        static string resPath = @"./resources/";

        public static Image loadBitmap(string name)
        {
            return Bitmap.FromFile(resPath + name);
        }
    }
}
