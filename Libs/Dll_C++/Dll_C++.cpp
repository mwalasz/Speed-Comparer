#include "framework.h"

extern "C" {
	__declspec(dllexport)
		void test(float matrix[], float scalar, int matrixLength)
	{
		int dupa = 1;

		for (size_t i = 0; i < matrixLength; i++)
		{
			matrix[i] *= scalar;
		}
	}
}