$numargs = $#ARGV + 1;

($numargs == 1) || die "usage: compareIL.pl <IL file>\n";

$input = $ARGV[0];

open(IN, "<$input") || die "can't open $input"; 

my @array = ();

$inmethod = 0;
while (<IN>) {

# CCI replaces "ldc.i8 x" with "ldc.i4 x; conv.i8" if x fits in 4 bytes
# A little bit tricker, because we need to check the next line after the
# current one to see if it matches the pattern.
# So this rule needs to come first since it may read an extra line and if
# the pattern isn't found, that extra line might match a later pattern
#
# Transforms "ldc.i4 H; conv.i8" ==> "ldc.i8 H" (H is already in hex format)
#
  if (/(^.*)ldc\.i4(\s*)(0x[a-fA-F0-9]{1,4})/){
    $a = $1; $b = $2;$H = $3;
    $a =~  s/IL_[a-fA-F0-9]{4,4}/IL_xxxx/g;  
    $_ = <IN>;
    if (/conv\.i8/){
      print $a,"ldc.i8",$b,$H,"\n";
      next;
    }else{
      print $a,"ldc.i4",$b,$H,"\n"; # print the mistakenly consumed ldc.i4 instruction
      # leave the second line in $_ in case any of the following rules apply to it.      
    }
  }
# There's probably a better way to do this, but also need to look for "ldc.i4.d"
# where d is between 0 and 8 because that also is used.
#
# Transforms "ldc.i4.d; conv.i8" ==> "ldc.i8 0xd" (since d is the same in decimal and hex)
#
  if (/(^.*)ldc\.i4\.([0-9])/){
    $a = $1; $d = $2;
    $a =~  s/IL_[a-fA-F0-9]{4,4}/IL_xxxx/g;  
    $_ = <IN>;
    if (/conv\.i8/){
      print $a,"ldc.i8     0x",$d,"\n";
      next;
    }else{
      print $a,"ldc.i4.",$d,"\n"; # print the mistakenly consumed ldc.i4 instruction
      # leave the second line in $_ in case any of the following rules apply to it.      
    }
  }
# Even uglier, but also need to look for "ldc.i4.s x" where x is a small integer
# that needs to get converted to hex in the conv.i8 form.
# E.g. "ldc.i4.s 26;conv.i8" ==> "ldc.i8     0x1a"
#
# Transforms "ldc.i4.s N; conv.i8" ==> "ldc.i8 0xH" (where H is the hex format of N)
#
  if (/(^.*)ldc\.i4\.s(\s*)([0-9]{1,3})/){
    $a = $1; $b = $2; $N = $3;
    $a =~  s/IL_[a-fA-F0-9]{4,4}/IL_xxxx/g;  
    $_ = <IN>;
    if (/conv\.i8/){
      $H = sprintf("%lx", $N);
      print $a,"ldc.i8     0x",$H,"\n";
      next;
    }else{
      print $a,"ldc.i4.",$b,$N,"\n"; # print the mistakenly consumed ldc.i4 instruction
      # leave the second line in $_ in case any of the following rules apply to it.      
    }
  }
  
# Normalize the order that the getter and setter are declared within a property declaration
# This puts the setter first. No particular reason, but at one point I thought that ildasm
# was dumping it in that order. It turns out that you see both orders in the output from
# ildasm.
  if (/^ *} \/\/ end of property/){
    if ($getter ne ""){
      print $getter; $getter = ""; 
    }
    $in_setter = 0; $in_getter = 0; $in_prop_def = 0; print; next;
  }
  if(/^ *\.property / && $in_prop_def == 0){
    $in_prop_def = 1; $in_getter = 0; $in_setter = 0; $getter = "";
    print; next;
  }
  if ($in_prop_def == 1 && /^ *\.get /){
    $in_getter = 1; $getter = $_; $in_prop_def = 0; next;
  }
  if ($in_getter == 1){
    if (/^ *\.set /){
      $in_setter = 1; $in_getter = 0; print; next;
    }else{
      $getter .= $_; next;
    }
  }

# Normalize the order that the removeon and addon are declared within a event declaration
# This puts the addon first. No particular reason, but at one point I thought that ildasm
# was dumping it in that order. It turns out that you see both orders in the output from
# ildasm.
  if (/^ *} \/\/ end of event/){
    if ($remover ne ""){
      print $remover; $remover = ""; 
    }
    $in_adder = 0; $in_remover = 0; $in_event_def = 0; print; next;
  }
  if(/^ *\.event / && $in_event_def == 0){
    $in_event_def = 1; $in_remover = 0; $in_adder = 0; $remover = "";
    print; next;
  }
  if ($in_event_def == 1 && /^ *\.removeon /){
    $in_remover = 1; $remover = $_; $in_event_def = 0; next;
  }
  if ($in_remover == 1){
    if (/^ *\.addon /){
      $in_adder = 1; $in_remover = 0; print; next;
    }else{
      $remover .= $_; next;
    }
  }
  
# Skip all lines between ".permissionset linkcheck" and "// Code size" because of problems
# # with the way the security attributes are serialized.
#   if (/^ *\.permissionset (linkcheck|assert|demand|inheritcheck)/){
#     print PERMISSIONSET $1,"\n";
#     while (<IN>){
#       last if /^ *\/\/Code size/;
#       last if /^ *} \/\/ end of/;
#       last if /^ *\.override/;
#     }
#   }  
  
# BUGBUG?? Should this be done?
  s/I_[a-fA-F0-9]{8,8}/I_XXXXXXXX/g;  
  
# CCI replaces "ldobj T" instructions with ldind.T' whenever T is a primitive type
# It also replaces all "stobj T" with stind.T'
  s/ldobj\s*.*/LDOBJ/;
  s/ldind\..*/LDOBJ/;
  s/stobj\s*.*/STOBJ/;
  s/stind\..*/STOBJ/;
# That makes the code size smaller, so get rid of code size line and remove all IL addresses
  s/Code size\s*\d+\s*\(0x[a-f0-9]+\)/Code size/;
  s/IL_[a-f0-9][a-f0-9][a-f0-9][a-f0-9]/IL_xxxx/g;

# In mscorlib, short branches are not preserved
  s/(b.*)\.s/\1  /;
  s/leave.s/leave  /;

# get rid of maxstack lines, but this might be a real difference
  if (/\.maxstack/){ next; }
  
  print;

}
