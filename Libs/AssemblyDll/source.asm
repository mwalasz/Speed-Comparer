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

;wczytanie danych
	mov RSI, RCX ;wczytanie wskaznika na tablice
	mov RBX, R8  ;wczytanie dlugosci tablicy
	mov RDX, 4  ;liczba przetwarzanych elementow tablicy
	;vmovq xmm15, xmm0 ; za³adowanie skalaru do xmm15
	vmovq r9, xmm0 ;wrzucenie skalaru do r9

	cmp rbx, rdx		   ;porownanie liczby elementow w tablicy i 4 (liczba el do przetworzenia)
	jb mnozenie_pojedyncze ;jesli liczba elementow w tablicy jest mniejsza niz 4, skok do mnozenia pojedynczego

mnozenie_wektorowe:
	movups xmm1, [rsi] ;za³adowanie 4 floatow do rejestru
	
	vpbroadcastd xmm2, xmm0 ;za³adowanie do kazdej z komorek xmm1 skalara
	vmulps xmm1, xmm2, xmm1 ;mnozenie xmm15 * xmm1 i zapisanie do xmm15

	movups [rsi], xmm1	;zapisanie wymnozonych danych
	
	add rsi, 16 ;przesuniecie sie o 4 miejsca w tablicy
	sub rbx, rdx ;odjecie 4 przerobionych elementow od rozmiaru tablicy
	
	cmp rbx, rdx ;porownanie liczby elementow w tablicy i 4 (liczba el do przetworzenia)
	jae mnozenie_wektorowe ;jesli liczba elementow >= 4x

mnozenie_pojedyncze:
	;mov rax, [rsi] ;zaladowanie wartosci elementu do rda
	;imul r9
	
	mov eax, [rsi]
	movd xmm4, rax
	mulss xmm4, xmm0
	movd rax, xmm4

	mov [rsi], eax
	dec rbx ;dekrementacja liczby elementow tablicy
	add rsi, rdx ; dodanie do licznika tablicy 4 -> zeby sie przesunac w prawo do kolejnego elementu
	cmp rbx, 0 ;sprawdzenie czy liczba elementow w tablicy wynosi 0
	jne mnozenie_pojedyncze ;jesli nie to mnozymy dalej

	;mov rcx, [rsi]
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
