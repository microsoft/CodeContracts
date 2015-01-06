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

//#define PROBLEM_4

#if PROBLEM_4
using System.Diagnostics.Contracts;


public struct Options
{
  public int number_of_password_prompts;    /* Max number of password prompts. */
  public int cipher;        /* Cipher to use. */
}

class Problem4
{
  const int NULL = 0;
  const int SIZE_T_MAX = 10000;

  const int RP_ECHO = 0x0001;
  const int RP_ALLOW_STDIN = 0x0002;
  const int RP_ALLOW_EOF = 0x0004;
  const int RP_USE_ASKPASS = 0x0008;
  /*
  typedef unsigned char        uint8_t;
  typedef unsigned short int    uint16_t;
  typedef unsigned int        uint32_t;
  typedef unsigned long int    uint64_t;
  typedef unsigned char u_char;
  typedef unsigned short int u_short;
  typedef unsigned int u_int;
  typedef unsigned long int u_long;
  typedef int pid_t;
  typedef unsigned int socklen_t;
    */

  const int RPP_ECHO_OFF = 0x00;        /* Turn off echo (default). */
  const int RPP_ECHO_ON = 0x01;       /* Leave echo on. */
  const int RPP_REQUIRE_TTY = 0x02;       /* Fail if there is no tty. */



  const string _PATH_TTY = "/dev/tty";
  const string _PATH_SSH_ASKPASS_DEFAULT = "/usr/X11R6/bin/ssh-askpass";

  /* Standard file descriptors.  */
  const int STDIN_FILENO = 0; /* Standard input.  */
  const int STDOUT_FILENO = 1; /* Standard output.  */
  const int STDERR_FILENO = 2;/* Standard error output.  */


  const int EPIPE = 32;   /* Broken pipe */

  /*
  * Environment variable for overwriting the default location of askpass
  */
  const string SSH_ASKPASS_ENV = "SSH_ASKPASS";

  /* Message name */
  /* msg code */
  /* arguments */
  const int SSH_MSG_NONE = 0;	/* no message */
  const int SSH_MSG_DISCONNECT = 1;	/* cause (string) */
  const int SSH_SMSG_PUBLIC_KEY = 2;/* ck,msk,srvk,hostk */
  const int SSH_CMSG_SESSION_KEY = 3;	/* key (BIGNUM) */
  const int SSH_CMSG_USER = 4;/* user (string) */
  const int SSH_CMSG_AUTH_RHOSTS = 5;	/* user (string) */
  const int SSH_CMSG_AUTH_RSA = 6;	/* modulus (BIGNUM) */
  const int SSH_SMSG_AUTH_RSA_CHALLENGE = 7;	/* int (BIGNUM) */
  const int SSH_CMSG_AUTH_RSA_RESPONSE = 8;	/* int (BIGNUM) */
  const int SSH_CMSG_AUTH_PASSWORD = 9;	/* pass (string) */
  const int SSH_CMSG_REQUEST_PTY = 10;	/* TERM, tty modes */
  const int SSH_CMSG_WINDOW_SIZE = 11;	/* row,col,xpix,ypix */
  const int SSH_CMSG_EXEC_SHELL = 12;	/* */
  const int SSH_CMSG_EXEC_CMD = 13;	/* cmd (string) */
  const int SSH_SMSG_SUCCESS = 14;	/* */
  const int SSH_SMSG_FAILURE = 15;	/* */
  const int SSH_CMSG_STDIN_DATA = 16;	/* data (string) */
  const int SSH_SMSG_STDOUT_DATA = 17;	/* data (string) */
  const int SSH_SMSG_STDERR_DATA = 18;	/* data (string) */
  const int SSH_CMSG_EOF = 19;	/* */
  const int SSH_SMSG_EXITSTATUS = 20;	/* status (int) */
  const int SSH_MSG_CHANNEL_OPEN_CONFIRMATION = 21;	/* channel (int) */
  const int SSH_MSG_CHANNEL_OPEN_FAILURE = 22;	/* channel (int) */
  const int SSH_MSG_CHANNEL_DATA = 23;/* ch,data (int,str) */
  const int SSH_MSG_CHANNEL_CLOSE = 24;/* channel (int) */
  const int SSH_MSG_CHANNEL_CLOSE_CONFIRMATION = 25;	/* channel (int) */
  /*      SSH_CMSG_X11_REQUEST_FORWARDING		26	   OBSOLETE */
  const int SSH_SMSG_X11_OPEN = 27;/* channel (int) */
  const int SSH_CMSG_PORT_FORWARD_REQUEST = 28;/* p,host,hp (i,s,i) */
  const int SSH_MSG_PORT_OPEN = 29;/* ch,h,p (i,s,i) */
  const int SSH_CMSG_AGENT_REQUEST_FORWARDING = 30;	/* */
  const int SSH_SMSG_AGENT_OPEN = 31;	/* port (int) */
  const int SSH_MSG_IGNORE = 32;/* string */
  const int SSH_CMSG_EXIT_CONFIRMATION = 33;	/* */
  const int SSH_CMSG_X11_REQUEST_FORWARDING = 34;	/* proto,data (s,s) */
  const int SSH_CMSG_AUTH_RHOSTS_RSA = 35;	/* user,mod (s,mpi) */
  const int SSH_MSG_DEBUG = 36;	/* string */
  const int SSH_CMSG_REQUEST_COMPRESSION = 37;	/* level 1-9 (int) */
  const int SSH_CMSG_MAX_PACKET_SIZE = 38;	/* size 4k-1024k (int) */
  const int SSH_CMSG_AUTH_TIS = 39;	/* we use this for s/key */
  const int SSH_SMSG_AUTH_TIS_CHALLENGE = 40;	/* challenge (string) */
  const int SSH_CMSG_AUTH_TIS_RESPONSE = 41;	/* response (string) */
  const int SSH_CMSG_AUTH_KERBEROS = 42;/* (KTEXT) */
  const int SSH_SMSG_AUTH_KERBEROS_RESPONSE = 43;/* (KTEXT) */
  const int SSH_CMSG_HAVE_KERBEROS_TGT = 44;	/* credentials (s) */
  const int SSH_CMSG_HAVE_AFS_TOKEN = 65;/* token (s) */
  const int SSH_CIPHER_NONE = 77;







  //----------------------------------------

  /* Fatal messages.  This function never returns. */

  /*
void
fatal(const char *fmt,...)
{
  exit(1);
}
  */
  void packet_disconnect(params string[] args)
  {
    // printf("Packet disconnected\n");
  }

  object xmalloc(int size)
  {
    Contract.Requires(size != 0);
    object ptr;
    /*
    if (size == 0)
      fatal("xmalloc: zero size");
     * */


    ptr = new object();
    /*
      ptr = malloc(size);
      if (ptr == NULL)
        fatal("xmalloc: out of memory (allocating %lu bytes)", (u_long) size);
     */
    return ptr;
  }

  static char[] xstrdup(char[] s)
  {
    /*  register int len = strlen (s) + 1;
      register char *ret = xmalloc (len); memcpy (ret, s, len);
      return ret;
     */

    return s;
  }

  static int errno;
  /* import */

  /*
  * General data structure for command line options and options configurable
  * in configuration files. No assumtions can be made about any value in options
  */
  static public Options options;

  static void
  xfree(object ptr)
  {
    Contract.Requires(ptr != null);

    /*  if (ptr == NULL)
      fatal("xfree: NULL pointer given as argument");
    free(ptr);
   */
  }


  /* 
  * Returns 4 bytes from the current network packet received from the host as integer
  * and advances the cursor. No assumtions can be made about the value received.
  */
  extern static int packet_read();

  /*
  * The  getenv()  function  searches  the  environment  list to find the environment variable name, 
  * and returns a pointer to the corresponding value string or NULL if there is no match. No
  * assumptions can be made on the value of environment variables.
  */
  static extern char[] getenv(string name);


  static char[] read_response(char[] prompt, int flags)
  {
    Contract.Ensures(Contract.Result<char[]>() != null);

    char[] askpass = null, ret = null;
    char[] buf = new char[1024];

    int rppflags, use_askpass = 0, ttyfd;

    rppflags = (flags & RP_ECHO) != 0 ? RPP_ECHO_ON : RPP_ECHO_OFF;
    if ((flags & RP_USE_ASKPASS) != 0)
      use_askpass = 1;
    else if ((flags & RP_ALLOW_STDIN) != 0)
    {
      if (!isatty(STDIN_FILENO))
      {
        //debug("read_response: stdin is not a tty");
        use_askpass = 1;
      }
    }
    else
    {
      rppflags |= RPP_REQUIRE_TTY;
      ttyfd = open(_PATH_TTY);
      if (ttyfd >= 0)
        close(ttyfd);
      else
      {
        //debug("read_response: can't open %s: %s", _PATH_TTY,
        strerror(errno);
        use_askpass = 1;
      }
    }

    if ((flags & RP_USE_ASKPASS) != 0 || (ret = getenv("DISPLAY")) != null)
      goto end;

    if (use_askpass != 0 && getenv("DISPLAY") != null)
    {
      if (getenv(SSH_ASKPASS_ENV) != null)
        askpass = getenv(SSH_ASKPASS_ENV);
      else
        askpass = _PATH_SSH_ASKPASS_DEFAULT.ToCharArray();
      if ((ret = ssh_askpass(askpass, prompt)) == null)
        if ((flags & RP_ALLOW_EOF) == 0)
          return xstrdup("".ToCharArray());
      goto end;
    }


    ret = xstrdup(buf);
    memset(buf, 'x', buf.Length);
  end:
    return ret;
  }

  [ContractVerification(false)]
  [Pure]
  static private void memset(char[] buf, char p, int p_2)
  {
    throw new System.NotImplementedException();
  }

  [ContractVerification(false)]
  [Pure]
  static private char[] ssh_askpass(char[] askpass, char[] prompt)
  {
    throw new System.NotImplementedException();
  }

  [ContractVerification(false)]
  [Pure]
  static private void strerror(int errno)
  {
    throw new System.NotImplementedException();
  }

  [ContractVerification(false)]
  [Pure]
  static private void close(int ttyfd)
  {
    throw new System.NotImplementedException();
  }

  [ContractVerification(false)]
  static private int open(string _PATH_TTY)
  {
    throw new System.NotImplementedException();
  }

  [ContractVerification(false)]
  static private bool isatty(int STDIN_FILENO)
  {
    throw new System.NotImplementedException();
  }



  static int try_challenge_response_authentication()
  {
    int type, i;
    uint clen;
    char[] prompt = new char[1024];
    char[] challenge = null, response = null;

    //debug("Doing challenge response authentication.");
    i = 0;
    do
    {
      i++;
      /* request a challenge */
      packet_start(SSH_CMSG_AUTH_TIS);
      packet_send();
      packet_write_wait();

      type = packet_read();
      if (type != SSH_SMSG_FAILURE &&
        type != SSH_SMSG_AUTH_TIS_CHALLENGE)
      {
        packet_disconnect("Protocol error: got %d in response to SSH_CMSG_AUTH_TIS", type);
      }
      if (type != SSH_SMSG_AUTH_TIS_CHALLENGE)
      {
        //debug("No challenge.");
      }
      challenge = packet_get_string(out clen);
      packet_check_eom();
      snprintf(prompt, prompt.Length, "%s%s", challenge, strchr(challenge, '\n') ? "" : "\nResponse: ");

      xfree(challenge);
      if (i != 0)
      {
        Contract.Assert(false);
      }

      //error("Permission denied, please try again.");
      if (options.cipher == SSH_CIPHER_NONE)
      {
        //logit("WARNING: Encryption is disabled! Response will be transmitted in clear text.");
      }
        response = read_response(prompt, 0);

      //ASSERTION HERE
      //(response != 0);
      Contract.Assert(response != null);

      /*
      //REAL ERROR
    
      Q1: Is the return value of function getenv() always != 0 ?
      Yes-> VALIDATED
      No-> ABORT
    

      */

      if (strcmp(response, "") == 0)
      {
        return 0;
      }
      packet_start(SSH_CMSG_AUTH_TIS_RESPONSE);

      packet_send();
      packet_write_wait();
      type = packet_read();
      if (type == SSH_SMSG_SUCCESS)
        return 1;
      if (type != SSH_SMSG_FAILURE)
        packet_disconnect("Protocol error: got %d in response to SSH_CMSG_AUTH_TIS_RESPONSE", type);
    }
    while (i < options.number_of_password_prompts);
    /* failure */
    return 0;
  }

  [ContractVerification(false)]
  [Pure]
  private static int strcmp(char[] response, string p)
  {
    throw new System.NotImplementedException();
  }

  [ContractVerification(false)]
  [Pure]
  private static void snprintf(char[] prompt, int p, string p_2, char[] challenge, string p_3)
  {
    throw new System.NotImplementedException();
  }

  [ContractVerification(false)]
  [Pure]
  private static bool strchr(char[] challenge, char p)
  {
    throw new System.NotImplementedException();
  }

  [ContractVerification(false)]
  [Pure]
  private static void packet_check_eom()
  {
    throw new System.NotImplementedException();
  }

  [ContractVerification(false)]
  [Pure]
  private static char[] packet_get_string(out uint clen)
  {
    throw new System.NotImplementedException();
  }

  [ContractVerification(false)]
  [Pure]
  private static void packet_disconnect(string p, int type)
  {
    throw new System.NotImplementedException();
  }

  [ContractVerification(false)]
  [Pure]
  private static void packet_write_wait()
  {
    throw new System.NotImplementedException();
  }

  [ContractVerification(false)]
  [Pure]
  private static void packet_send()
  {
    throw new System.NotImplementedException();
  }

  [ContractVerification(false)]
  [Pure]
  private static void packet_start(int SSH_CMSG_AUTH_TIS)
  {
    throw new System.NotImplementedException();
  }
}
#endif