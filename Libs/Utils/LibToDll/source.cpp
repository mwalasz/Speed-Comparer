#include "windows.h"

extern "C" __declspec(dllimport) void MatrixScalarMultiplication(float matrix[], float scalar, int matrixLength);

extern "C" __declspec (dllexport) void asmMatrixScalarMultiplication(float matrix[], float scalar, int matrixLength)
{
	MatrixScalarMultiplication(matrix, scalar, matrixLength);
}