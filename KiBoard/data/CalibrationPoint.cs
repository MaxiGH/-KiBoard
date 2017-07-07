using System;
using System.Numerics;

namespace KiBoard
{
    public class CalibrationPoint
    {
        public Vector3 kinectVec;
        public Vector3 wallVec;

        public CalibrationPoint(Vector3 kinectVec_arg, Vector3 wallVec_arg)
        {
            kinectVec = kinectVec_arg;
            wallVec = wallVec_arg;
        }

        public override string ToString()
        {
            return "kinectVec=" + kinectVec.ToString();
        }
    }
}