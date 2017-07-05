using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KiBoard.tracker
{
    class HandCollection
    {
        private Hand left;
        private Hand right;

        public HandCollection(Hand left, Hand right)
        {
            this.left = left;
            this.right = right;
        }
    }
}
