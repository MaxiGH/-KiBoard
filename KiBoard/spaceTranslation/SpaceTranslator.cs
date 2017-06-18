using System;
using System.Collections.Generic;
using System.Windows.Media.Media3D;

namespace KiBoard
{
    public interface SpaceTranslator
    {
        // translates a KinectSpace Koordinate into a WallSpace Koordinate
        Vector3D translate(Vector3D kinectVec);
        // find out, how to translate KinectSpace into WallSpace
        void processCalibrationPoints(List<CalibrationPoint> points);
    }
}