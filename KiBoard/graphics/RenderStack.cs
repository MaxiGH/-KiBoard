using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Numerics;
using KiBoard.math;

namespace KiBoard.graphics
{
    public class RenderStack : IEnumerable<Drawable>
    {
        private List<Drawable> items;
        private List<Drawable> free;

        public RenderStack()
        {
            items = new List<Drawable>();
            free = new List<Drawable>();
        }

        public int Count
        {
            get { return items.Count; }
        }

        public void clear()
        {
            items.Clear();
        }

        public bool isEmpty()
        {
            return Count == 0;
        }

        public bool canRepush()
        {
            return free.Count > 0;
        }

        public List<Drawable> getItems()
        {
            return items;
        }

        public Drawable this[int index]
        {
            get { return items[index]; }
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
            free.Clear();
        }

        public void repush()
        {
            if (free.Count > 0)
            {
                items.Add(free[free.Count - 1]);
                free.RemoveAt(free.Count - 1);
            }
        }

        public void pop()
        {
            if (items.Count > 0)
            {
                free.Add(items[items.Count - 1]);
                items.RemoveAt(items.Count - 1);
            }
        }

        /*public void remove(int index)
        {
            if (0 <= index && index < items.Count)
            {
                free.Add(items[index]);
                items.RemoveAt(index);
            }
        }*/

        public IEnumerator<Drawable> GetEnumerator()
        {
            return items.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return items.GetEnumerator();
        }
    }
}
