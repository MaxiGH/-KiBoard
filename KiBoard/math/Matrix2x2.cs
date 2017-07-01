using System.Numerics;

namespace KiBoard.math
{
    public class Matrix2x2
    {
        private Vector2[] cols;

        private Matrix2x2()
        {
            cols = new Vector2[2];
            for (int i = 0; i < 2; i++)
            {
                cols[i] = new Vector2();
            }
        }

        public void setColoumns(Vector2 col0, Vector2 col1)
        {
            cols[0] = new Vector2(col0.X, col0.Y);
            cols[1] = new Vector2(col1.X, col1.Y);
        }

        public void setRows(Vector2 row0, Vector2 row1)
        {
            cols[0] = new Vector2(row0.X, row1.X);
            cols[1] = new Vector2(row0.Y, row1.Y);
        }

        public static Matrix2x2 createMatrix2x2ByColoumns(Vector2 col0, Vector2 col1)
        {
            Matrix2x2 matrix = new Matrix2x2();
            matrix.setColoumns(col0, col1);
            return matrix;
        }

        public static Matrix2x2 createMatrix2x2ByRows(Vector2 row0, Vector2 row1)
        {
            Matrix2x2 matrix = new Matrix2x2();
            matrix.setRows(row0, row1);
            return matrix;
        }

        public float getDeterminant()
        {
            return (cols[0].X * cols[1].Y) - (cols[0].Y * cols[1].X);
        }

        public static Vector2 operator*(Matrix2x2 m, Vector2 vec)
        {
            return new Vector2(m.cols[0].X * vec.X + m.cols[1].Y * vec.X, m.cols[0].Y * vec.Y + m.cols[1].Y * vec.Y);
        }
    }
}
