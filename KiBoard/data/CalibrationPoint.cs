using System;

namespace KiBoard
{
    public class CalibrationPoint
    {
        public Vec3D kinectVec;
        public Vec3D wallVec;

        public CalibrationPoint(Vec3D kinectVec_arg, Vec3D wallVec_arg)
        {
            kinectVec = kinectVec_arg;
            wallVec = wallVec_arg;
        }
    }
}