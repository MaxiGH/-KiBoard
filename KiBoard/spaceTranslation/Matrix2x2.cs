using System.Numerics;

namespace KiBoard.spaceTranslation
{
    public class Matrix2x2
    {
        private Vector2[] data;

        private Matrix2x2()
        {
            data = new Vector2[2];
            for (int i = 0; i < 2; i++)
            {
                data[i] = new Vector2();
            }
        }

        public void setColoumns(Vector2 col0, Vector2 col1)
        {
            data[0] = new Vector2(col0.X, col0.Y);
            data[1] = new Vector2(col1.X, col1.Y);
        }

        public void setRows(Vector2 row0, Vector2 row1)
        {
            data[0] = new Vector2(row0.X, row1.X);
            data[1] = new Vector2(row0.Y, row1.Y);
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
            return (data[0].X * data[1].Y) - (data[0].Y * data[1].X);
        }
    }
}
