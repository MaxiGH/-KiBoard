using System.Collections.Generic;
using System.Drawing;

namespace KiBoard.graphics
{
    class MessageBox
    {
        public class Message
        {
            public Message(string t, int c)
            {
                text = t;
                counter = c;
            }
            public string text;
            public int counter;
        }

        private static List<Message> messages = new List<Message>();
        private const int FONT_SIZE = 12;
        private static Font font = new Font("Monospace", FONT_SIZE);
        private static Brush brush = new SolidBrush(Color.White);

        private MessageBox()
        { }

        public static void print(string s, int frames = 1)
        {
            messages.Add(new Message(s, frames));
        }

        public static void tick()
        {
            for (int i = 0; i < messages.Count; i++)
            {
                messages[i].counter--;
                if (messages[i].counter <= 0)
                {
                    messages.RemoveAt(i);
                    i--;
                }
            }
        }

        public static void draw(FrameBuffer frame)
        {
            int y = 5;
            for (int i = 0; i < messages.Count; i++)
            {
                frame.graphics().DrawString(messages[i].text,
                                            font,
                                            brush,
                                            5, y);
                y += FONT_SIZE+2;
            }
        }
    }
}
