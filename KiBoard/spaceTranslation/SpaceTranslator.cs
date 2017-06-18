using System;
using System.Collections.Generic;
using System.Numerics;

namespace KiBoard
{
    public interface SpaceTranslator
    {
        // translates a KinectSpace Koordinate into a WallSpace Koordinate
        Vector3 translate(Vector3 kinectVec);
        // find out, how to translate KinectSpace into WallSpace
        void processCalibrationPoints(List<CalibrationPoint> points);
    }
}