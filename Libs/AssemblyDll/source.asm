.code

;----------------------------------------------------------------------
; Arguments passed to function:
; RCX - pointer to array of floats
; XMM0 - scalar number to multiply for
; R8  - length of array of floats
;----------------------------------------------------------------------

MatrixScalarMultiplication proc

	;saving registers to stack to restore them after function execution
	push RSI
	push RAX
	push RBX
	push RCX
	push R8
	push RDX

	mov RSI, RCX				;loading pointer to array
	mov RBX, R8					;loading array length
	mov RDX, 4					;saving 4 as number of numbers to process
	
	cmp RBX, RDX				;comparing number of numbers to process with possible number to process (4)
	jb single_multiplication	;if number of numbers to process is smaller than 4 then go to single_multiplication section
									;if not starting vector_multiplication

vector_multiplication:
	movups XMM1, [RSI]			;loading 4 float numbers from array to XMM1 register
	vpbroadcastd XMM2, XMM0		;broadcasting scalar number to every cell of vector
	vmulps XMM1, XMM2, XMM1		;multiplying scalar by floats from array and storing them in XMM1 register
	movups [RSI], XMM1			;saving processed data back to array
	
	add RSI, 16					;moving further in array by 4 positions (float = 4 bytes, 4 number * 4 bytes = 16)
	sub RBX, RDX				;subtracting 4 from size of the array (because of 4 processed numbers)
	cmp RBX, RDX				;comparing number of numbers to process with possible number to process (4)
	jae vector_multiplication	;if number of elements to process is higher than 4 then going back to vector_multiplication
									;if not starting single_multiplication 
									;if it is equal to 0 then ending function
	cmp RBX, 0
	je end_of_function

single_multiplication:
	mov RAX, [RSI]				;loading float number from array to RAX register
	movd XMM4, RAX				;loading float number to XMM4 register
	mulss XMM4, XMM0			;multiplying scalar by float
	movd RAX, XMM4				;loading processed float number back to RAX register

	mov [RSI], RAX				;saving processed data back to array
	dec RBX						;decrementing size of array because of processed one element
	add RSI, RDX				;moving further in array by 1 position
	cmp RBX, 0					;checking if number of elements in array is equal to 0
	jne single_multiplication	;if it is equal to 0 then end function
									;if not going back to single_multiplication

	;restoring registers to values from before function execution
end_of_function:
	pop RDX
	pop R8
	pop RCX
	pop	RBX
	pop RAX
	pop	RSI

	ret

MatrixScalarMultiplication endp

end
