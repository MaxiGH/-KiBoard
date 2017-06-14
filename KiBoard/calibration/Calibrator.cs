using System;
using System.Collections.Generic;

namespace KiBoard
{
    interface Calibrator
    {
        // will be called every frame
        void tick();
        // returns whether the calibration Process is finished
        bool hasCalibrationPoints();
        // returns the determined calibration points
        // returns null, if calibration points are not determined yet
        List<CalibrationPoint> getCalibrationPoints();
    }
}