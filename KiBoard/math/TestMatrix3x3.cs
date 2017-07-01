using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using static KiBoard.math.Matrix3x3;

namespace KiBoard.math
{
    public class TestMatrix3x3
    {
        public static void start()
        {
            /*
            Matrix3x3 matrix = Matrix3x3.createMatrixByRows(new Vector3(3, 0, 2), 
                                                            new Vector3(2, 0, -2), 
                                                            new Vector3(0, 2, 1));
            System.Console.WriteLine("testMatrix:");
            System.Console.WriteLine(matrix.toString());*/

            //testTranspose(matrix);
            //testDeterminante(matrix);
            //testDoubleTranspose(matrix);
            //testInvert(matrix);
            //testMultiply(matrix);
            testTranslator();
        }

        public static void testTranspose(Matrix3x3 matrix)
        {
            Matrix3x3 transposedMatrix = matrix.getTransposed();
            System.Console.WriteLine("TransposedMatrix:");
            System.Console.WriteLine(transposedMatrix.toString());
        }

        public static void testDeterminante(Matrix3x3 matrix)
        {
            float deter = matrix.getDeterminant();
            System.Console.WriteLine("Determinante: " + deter);
        }

        public static void testDoubleTranspose(Matrix3x3 matrix)
        {
            matrix = matrix.getTransposed();
            matrix = matrix.getTransposed();
            System.Console.WriteLine("double transposed matrix:\n" + matrix.toString());
        }

        public static void testInvert(Matrix3x3 matrix)
        {
            Matrix3x3 mat;
            if (!matrix.getInverted(out mat))
            {
                System.Console.WriteLine("cant invert");
                return;
            }

            System.Console.WriteLine("inverted:");
            System.Console.WriteLine(mat.toString());
        }

        public static void testMultiply(Matrix3x3 matrix)
        {
            Matrix3x3 mat;
            if (!matrix.getInverted(out mat))
            {
                System.Console.WriteLine("cant invert");
                return;
            }

            System.Console.WriteLine("multiplied:");
            System.Console.WriteLine(Matrix3x3.multiply(matrix, mat).toString());
        }

        public static void testTranslator()
        {
            List<CalibrationPoint> points = new List<CalibrationPoint>();
            points.Add(new CalibrationPoint(
                    new Vector3(0, 0, 5),
                    new Vector3(0, 0, 0)
                ));
            points.Add(new CalibrationPoint(
                    new Vector3(0, 1, 5),
                    new Vector3(0, 1, 0)
                ));
            points.Add(new CalibrationPoint(
                    new Vector3(0, 0, 4),
                    new Vector3(1, 0, 0)
                ));
            SpaceTranslator translator = new SpaceTranslator();
            translator.processCalibrationPoints(points);
            for (int i = 0; i < points.Count; i++)
            {
                System.Console.WriteLine(points[i].wallVec.ToString() + " translate" + 
                    points[i].kinectVec.ToString() + " = " + translator.translate(points[i].kinectVec));
            }
        }
    }
}
