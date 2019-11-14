;----------------------------------------------------------------------
.XMM

.MODEL FLAT, STDCALL

OPTION CASEMAP: NONE

INCLUDE c:\masm32\include\windows.inc

.code

DllEntry PROC hInstDll: HINSTANCE, reason: DWORD, reserved1: DWORD
mov eax, TRUE
ret
DllEntry ENDP

;----------------------------------------------------------------------

MatrixScalarMultiplication PROC matrix: DWORD, scalar: REAL4, len: DWORD
mov eax, 123
ret

MatrixScalarMultiplication ENDP

END DllEntry