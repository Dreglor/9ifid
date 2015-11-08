/*****************************************************************************\
|*
|* Copyright(c) 2015, Steven 'Dreglor' Garcia
|* All rights reserved.
|*
|* Redistribution and use in source and binary forms, with or without 
|* modification, are permitted provided that the following conditions are met:
|* 1. Redistributions of source code must retain the above copyright notice, 
|*		this list of conditions and the following disclaimer.
|* 2. Redistributions in binary form must reproduce the above copyright
|*		notice, this list of conditions and the following disclaimer in the
|*		documentation and / or other materials provided with the distribution.
|* 3. Neither the name of the copyright holder nor the names of its 
|*		contributors may be used to endorse or promote products derived from 
|*		this software without specific prior written permission.
|*
|* THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS"
|* AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE
|* IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE
|* ARE DISCLAIMED.IN NO EVENT SHALL THE COPYRIGHT HOLDER OR CONTRIBUTORS BE
|* LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR
|* CONSEQUENTIAL DAMAGES(INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF
|* SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR PROFITS; OR
|* BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY,
|* WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT(INCLUDING NEGLIGENCE OR 
|* OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF
|* ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
|*
\*****************************************************************************/

#define WIN32_LEAN_AND_MEAN
#include <windows.h>
#include <msclr\marshal.h>

#include "dllmain.h"

using namespace System;
using namespace msclr::interop;
using namespace lib9ifid;

#pragma unmanaged
BOOL APIENTRY DllMain(HMODULE hModule, DWORD  ul_reason_for_call, LPVOID lpReserved)
{
	switch (ul_reason_for_call)
	{
	case DLL_PROCESS_ATTACH:
	case DLL_THREAD_ATTACH:
	case DLL_THREAD_DETACH:
	case DLL_PROCESS_DETACH:
		break;
	}
	return TRUE;
}
#pragma managed

void STDCALL RVExtension(char *output, int outputSize, const char *function)
{
	//build a garbage collector & convert the "function" string into a managed one
	marshal_context^ context = gcnew marshal_context();
	String^ input = marshal_as<String^>(function);

	//call the C# side of extention and copy the return back to ARMA
	String^ result = Extension::Invoke(input);
	strncpy_s(output, outputSize, context->marshal_as<const char*>(result), _TRUNCATE);

	//ensure we clean up
	delete context;
}