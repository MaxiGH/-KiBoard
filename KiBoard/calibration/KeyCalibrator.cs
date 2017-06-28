using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KiBoard;
using System.Numerics;

namespace KiBoard.calibration
{
    class KeyCalibrator : Calibrator
    {

        private List<CalibrationPoint> list;
        private CalibrationPoint calPoint1;
        private CalibrationPoint calPoint2;
        private CalibrationPoint calPoint3;
        private Tracker tracker;
        private int state;

        public KeyCalibrator(Tracker tracker) 
        {
            this.list = new List<CalibrationPoint>();
            this.tracker = tracker;
            this.state = 0;
        }

        public List<CalibrationPoint> getCalibrationPoints()
        {
            return list;
        }

        public bool keyPressed()
        {
            throw new NotImplementedException();
        }

        public void calibratePointOne()
        {
            Vector3 kinectVec = tracker.Coordinates;

            calPoint1 = new CalibrationPoint(new Vector3(kinectVec.X, kinectVec.Y, kinectVec.Z), new Vector3(0, 0, 0));
            list.Add(calPoint1);
        }

        public void calibratePointTwo()
        {
            Vector3 kinectVec = tracker.Coordinates;

            calPoint2 = new CalibrationPoint(new Vector3(kinectVec.X, kinectVec.Y, kinectVec.Z), new Vector3(0, 1, 0));
            list.Add(calPoint2);
        }

        public void calibratePointThree()
        {
            Vector3 kinectVec = tracker.Coordinates;

            calPoint3 = new CalibrationPoint(new Vector3(kinectVec.X, kinectVec.Y, kinectVec.Z), new Vector3(1, 0, 0));
            list.Add(calPoint3);
        }

        public void processCalibration()
        {
            if(state == 0 && keyPressed())
            {
                calibratePointOne();
                state = 1; 
            }
            else if (state == 1 && keyPressed())
            {
                calibratePointTwo();
                state = 2;
            }
            else if (state == 2 && keyPressed())
            {
                calibratePointThree();
                state = 3;
            }
        }

        public bool hasCalibrationPoints()
        {
            return (list.Count == 3);
        }

        public void tick()
        {
            while (state < 3)
            {
                processCalibration();
            }
        }
    }
}
