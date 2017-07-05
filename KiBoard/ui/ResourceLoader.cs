using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KiBoard.ui
{
    class ResourceLoader
    {
        static string resPath = System.IO.Path.GetDirectoryName(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName) + @"\..\..\resources\";

        public static Image loadBitmap(string name)
        {
            return Bitmap.FromFile(resPath + name);
        }
    }
}
