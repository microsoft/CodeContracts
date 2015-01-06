#include <process.h> // _execv
#include <direct.h>  // _mkdir
#include <string.h>  // strrchr
#include <stdio.h>   // fopen and friends
#include <windows.h>
#include <msi.h>
#include <msiquery.h>


/*---------------------------------------------------------------
   Custom Contents
---------------------------------------------------------------*/

#define ContractsStartSequence "\n  <!-- Begin Microsoft Code Contracts -->\n"
#define ContractsEndSequence   "\n  <!-- End Microsoft Code Contracts -->\n"

// Add more content to template here
static const char* contractsContentTemplate 
  = ContractsStartSequence 
    "  <PropertyGroup>\n"
    "    <CodeContractsInstallDir>%s</CodeContractsInstallDir>\n"
    "  </PropertyGroup>\n"
    "  <Import Condition=\"'$(CodeContractsImported)' != 'true' AND '$(DontImportCodeContracts)' != 'true'\" Project=\"%s\"/>\n" 
    ContractsEndSequence;

static const char* projectStart
  = "<Project xmlns=\"http://schemas.microsoft.com/developer/msbuild/2003\">";
  
static const char* projectEnd
  = "\n</Project>\n";

/*---------------------------------------------------------------
   String Helpers
---------------------------------------------------------------*/
static long strLen( const char* s )
{
  if (s==NULL) return 0;
  long n = 0;
  while (s[n] != 0) n++;
  return n;
}

static void strCpy( char* dest, const char* src, long n )
{
  if (dest==NULL || n<=0) return;
  dest[0] = 0;
  if (src==NULL) return;
  long i = 0;
  while (i < (n-1) && src[i] != 0) {
    dest[i] = src[i];
    i++;
  }
  dest[i] = 0;
}

static void strCat( char* dest, const char* src, long n )
{
  if (dest==NULL || src==NULL || n<=0) return;

  long len = 0;
  while (len < (n-1) && dest[len] != 0) len++;
  
  long i = 0;
  while (len+i < (n-1) && src[i] != 0) {
    dest[len+i] = src[i];
    i++;
  }
  dest[i] = 0;
}

static long strStrI( const char* txt, const char* pat )
{
  if (txt==NULL || pat==NULL) return NULL;
  long patLen = strLen(pat);
  if (patLen==0) return NULL;
  
  for( const char* p = txt; *p != 0; *p++ ) {
    if (*p == *pat && strnicmp(p,pat,patLen) == 0) {
      return (long)(p-txt);
    }
  }
  return (-1);
}

static long strStrIend( const char* txt, const char* pat )
{
  long i = strStrI(txt,pat);
  if (i >= 0)
    return (i + strLen(pat));
  else
    return i;
}

/*---------------------------------------------------------------
   File Helpers
---------------------------------------------------------------*/
static size_t fsize( FILE* f )
{
  if (f==NULL) return 0;
  if (fseek(f,0,SEEK_END) != 0) return 0;
  size_t size = ftell(f);
  fseek(f,0,SEEK_SET);
  return size;
}

static char* readFile( const char* fname )
{
  char* text = NULL;
  FILE* f = fopen( fname, "r" );
  if (f!=NULL) {
    size_t fileSize = fsize(f);
    if (fileSize > 0) {   
      text = (char*)malloc(fileSize+16);
      if (text!=NULL) {
        size_t len = fread( text, 1, fileSize, f );
        if (feof(f)==0) {
          free(text); 
          text = NULL; 
        }
        else {
          text[len] = 0;  
        }
      }
    }
    fclose(f);
  }
  return text;
}

static void makeDir( const char* dirName )
{
  if (dirName==NULL) return;

  // create superdirs
  char dir[MAX_PATH];
  strCpy(dir,dirName,MAX_PATH);

  char* p = dir;
  if (dir[1] == ':' && strLen(dirName) > 3) {
	 p = dir+3; // skip drive designator
  }

  // create all intermediate directories
  int err;
  while (1)
  {
    p = strchr(p,'\\');
    if (p==NULL) break;

    *p = 0;
    _mkdir(dir);
	*p = '\\';
    p++;  // step beyond the '\'
  }

  // final creation
  _mkdir(dirName);  
  return;

}

static void ensureDir( const char* fname )
{
  if (fname==NULL) return;

  char dir[MAX_PATH];
  strCpy(dir,fname,MAX_PATH);
  char* p = strrchr(dir,'\\');
  if (p != NULL)
  {
    *p = 0;
    makeDir(dir);
  }
}

/*---------------------------------------------------------------
   Uninstall / Install
---------------------------------------------------------------*/
enum Result 
{
  Ok,
  ErrInvalidFile,
  ErrUnknown
};

static Result uninstall( const char* buildFileName )
{
  /* read the file and find the contracts section */
  char* text = readFile( buildFileName );
  if (text != NULL) {
    long start = strStrI( text, ContractsStartSequence );
    if (start >= 0) {
      long end = strStrIend( text+start, ContractsEndSequence );
      if (end >= 0) {
        /* rewrite the file contents without the contracts section */
        FILE* f = fopen( buildFileName, "w" );
        if (f != NULL) {
          fwrite( text, 1, start, f );
          fwrite( text+start+end, 1, strLen(text)-start-end, f );
          fclose(f);
        }
      }
    }
    free(text);
  }
  return Ok;
}
  
//
//  installDir should be the installation dir for Code Contracts
//
static Result install( const char* buildFileName, 
                       const char* installDir, 
                       const char* customTargets )
{
  Result result = ErrUnknown;
  
  char* contractsContents = NULL;
  char* text = NULL;
  FILE* f = NULL;
  
  /* initialize contents */
  long contentsLen = strLen(contractsContentTemplate) 
                      + strLen(customTargets) + strLen(installDir) + 16;
  contractsContents = (char*)malloc( contentsLen+1 );
  if (contractsContents==NULL) goto err;
  // Change this if you add more template parameters to the template
  sprintf( contractsContents, contractsContentTemplate, installDir, customTargets );
  
  /* remove a previous installation */
  uninstall(buildFileName);  
  
  /* find insertion point */
  text = readFile(buildFileName);
  if (text==NULL) {
    /* write a fresh file */
    ensureDir( buildFileName ); /* ensure the directory exists */
    f = fopen( buildFileName, "w" );
    if (f == NULL) { goto err; }
    fwrite( projectStart, 1, strLen(projectStart), f );
    fwrite( contractsContents, 1, strLen(contractsContents), f );
    fwrite( projectEnd, 1, strLen(projectEnd), f );    
    result = Ok;
  }
  else {
    /* update an existing file */
    long project = strStrIend(text,"<Project");
    if (project < 0) { result = ErrInvalidFile; goto err; }
    
    long insert = project+strStrIend(text+project, ">");
    if (insert <= project) { result = ErrInvalidFile; goto err; }
    
    /* rewrite the file contents with our contracts section */
    f = fopen( buildFileName, "w" );
    if (f == NULL) { goto err; }   
    fwrite( text, 1, insert, f );
    fwrite( contractsContents, 1, strLen(contractsContents), f );
    fwrite( text+insert, 1, strLen(text)-insert, f );
    result = Ok;
  }
    
err:
  if (f != NULL) fclose(f);
  if (contractsContents!=NULL) free(contractsContents);
  if (text!=NULL) free(text);
  return result;
}


/*---------------------------------------------------------------
   Custom action entry point
---------------------------------------------------------------*/

typedef enum {
	ValBuildFile,
  ValCustomTargets,
	ValInstallDir,
	ValRemove,
  _ValCount
} Values;

UINT __stdcall AddBuildTargets(MSIHANDLE hInstall) 
{
  Result result = ErrUnknown;

  /* Read the "CustomActionData" property. Due to UAC restrictions, we need to run the custom action as 'deferred' 
     and this is the only property that is available in that case. Therefore, this property contains a set of strings
     separated by semicolons. Currently in the order: MSBuildFile, CustomTargets file, Spec# install dir, and the REMOVE flag .
  */
  char customData[1024+1];
  DWORD customDataLen = 1024;
  customData[0] = 0;
  MsiGetProperty( hInstall, "CustomActionData", customData, &customDataLen );
  customData[customDataLen] = 0;  // paranoia

  // Parse all strings into the values array (indexed by the Values enumeration)
  const char* values[_ValCount];
  const char* empty = "";
  char* p = customData;
  for (int i = 0; i < _ValCount; i++)
  {
    if (p == NULL) {
      values[i] = empty;
    }
    else {
      values[i] = p;
      p = strchr(p,';');
      if (p != NULL) {
        *p = 0;
        p++;
      }
    }
  }
  
  
  /*
  char message[1024];
  _snprintf( message, 1024, "%s - %s - %s - %s .", values[ValBuildFile], values[ValCustomTargets], values[ValInstallDir], values[ValRemove] );
  MessageBox( NULL, message, "Microsoft Contracts", MB_OK );
  */
    
  if (strLen(values[ValRemove])==0) //  || stricmp(values[ValRemove],"ALL") != 0)
  {
    result = install( values[ValBuildFile], values[ValInstallDir], values[ValCustomTargets] );
  }
  else {
    result = uninstall( values[ValBuildFile] );
  }
  
  /* Show message box on error */  
  if (result != Ok) {
    char message[1024];
    const char* errmsg;
    switch (result) {
      case ErrInvalidFile: errmsg = "Invalid custom MSBuild targets file"; break;
      default:             errmsg = "Unable to change the custom MSBuild targets file"; break;
    }
    _snprintf( message, 1024, "Error: %s (%s).\n\nPlease ensure that the installer runs with administrator privileges.", errmsg, values[ValBuildFile] );
    MessageBox( NULL, message, "Microsoft Contracts", MB_OK );      
  }
  return (result==Ok ? ERROR_SUCCESS : -1);
}

/*---------------------------------------------------------------
   Testing 
---------------------------------------------------------------*/

int main()
{
  install( "C:\\Program Files\\msbuild\\v1.0\\test1.targets", "myinstalldir", "myinstalldir\\custom.targets" );
}
