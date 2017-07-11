using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace KiBoard.tracker
{
    interface Tracker
    {
        HandCollection getHandCollection(bool middled);
    }
}
