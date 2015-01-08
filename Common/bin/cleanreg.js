var WshShell = new ActiveXObject("WScript.Shell")

function ExecAndWait(cmd) {
    var oExec = WshShell.Exec(cmd);               
    while (oExec.Status == 0)
    {
       WScript.Sleep(100);
    }
    if (oExec.ExitCode != 0) {
       WScript.echo("Error: " + oExec.ExitCode);
    }
}

var fso = new ActiveXObject("Scripting.FileSystemObject")
var ForReading = 1;
var stm = fso.OpenTextFile("makefile",ForReading);
var makefile = stm.ReadAll();
stm.Close();

var lines = makefile.split("\n");
var target = null;
var commands = new Array();
var comments = new Array();
var count = 0;

for (l = 0; l < lines.length; l++) {
  var line = lines[l];
  try {
    var i = line.indexOf(":");  
    if (i >= 0) {        
      target = line.substr(0,i);
      assem = null;
      assemblyNameLength = target.indexOf(".dll");
      if (assemblyNameLength > -1) {   
        assem = target.substr(0,assemblyNameLength);        
      }
      continue;      
    }
        
    if (line.indexOf("GacInstall")>=0 || line.indexOf("GacRegInstall")>=0) {
      comments[count] = "Ungacing " + assem;
      commands[count++] = "gacutil /silent /nologo /uf "+assem;
    }
    
    if (line.indexOf("RegInstall")>=0 || line.indexOf("GacRegInstall")>=0) {
      comments[count] = "Unregistering " + target;
      commands[count++] = "regasm /nologo /u "+target;
    }
  } catch (e) {
      WScript.echo("error: " + e.description);
  }  
}

// uninstall in reverse order
for (i = count-1; i >= 0; i--) {
   WScript.echo(comments[i]);
   ExecAndWait(commands[i]);
}