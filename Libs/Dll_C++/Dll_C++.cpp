#include "framework.h"
#include "windows.h"

extern "C" {
	__declspec(dllexport)
		void test(float matrix[], float scalar, int matrixLength)
	{
		for (size_t i = 0; i < matrixLength; i++)
		{
			matrix[i] *= scalar;
		}

		//Sleep(500);
	}
}