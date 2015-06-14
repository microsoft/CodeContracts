// SAL.cpp : Defines the entry point for the console application.
//

#include "stdafx.h"

#include <malloc.h>

void fill_string(
      __out_ecount(cchBuf) TCHAR* buf,   
      size_t cchBuf,   
      char ch) {  

  for (size_t i = 0; i < cchBuf; i++)   {     
    buf[i] = ch;   
  } 
} 

int _tmain(int argc, _TCHAR* argv[])
{
	TCHAR *b = (TCHAR*)malloc(200*sizeof(TCHAR));
	fill_string(b,210,'x');

	return 0;
}
