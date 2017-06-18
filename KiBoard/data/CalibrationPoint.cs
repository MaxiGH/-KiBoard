using System;
using System.Windows.Media.Media3D;

namespace KiBoard
{
    public class CalibrationPoint
    {
        public Vector3D kinectVec;
        public Vector3D wallVec;

        public CalibrationPoint(Vector3D kinectVec_arg, Vector3D wallVec_arg)
        {
            kinectVec = kinectVec_arg;
            wallVec = wallVec_arg;
        }
    }
}