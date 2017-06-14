using System;
using System.Collections.Generic;

namespace KiBoard
{
    public interface SpaceTranslator
    {
        // translates a KinectSpace Koordinate into a WallSpace Koordinate
        Vec3D translate(Vec3D kinectVec);
        // find out, how to translate KinectSpace into WallSpace
        void processCalibrationPoints(List<CalibrationPoint> points);
    }
}