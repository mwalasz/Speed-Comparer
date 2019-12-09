.code

DllEntry proc hInstDLL: DWORD, reason: DWORD, reserved1: DWORD
	mov RAX, 1
	ret
DllEntry endp

;----------------------------------------------------------------------
; Arguments passed to function:
; RCX - pointer to array of floats
; RDX - scalar number
; R8 - length of array of floats
;----------------------------------------------------------------------

MatrixScalarMultiplication proc
;saving registers to stack to restore them after function execution
	push RSI
	push RAX
	push RBX
	jmp endProgram

;restoring registers to original state from before function execution
endProgram:
	pop	RBX
	pop	RAX
	pop	RSI
	ret

MatrixScalarMultiplication endp

;----------------------------------------------------------------------

end
