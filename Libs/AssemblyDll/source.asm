;----------------------------------------------------------------------
.CODE

DllEntry PROC hInstDLL: DWORD, reason: DWORD, reserved1: DWORD
	mov rax, 1
	ret
DllEntry ENDP
;----------------------------------------------------------------------

MatrixScalarMultiplication PROC matrix: PTR, scalar: REAL4, len: DWORD
local skalar: DWORD

mov r11, [rcx]
movss xmm7, xmm0
mov ebx, skalar
mov eax, [ebx + 1]
mov eax, [ebx + 2]
mov eax, [ebx + 3]
ret

MatrixScalarMultiplication ENDP

END
;----------------------------------------------------------------------