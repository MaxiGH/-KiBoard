using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace KiBoard
{
    class Hand
    {
        public Vector3 jointCoordinate;
        public bool touchWall=false;
        public bool isDefined;
        
        public Hand(Vector3 vec3, bool defined)
        {
            jointCoordinate = vec3;
            isDefined = defined;
        }

        public Hand()
        {
            isDefined = false;
        }
    }
}
