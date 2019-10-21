.686
.XMM
.MODEL FLAT, STDCALL

OPTION CASEMAP: NONE

INCLUDE c:\masm32\include\windows.inc

.code

DllEntry PROC hInstDll: HINSTANCE, reason: DWORD, reserved1: DWORD
mov eax, TRUE
ret
DllEntry ENDP


AsmVal proc
mov eax, 999
ret
AsmVal ENDP

END DllEntry