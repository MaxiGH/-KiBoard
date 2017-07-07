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
        
        public Hand(Vector3 vec3)
        {
            jointCoordinate = vec3;
            isDefined = true;
        }

        public Hand()
        {
            isDefined = false;
        }
    }
}
