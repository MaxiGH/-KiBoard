using System;
using System.Collections.Generic;

namespace KiBoard
{
    public class Graphics
    {
        Graphics()
        {
            items = new List<Drawable>();
        }

        private List<Drawable> items;

        public int Count
        {
            get { return items.Count; }
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
            foreach (Drawable i in items)
            {
                i.draw();
            }
        }
    }
}