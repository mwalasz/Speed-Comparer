;----------------------------------------------------------------------
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
;vbroadcastss xmm1, xmm2

local skalar: DWORD

movss xmm7, xmm0
mov ebx, skalar
mov eax, [ebx + 1]
mov eax, [ebx + 2]
mov eax, [ebx + 3]
ret

MatrixScalarMultiplication ENDP

END DllEntry