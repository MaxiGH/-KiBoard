using KiBoard.math;
using System;
using System.Collections.Generic;
using System.Numerics;

namespace KiBoard
{
    public class SpaceTranslator
    {
        Matrix3x3 translationMatrix;
        Vector3 k0;
        bool initialised;

        public SpaceTranslator()
        {
            initialised = false;
        }

        // translates a KinectSpace Koordinate into a WallSpace Koordinate
        public Vector3 translate(Vector3 kinectVec)
        {
            if (!canTranslate())
            {
                return new Vector3();
            }
            return Matrix3x3.multiply(translationMatrix, kinectVec - k0);
        }

        public bool canTranslate()
        {
            return initialised;
        }

        // find out, how to translate KinectSpace into WallSpace
        public void processCalibrationPoints(List<CalibrationPoint> points)
        {
            if (points.Count < 3)
            {
                throw new System.Exception("points.Count=" + points.Count);
            }
            for (int i = 0; i < 3; i++)
            {
                Console.WriteLine("point" + i + ": " + points[i]);
            }

            k0 = points[0].kinectVec;
            Vector3 k1s = points[1].kinectVec - k0;
            Vector3 k2s = points[2].kinectVec - k0;
            Vector3 kss = Vector3.Normalize(Vector3.Cross(k1s, k2s));
            Matrix3x3 K = Matrix3x3.createMatrixByColoumns(k1s, k2s, kss);
            Vector3 wss = Vector3.Normalize(Vector3.Cross((points[1].wallVec - points[0].wallVec), (points[2].wallVec - points[0].wallVec)));
            Matrix3x3 W = Matrix3x3.createMatrixByColoumns(points[1].wallVec, points[2].wallVec, wss);
            Matrix3x3 Kinverted;
            if (!K.getInverted(out Kinverted))
            {
                throw new Exception("cant invert");
            }
            translationMatrix = Matrix3x3.multiply(W, Kinverted);
            initialised = true;
            //System.Console.WriteLine("translationMatrix:");
            //System.Console.WriteLine(translationMatrix.toString());
            //System.Console.WriteLine("k0=" + k0.ToString());
        }
    }
}