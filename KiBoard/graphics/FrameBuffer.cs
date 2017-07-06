using System.Numerics;
using System.Drawing;

namespace KiBoard.graphics
{
    public class FrameBuffer
    {
        private Bitmap bitmap;
        //private Graphics gfx;

        public FrameBuffer(Vector2 size)
        {
            this.bitmap = new Bitmap((int)size.X, (int)size.Y);
            //this.gfx = Graphics.FromImage(bitmap);
        }

        public Bitmap Bitmap
        {
            get { return bitmap; }
            set {
                bitmap = value;
                //gfx = Graphics.FromImage(bitmap);
            }
        }

        public Vector2 Size
        {
            get {
                return new Vector2(bitmap.Width, bitmap.Height); }
            set {
                bitmap = new Bitmap((int)value.X, (int)value.Y);
                //gfx = Graphics.FromImage(bitmap);
            }
        }

        public Graphics graphics()
        {
            //return gfx;
            return Graphics.FromImage(bitmap);
        }
    }
}
