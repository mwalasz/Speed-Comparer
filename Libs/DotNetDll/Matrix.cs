namespace DotNetDll
{
    public class Matrix
    {
        private readonly int[,] content; 
        
        public int Columns { get; set; }
        public int Rows { get; set; }

        public Matrix(int rows, int columns)
        {
            Rows = rows;
            Columns = columns;

            content = new int[Rows, Columns];
        }

        public Matrix(int [,] matrix)
        {
            Rows = matrix.GetLength(0);
            Columns = matrix.GetLength(1);

            content = matrix;
        }

        public Matrix ScalarMultiplication(int scalar)
        {
            return new Matrix(1,1);
        }
    }
}
