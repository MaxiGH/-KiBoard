using System.Numerics;

namespace KiBoard.math
{
    public class Matrix3x3
    {
        private Vector3[] cols;

        private Matrix3x3()
        {
            cols = new Vector3[3];
            for (int i = 0; i < 3; i++)
            {
                cols[i] = new Vector3();
            }
        }

        public static Matrix3x3 identity()
        {
            Matrix3x3 mat = new Matrix3x3();
            for (int i = 0; i < 3; i++)
            {
                mat.cols[i] = new Vector3(i == 0 ? 1 : 0, i == 1 ? 1 : 0, i == 2 ? 1 : 0);
            }
            return mat;
        }

        public Matrix3x3 scale(Vector2 vec)
        {
            cols[0].X *= vec.X;
            cols[1].Y *= vec.Y;
            return this;
        }

        public Matrix3x3 scale(Vector3 vec)
        {
            cols[0].X *= vec.X;
            cols[1].Y *= vec.Y;
            cols[2].Z *= vec.Z;
            return this;
        }

        public void setColoumn(int index, Vector3 col)
        {
            if ((index < 0) || (index >= 3))
                return;
            cols[index] = col;
        }

        public void setRow(int index, Vector3 row)
        {
            switch (index)
            {
                case 0:
                    cols[0].X = row.X;
                    cols[1].X = row.Y;
                    cols[2].X = row.Z;
                    break;
                case 1:
                    cols[0].Y = row.X;
                    cols[1].Y = row.Y;
                    cols[2].Y = row.Z;
                    break;
                case 2:
                    cols[0].Z = row.X;
                    cols[1].Z = row.Y;
                    cols[2].Z = row.Z;
                    break;
            }
        }

        public Vector3 getColoumn(int index)
        {
            if ((index < 0) || (index >= 3))
                throw new System.Exception("index = " + index);
            return new Vector3(cols[index].X, cols[index].Y, cols[index].Z);
        }

        public Vector3 getRow(int index)
        {
            switch (index)
            {
                case 0:
                    return new Vector3(cols[0].X, cols[1].X, cols[2].X);
                case 1:
                    return new Vector3(cols[0].Y, cols[1].Y, cols[2].Y);
                case 2:
                    return new Vector3(cols[0].Z, cols[1].Z, cols[2].Z);
                default:
                    throw new System.Exception("index = " + index);
            }
        }

        public static Matrix3x3 createMatrixByColoumns(Vector3 col0, Vector3 col1, Vector3 col2)
        {
            Matrix3x3 matrix = new Matrix3x3();
            matrix.setColoumns(col0, col1, col2);
            return matrix;
        }

        public static Matrix3x3 createMatrixByRows(Vector3 row0, Vector3 row1, Vector3 row2)
        {
            Matrix3x3 matrix = new Matrix3x3();
            matrix.setRows(row0, row1, row2);
            return matrix;
        }

        private void setColoumns(Vector3 col0, Vector3 col1, Vector3 col2)
        {
            cols[0] = col0;
            cols[1] = col1;
            cols[2] = col2;
        }

        private void setRows(Vector3 row0, Vector3 row1, Vector3 row2)
        {
            setRow(0, row0);
            setRow(1, row1);
            setRow(2, row2);
        }

        public float getDeterminant()
        {
            return (cols[0].X * cols[1].Y * cols[2].Z
                + cols[1].X * cols[2].Y * cols[0].Z
                + cols[2].X * cols[0].Y * cols[1].Z
                - cols[0].Z * cols[1].Y * cols[2].X
                - cols[1].Z * cols[2].Y * cols[0].X
                - cols[2].Z * cols[0].Y * cols[1].X
                );
        }

        public Matrix3x3 getTransposed()
        {
            Matrix3x3 matrix = Matrix3x3.createMatrixByRows(this.getColoumn(0), 
                                                            this.getColoumn(1), 
                                                            this.getColoumn(2));
            return matrix;
        }

        // inverts this Matrix
        // returns weather the matrix is invertable or not
        public bool getInverted(out Matrix3x3 result)
        {
            float determinant = getDeterminant();
            if (determinant == 0.0f)
            {
                result = new Matrix3x3();
                return false;
            }
            result = getAdjugate().getScaled(1.0f / determinant);
            return true;
        }

        public Matrix3x3 getScaled(float f)
        {
            return Matrix3x3.createMatrixByRows(
                    new Vector3(getRow(0).X * f, getRow(0).Y * f, getRow(0).Z * f),
                    new Vector3(getRow(1).X * f, getRow(1).Y * f, getRow(1).Z * f),
                    new Vector3(getRow(2).X * f, getRow(2).Y * f, getRow(2).Z * f));
        }

        public static Matrix3x3 multiply(Matrix3x3 mat0, Matrix3x3 mat1)
        {
            return Matrix3x3.createMatrixByRows(
                    new Vector3(mat0.getRow(0).X * mat1.getRow(0).X + mat0.getRow(0).Y * mat1.getRow(1).X + mat0.getRow(0).Z * mat1.getRow(2).X,
                                mat0.getRow(0).X * mat1.getRow(0).Y + mat0.getRow(0).Y * mat1.getRow(1).Y + mat0.getRow(0).Z * mat1.getRow(2).Y,
                                mat0.getRow(0).X * mat1.getRow(0).Z + mat0.getRow(0).Y * mat1.getRow(1).Z + mat0.getRow(0).Z * mat1.getRow(2).Z),

                    new Vector3(mat0.getRow(1).X * mat1.getRow(0).X + mat0.getRow(1).Y * mat1.getRow(1).X + mat0.getRow(1).Z * mat1.getRow(2).X,
                                mat0.getRow(1).X * mat1.getRow(0).Y + mat0.getRow(1).Y * mat1.getRow(1).Y + mat0.getRow(1).Z * mat1.getRow(2).Y,
                                mat0.getRow(1).X * mat1.getRow(0).Z + mat0.getRow(1).Y * mat1.getRow(1).Z + mat0.getRow(1).Z * mat1.getRow(2).Z),

                    new Vector3(mat0.getRow(2).X * mat1.getRow(0).X + mat0.getRow(2).Y * mat1.getRow(1).X + mat0.getRow(2).Z * mat1.getRow(2).X,
                                mat0.getRow(2).X * mat1.getRow(0).Y + mat0.getRow(2).Y * mat1.getRow(1).Y + mat0.getRow(2).Z * mat1.getRow(2).Y,
                                mat0.getRow(2).X * mat1.getRow(0).Z + mat0.getRow(2).Y * mat1.getRow(1).Z + mat0.getRow(2).Z * mat1.getRow(2).Z)
            );
        }

        public static Vector3 multiply(Matrix3x3 mat, Vector3 vec)
        {
            return new Vector3(
                    mat.getRow(0).X * vec.X + mat.getRow(0).Y * vec.Y + mat.getRow(0).Z * vec.Z,
                    mat.getRow(1).X * vec.X + mat.getRow(1).Y * vec.Y + mat.getRow(1).Z * vec.Z,
                    mat.getRow(2).X * vec.X + mat.getRow(2).Y * vec.Y + mat.getRow(2).Z * vec.Z
                );
        }

        public static Matrix3x3 multiplyElementwise(Matrix3x3 mat0, Matrix3x3 mat1)
        {
            return Matrix3x3.createMatrixByRows(
                new Vector3(mat0.getRow(0).X * mat1.getRow(0).X, 
                            mat0.getRow(0).Y * mat1.getRow(0).Y, 
                            mat0.getRow(0).Z * mat1.getRow(0).Z),

                new Vector3(mat0.getRow(1).X * mat1.getRow(1).X,
                            mat0.getRow(1).Y * mat1.getRow(1).Y,
                            mat0.getRow(1).Z * mat1.getRow(1).Z),

                new Vector3(mat0.getRow(2).X * mat1.getRow(2).X,
                            mat0.getRow(2).Y * mat1.getRow(2).Y,
                            mat0.getRow(2).Z * mat1.getRow(2).Z)
                );
        }

        public Matrix3x3 translate(Vector2 vec)
        {
            cols[2].X = vec.X;
            cols[2].Y = vec.Y;
            return this;
        }

        public Vector2 transform(Vector2 vec)
        {
            Vector3 result = multiply(this, new Vector3(vec.X, vec.Y, 1));
            return new Vector2(result.X, result.Y);
        }

        public Vector3 transform(Vector3 vec)
        {
            return multiply(this, vec);
        }

        public static Matrix3x3 getCheckerboard()
        {
            return Matrix3x3.createMatrixByRows(
                new Vector3(1.0f, -1.0f, 1.0f),
                new Vector3(-1.0f, 1.0f, -1.0f),
                new Vector3(1.0f, -1.0f, 1.0f)
                );
        }

        public Matrix3x3 getAdjugate()
        {
            Matrix3x3 matrix = getTransposed();

            matrix = createMatrixByRows(
                new Vector3(matrix.getSubMatrix(0, 0).getDeterminant(),
                            matrix.getSubMatrix(1, 0).getDeterminant(), 
                            matrix.getSubMatrix(2, 0).getDeterminant()),

                new Vector3(matrix.getSubMatrix(0, 1).getDeterminant(), 
                            matrix.getSubMatrix(1, 1).getDeterminant(), 
                            matrix.getSubMatrix(2, 1).getDeterminant()),

                new Vector3(matrix.getSubMatrix(0, 2).getDeterminant(), 
                            matrix.getSubMatrix(1, 2).getDeterminant(),
                            matrix.getSubMatrix(2, 2).getDeterminant()));

            matrix = multiplyElementwise(matrix, getCheckerboard());
            return matrix;
        }

        public Matrix2x2 getSubMatrix(int col, int row)
        {
            if ((col < 0) || (col >= 3))
            {
                throw new System.Exception("col=" + col);
            }
            if ((row < 0) || (row >= 3))
            {
                throw new System.Exception("row=" + row);
            }
            Vector3 colVec0;
            Vector3 colVec1;
            if (col == 0)
            {
                colVec0 = cols[1];
                colVec1 = cols[2];
            } else if (col == 1)
            {
                colVec0 = cols[0];
                colVec1 = cols[2];
            } else {  // col == 2           
                colVec0 = cols[0];
                colVec1 = cols[1];
            }
            Matrix2x2 matrix;
            if (row == 0)
            {
                matrix = Matrix2x2.createMatrix2x2ByColoumns(new Vector2(colVec0.Y, colVec0.Z), 
                                                             new Vector2(colVec1.Y, colVec1.Z));
            } else if (row == 1)
            {
                matrix = Matrix2x2.createMatrix2x2ByColoumns(new Vector2(colVec0.X, colVec0.Z), 
                                                             new Vector2(colVec1.X, colVec1.Z));
            } else // row == 2
            {
                matrix = Matrix2x2.createMatrix2x2ByColoumns(new Vector2(colVec0.X, colVec0.Y), 
                                                             new Vector2(colVec1.X, colVec1.Y));
            }
            return matrix;
        }

        public string toString()
        {
            return "/" + cols[0].X + " " + cols[1].X + " " + cols[2].X + "\\\n"
                 + "|" + cols[0].Y + " " + cols[1].Y + " " + cols[2].Y + "|\n"
                 + "\\" + cols[0].Z + " " + cols[1].Z + " " + cols[2].Z + "/";
        }
    }
}
