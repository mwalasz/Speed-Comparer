.code

DllEntry proc hInstDLL: DWORD, reason: DWORD, reserved1: DWORD
	mov RAX, 1
	ret
DllEntry endp

;----------------------------------------------------------------------
; Arguments passed to function:
; RCX - pointer to array of floats
; XMM0 - scalar number
; R8  - length of array of floats
;----------------------------------------------------------------------
; Used registers in function:
;----------------------------------------------------------------------

MatrixScalarMultiplication proc

;saving registers to stack to restore them after function execution
	push RSI
	push RAX
	push RBX
	push RCX
	PUSH R8
	PUSH RDX


;load data
	mov RSI, RCX ;load array
	mov RBX, R8  ;load array length
	mov RDX, 4 ;liczba przetwarzanych elementow

	;VMOVQ xmm15, xmm0

	cmp rbx, rdx
	jb mnozenie_pojedyncze

mnozenie_wektorowe:
	movups xmm1, [rsi] ;za³adowanie 4 floatow do rejestru
	vpbroadcastd xmm2, xmm0 ;za³adowanie do kazdej z komorek xmm1 skalara
	vmulps xmm1, xmm2, xmm1 ;mnozenie xmm0 * xmm1 i zapisanie do xmm0
	movups [rsi], xmm1
	
	add rsi, 16 ;przesuniecie sie o 4 miejsca w tablicy
	sub rbx, RDX ;odjecie 4 przerobionych elementow od rozmiaru tablicy
	
	cmp rbx, RDX ;porownanie liczby elementow w tablicy i 4 (liczba el do przetworzenia)
	jae mnozenie_wektorowe ;jesli liczba elementow >= 4

mnozenie_pojedyncze:
	;mov rax, [rsi] ;zaladowanie wartosci elementu do rda
	;movq rcx, xmm0 ;wrzucenie skalaru do rcx
	;mul rcx

	movups xmm4, [rsi]
	vmulss xmm5, xmm0, xmm4
	movd rax, xmm5

	mov [rsi], rax ; zapisanie wyniku mnozenia do tablicy
	dec rbx ;dekrementacja liczby elementow tablicy
	add rsi, rdx ; dodanie do licznika tablicy 4 -> zeby sie przesunac w prawo do kolejnego elementu
	cmp rbx, 0
	jne mnozenie_pojedyncze

;restoring registers to original state from before function execution
endProgram:
	pop RDX
	pop R8
	pop RCX
	pop	RBX
	pop RAX
	pop	RSI
	ret

MatrixScalarMultiplication endp

;----------------------------------------------------------------------

end
