using MainProgram.Maths;

namespace MainProgram.Files
{
    public class LoadedData
    {
        public Matrix Matrix { get; set; }
        public Scalar Scalar { get; set; }

        public LoadedData(Matrix matrix, Scalar scalar)
        {
            Matrix = matrix;
            Scalar = scalar;
        }
    }
}
