using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;

namespace KiBoard.tracker
{
    interface Tracker
    {
        Vector3D Coordinates { get; }
    }
}
