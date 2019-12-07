#include "framework.h"
#include "windows.h"

extern "C" {
	__declspec(dllexport)
		void MatrixScalarMultiplication(float * matrix, float scalar, int matrixLength)
	{
		for (int i = 0; i < matrixLength; i++)
		{
			matrix[i] *= scalar;
		}
	}
}