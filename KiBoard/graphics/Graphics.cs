using System;
using System.Collections.Generic;
using System.Drawing;
using System.Numerics;
using KiBoard.math;

namespace KiBoard.graphics
{
    public class Graphics
    {
        private List<Drawable> items;
        private Vector2 size;
        private System.Drawing.Bitmap pixels;
        private System.Drawing.Graphics gfx;
        private Matrix3x3 transform;

        public Graphics(System.Drawing.Graphics gfx)
        {
            this.gfx = gfx;

            items = new List<Drawable>();
            size = new Vector2(100, 100);
            transform = Matrix3x3.identity();
            pixels = new System.Drawing.Bitmap((int)size.X, (int)size.Y);

            Size = size;
        }

        public int Count
        {
            get { return items.Count; }
        }

        public Matrix3x3 Transform
        {
            get { return transform; }
            set { transform = value; }
        }

        public Vector2 Size
        {
            get { return size; }
            set {
                size = value;
                //pixels.SetResolution((int)size.X, (int)size.Y);
                pixels = new System.Drawing.Bitmap((int)Size.X, (int)Size.Y);

                System.Drawing.Drawing2D.Matrix transform = new System.Drawing.Drawing2D.Matrix();
                gfx.Transform = transform;
            }
        }

        public void clear()
        {
            items.Clear();
        }

        public Drawable back()
        {
            if (items.Count == 0)
                return null;
            else
                return items[items.Count - 1];
        }

        public void push(Drawable item)
        {
            items.Add(item);
        }

        public void pop()
        {
            if(items.Count > 0)
                items.RemoveAt(items.Count - 1);
        }

        public void remove(int index)
        {
            if (index < items.Count)
                items.RemoveAt(index);
        }

        public void render()
        {
            System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(pixels);
            g.Clear(Color.White);

            Matrix3x3 mat = Matrix3x3.multiply(Matrix3x3.identity().scale(new Vector2(size.X, size.Y)), transform);
            foreach (Drawable i in items)
            {
                i.draw(g, mat);
            }

            g.Dispose();
            gfx.DrawImage(pixels, 0, 0);
        }
    }
}