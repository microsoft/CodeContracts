// CodeContracts
// 
// Copyright (c) Microsoft Corporation
// 
// All rights reserved. 
// 
// MIT License
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED *AS IS*, WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

#if false

using System.Diagnostics.Contracts;
class Problem2
{
const int SIZE_T_MAX = 10000;
const int RP_ECHO			= 0x0001;
const int RP_ALLOW_STDIN= 		0x0002;
const int RP_ALLOW_EOF	= 	0x0004;
const int RP_USE_ASKPASS	= 	0x0008;

const int	EINVAL		= 22;	/* Invalid argument */
const int	ENOTTY		= 25;	/* Not a typewriter */

const int	SSH_PROTO_UNKNOWN	= 0x00;
const int	SSH_PROTO_1		= 0x01;
const int	SSH_PROTO_1_PREFERRED	= 0x02;
const int	SSH_PROTO_2		= 0x04;

//typedef unsigned char u_char;
//typedef unsigned short int u_short;
//typedef unsigned int u_int;
//typedef unsigned long int u_long;
//typedef int pid_t;
//typedef unsigned int socklen_t;

/* Read NBYTES into BUF from FD.  Return the number read, -1 for errors or 0 for EOF.
This function is a cancellation point and therefore not marked with __THROW.  */
//extern ssize_t read (int __fd, void *__buf, size_t __nbytes) __wur;

const int	EINPROGRESS	= 115;	/* Operation now in progress */
const int	ETIMEDOUT	= 110;	/* Connection timed out */
const int	EINTR		 = 4;	/* Interrupted system call */

const int SOL_SOCKET	= 1;
const int SO_ERROR	= 4;
const int SO_KEEPALIVE	= 9;

const int RPP_ECHO_OFF    = 0x00;		/* Turn off echo (default). */
const int RPP_ECHO_ON     = 0x01;		/* Leave echo on. */
const int RPP_REQUIRE_TTY = 0x02;		/* Fail if there is no tty. */
const int RPP_FORCELOWER  = 0x04;		/* Force input to lower case. */
const int RPP_FORCEUPPER  = 0x08;		/* Force input to upper case. */
const int RPP_SEVENBIT    = 0x10;		/* Strip the high bit from input. */
const int RPP_STDIN       = 0x20;		/* Read from stdin, not /dev/tty */

const string  _PATH_TTY = "/dev/tty";
const string _PATH_SSH_ASKPASS_DEFAULT	= "/usr/X11R6/bin/ssh-askpass";

/* Standard file descriptors.  */
const int	STDIN_FILENO	=0;	/* Standard input.  */
const int	STDOUT_FILENO	=1;	/* Standard output.  */
const int	STDERR_FILENO	=2;	/* Standard error output.  */
/*
* Maximum number of RSA authentication identity files that can be specified
* in configuration files or on the command line.
*/
const int SSH_MAX_IDENTITY_FILES		=100;

/* Maximum number of TCP/IP ports forwarded per direction. */
const int SSH_MAX_FORWARDS_PER_DIRECTION	=100;
const int MAX_SEND_ENV	=256;
/*
* Environment variable for overwriting the default location of askpass
*/
const string SSH_ASKPASS_ENV	= "SSH_ASKPASS";

//static char * ssh_askpass(char *askpass, const char *msg);

struct Key {
  int	 type;
  int	 flags;
  int[] rsa;
  int[] dsa;
}

  // f: added
  struct termios
  {
    public int c_lflag;
  }

const int ECHO	=0000010;
const int ECHOE	=0000020;
const int ECHOK	=0000040;
const int ECHONL	=0000100;
const int NOFLSH	=0000200;
const int TOSTOP	=0000400;
const int  ECHOCTL =0001000;
const int  ECHOPRT =0002000;
const int  ECHOKE	 =0004000;
const int  FLUSHO	 =0010000;
const int  PENDIN	 =0040000;

/* tcsetattr uses these */
const int	TCSANOW		=0;
const int	TCSADRAIN	=1;
const int	TCSAFLUSH	=2;

const int _T_FLUSH	= 12345; // (TCSAFLUSH)
  //----------------------------------------

/* Fatal messages.  This function never returns. */

  /*
  void
fatal(const char *fmt,...)
{
  exit(1);
}
  */
/* Duplicates a character string. This function only returns NULL if s is passed as NULL.
*/
  [ContractVerification(false)] // was extern
string xstrdup (string s)
{
Contract.Ensures(s != null || Contract.Result<string>() == null);
    Contract.Ensures(s == null || Contract.Result<string>() != null);

    return s;
  }

/*
* Frees the memory pointed to by ptr. Aborts if ptr is NULL.
*/
  [ContractVerification(false)] // was extern
void xfree(object ptr)
{
  Contract.Requires(ptr != null);
}
  
  // defined in C library
  [ContractVerification(false)] // defined in C library
  int open(string name, int option) 
  {
    Contract.Requires(name != null);
    return -1;
  }

  [ContractVerification(false)]
  private unsafe void memcpy(object p,object p_2,int p_3)
{
 	throw new System.NotImplementedException();
}

  [ContractVerification(false)]
private int tcgetattr(int input, ref termios oterm)
{
 	throw new System.NotImplementedException();
}


  // f: Added by me
  private  int O_RDWR;


int errno;
/* import */
extern string __progname;
extern int  original_real_uid;
extern int  original_effective_uid;
extern int proxy_command_pid;
static volatile int signo;
//typedef unsigned char	cc_t;
//typedef unsigned int	speed_t;
//typedef unsigned int	tcflag_t;

unsafe string readpassphrase(string prompt,  string buf, uint bufsiz, int flags)
{
  //ssize_t nr;
  int input, output, save_errno;
  char ch;
  string p, end;

  /*struct */termios term, oterm; // f: where termios is defined???
  
  /* I suppose we could alloc on demand in this case (XXX). */
  if (bufsiz == 0) {
    errno = EINVAL;
    return null;
  }
  
  restart:
  signo = 0;
  /*
  * Read and write to /dev/tty if available.  If not, read from
  * stdin and write to stderr unless a tty is required.
  */
  if ((flags & RPP_STDIN) != 0||
    (input = output = open(_PATH_TTY, O_RDWR)) == -1) {
    if ((flags & RPP_REQUIRE_TTY) != null) {
      errno = ENOTTY;
      return null;
    }
    input = STDIN_FILENO;
  output = STDERR_FILENO;
  }
    
  /* Turn off echo if possible. */
  if (input != STDIN_FILENO && tcgetattr(input, ref oterm) == 0) {
    // F: commented
    // memcpy(term, oterm, term);

    if ((flags & RPP_ECHO_ON) == 0)
      term.c_lflag &= ~(ECHO | ECHONL);
    #if VSTATUS
    if (term.c_cc[VSTATUS] != _POSIX_VDISABLE)
      term.c_cc[VSTATUS] = _POSIX_VDISABLE;
    #endif
    tcsetattr(input, _T_FLUSH, ref term);
  } else {
    memset(ref term, 0, sizeof(termios));
    term.c_lflag |= ECHO;
    memset(ref oterm, 0, sizeof(termios));
    oterm.c_lflag |= ECHO;
  }
  
  if ((flags & RPP_STDIN) == 0)
    write(output, prompt, prompt.Length);
  end = buf + bufsiz - 1;
  for (p = buf; p<end; ) {
    nr = read(input, &ch, 1);
    if ((flags & RPP_SEVENBIT))
      ch &= 0x7f;
    if (isalpha(ch)) {
      if ((flags & RPP_FORCELOWER))
	ch = tolower(ch);
      if ((flags & RPP_FORCEUPPER))
	ch = toupper(ch);
    }
    *p++ = ch;
    
  }
  *p = '\0';
  save_errno = errno;
  if (!(term.c_lflag & ECHO))
    (void)write(output, "\n", 1);
  
  /* Restore old terminal settings and signals. */
  if (memcmp(&term, &oterm, sizeof(term)) != 0) {
    while (tcsetattr(input, _T_FLUSH, &oterm) == -1 &&
      errno == EINTR)
      continue;
  }

  /*
  * If we were interrupted by a signal, resend it to ourselves
  * now that we have restored the signal handlers.
  */
  if (signo) {
    kill(getpid(), signo);
    switch (signo) {
      case SIGTSTP:
      case SIGTTIN:
      case SIGTTOU:
	goto restart;
    }
  }
  
  errno = save_errno;
  return(nr == -1 ? NULL : buf);
}

  [ContractVerification(false)]
private object write(int output,string prompt,int p)
{
 	throw new System.NotImplementedException();
}

  [ContractVerification(false)]
private void memset(ref termios term,int p,int p_2)
{
 	throw new System.NotImplementedException();
}

  [ContractVerification(false)]
private void tcsetattr(int input,int _T_FLUSH,ref termios term)
{
 	throw new System.NotImplementedException();
}


/*
* Reads a passphrase from /dev/tty with echo turned off/on.  Returns the
* passphrase (allocated with xmalloc).  Exits if EOF is encountered. If
* RP_ALLOW_STDIN is set, the passphrase will be read from stdin if no
* tty is available
*/
char *
read_passphrase(const char *prompt, int flags)
{
  char *askpass = NULL, *ret, buf[1024];
  int rppflags, use_askpass = 0, ttyfd;
  
  rppflags = (flags & RP_ECHO) ? RPP_ECHO_ON : RPP_ECHO_OFF;
  if (flags & RP_USE_ASKPASS)
    use_askpass = 1;
  else if (flags & RP_ALLOW_STDIN) {
    if (!isatty(STDIN_FILENO)) {
      debug("read_passphrase: stdin is not a tty");
      use_askpass = 1;
    }
  } else {
    rppflags |= RPP_REQUIRE_TTY;
    ttyfd = open(_PATH_TTY, O_RDWR);
    if (ttyfd >= 0)
      close(ttyfd);
    else {
      debug("read_passphrase: can't open %s: %s", _PATH_TTY,
	    strerror(errno));
	    use_askpass = 1;
    }
  }
  
  if ((flags & RP_USE_ASKPASS) && getenv("DISPLAY") == NULL)
    return (flags & RP_ALLOW_EOF) ? NULL : xstrdup("");
  
  if (use_askpass && getenv("DISPLAY")) {
    if (getenv(SSH_ASKPASS_ENV))
      askpass = getenv(SSH_ASKPASS_ENV);
    else
      askpass = _PATH_SSH_ASKPASS_DEFAULT;
    if ((ret = ssh_askpass(askpass, prompt)) == NULL)
      if (!(flags & RP_ALLOW_EOF))
	return xstrdup("");
      return ret;
  }
  
  if ((flags & RP_USE_ASKPASS) &&
    readpassphrase(prompt, buf, sizeof buf, rppflags) == NULL) {  
    return NULL;
  }
  
  ret = xstrdup(buf);
  memset(buf, 'x', sizeof buf);
  return ret;
}

static char *
ssh_askpass(char *askpass, const char *msg)
{
  pid_t pid, ret;
  size_t len;
  char *pass;
  int p[2], status;
  char buf[1024];
  void (*osigchld)(int);
  
  if (fflush(stdout) != 0)
    error("ssh_askpass: fflush: %s", strerror(errno));
  if (askpass == NULL)
    fatal("internal error: askpass undefined");
  if (pipe(p) < 0) {
    error("ssh_askpass: pipe: %s", strerror(errno));
    return NULL;
  }
  osigchld = signal(SIGCHLD, SIG_DFL);
  if ((pid = fork()) < 0) {
    error("ssh_askpass: fork: %s", strerror(errno));
    signal(SIGCHLD, osigchld);
    return NULL;
  }
  if (pid == 0) {
    permanently_drop_suid(getuid());
    close(p[0]);
    if (dup2(p[1], STDOUT_FILENO) < 0)
      fatal("ssh_askpass: dup2: %s", strerror(errno));
    execlp(askpass, askpass, msg, (char *) 0);
    fatal("ssh_askpass: exec(%s): %s", askpass, strerror(errno));
  }
  close(p[1]);
  
  len = 0;
  do {
    ssize_t r = read(p[0], buf + len, sizeof(buf) - 1 - len);
    
    if (r == -1 && errno == EINTR)
      continue;
    if (r <= 0)
      break;
    len += r;
  } while (sizeof(buf) - 1 - len > 0);
  buf[len] = '\0';
  
  close(p[0]);
  while ((ret = waitpid(pid, &status, 0)) < 0)
    if (errno != EINTR)
      break;
    signal(SIGCHLD, osigchld);
  if (ret == -1 || !WIFEXITED(status) || WEXITSTATUS(status) != 0) {
    memset(buf, 0, sizeof(buf));
    return NULL;
  }
  
  buf[strcspn(buf, "\r\n")] = '\0';
  pass = xstrdup(buf);
  memset(buf, 0, sizeof(buf));
  return pass;
}

static int
confirm(const char *prompt)
{
  char *p;
  int ret = -1;
  const char* msg = prompt;
  if(!msg) return 0;
  p = read_passphrase(msg, RP_ECHO);
  static_assert(p!=NULL);
  
  if (p&&
    (p[0] == '\0') || (p[0] == '\n') ||
    strncasecmp(p, "no", 2) == 0)
    ret = 0;
  if (p && strncasecmp(p, "yes", 3) == 0)
    ret = 1;
  if(p)
    xfree(p);
  return ret;  
}
}
#endif