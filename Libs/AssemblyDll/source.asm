.code

DllEntry proc hInstDLL: DWORD, reason: DWORD, reserved1: DWORD
	mov RAX, 1
	ret
DllEntry endp

;----------------------------------------------------------------------
; Arguments passed to function:
; RCX - pointer to array of floats
; RDX - scalar number
; R8  - length of array of floats
;----------------------------------------------------------------------
; Used registers in function:
; RSI - array
; RAX - scalar number
; RBX - array length
;----------------------------------------------------------------------

MatrixScalarMultiplication proc
;saving registers to stack to restore them after function execution
	push RSI
	push RAX
	push RBX

;load data
	mov RSI, RCX ;load array
	mov RAX, RDX ;load scalar
	mov RBX, R8  ;load array length
	vmovq XMM7, RAX ;load scalar to xmm7

	movups xmm0, [rsi] ;za³adowanie 4 floatow do rejestru

	vbroadcastss xmm1, xmm7 ;za³adowanie do kazdej z komorek xmm1 skalara
	vmulps xmm0, xmm1, xmm0 ;mnozenie xmm0 * xmm1 i zapisanie do xmm0
	movups [rsi], xmm0

;restoring registers to original state from before function execution
endProgram:
	pop	RBX
	pop	RAX
	pop	RSI
	ret

MatrixScalarMultiplication endp

;----------------------------------------------------------------------

end
