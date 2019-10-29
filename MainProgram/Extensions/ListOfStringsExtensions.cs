using MainProgram.Exceptions;
using System.Collections.Generic;
using System.Linq;

namespace MainProgram.Extensions
{
    public static class ListOfStringsExtensions
    {
        public static float[,] ConvertToMatrix(this List<string> data)
        {
            data.RemoveAt(0); //remove first element which is scalar
            
            var numberOfRows = data.Count;
            var numberOfColumns = data[0].Split(' ').Length;

            var matrix = new float[numberOfRows, numberOfColumns];

            int currentRow = 0;

            foreach(var line in data)
            {
                var convertedRow = line.Split(' ')
                    .Select(number => float.Parse(number, System.Globalization.CultureInfo.InvariantCulture))
                    .ToArray();

                if (convertedRow.Length != numberOfColumns)
                    throw new MatrixDataException();

                for (int col = 0; col < numberOfColumns; col++)
                    matrix[currentRow, col] = convertedRow[col];

                currentRow++;
            }

            return matrix;
        }
    }
}
