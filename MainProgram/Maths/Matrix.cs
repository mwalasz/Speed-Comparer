using System;
using System.Linq;

namespace MainProgram.Maths
{
    public class Matrix
    {
        public float[,] Content { get; set; }
        public int Columns { get; set; }
        public int Rows { get; set; }

        public Matrix(int rows, int columns)
        {
            Rows = rows;
            Columns = columns;

            Content = new float[Rows, Columns];
        }

        public Matrix(float[,] matrix)
        {
            Rows = matrix.GetLength(0);
            Columns = matrix.GetLength(1);

            Content = matrix;
        }

        public Matrix MultiplyByScalar(Scalar scalar)
        {
            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Columns; j++)
                {
                    Content[i, j] = Content[i, j] * scalar.Value;
                }
            }

            return new Matrix(Content);
        }

        public override string ToString()
        {
            var numbers = Content.Cast<float>()
                .Select(i => i.ToString())
                .ToArray();

            string toDisplay = string.Empty;
            int start = 0;
            
            for (int i = 0; i < Rows; i++)
            {
                var line = string.Join(' ', numbers, start, Columns);
                toDisplay += (line + Environment.NewLine);

                start += Columns;
            }

            return toDisplay;
        }
    }
}
