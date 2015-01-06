#!/usr/local/bin/perl
use Cwd;
##### Global variables #########
my $v1, $v2;
my $pre = 0, $post = 0, $inv = 0, $empty = 0, $out = 0, $outfile = "";
###### Helper procedures 
sub Error{
  my $msg = shift;
  print "ERROR::$msg";
}
sub PrintUsage{
  print "\ndiffClousotContracts v1 v2 [options]\n";
  print "options:\n";
  print "\t -pre       [consider preconditions, default off]\n";
  print "\t -post      [consider postconditions, default off]\n";
  print "\t -inv       [consider invariants, default off]\n";
  print "\t -empty     [report missing procedures, default off]\n";
  print "\t -out:file  [outputs to a fle, default stdout]\n";
  die;
}
# Process command line args
sub ProcessOptions {
  if (not $ARGV[0]) {PrintUsage();}
  $v1 = shift @ARGV;
  $v2 = shift @ARGV;
  if (not (-e $v1 && -e $v2)) {
    Error("$v1 or $v2 does not exists"); 
    PrintUsage();
  }
  while ($ARGV[0]) {
    my $opt = shift @ARGV;
    if ($opt =~ /-pre$/){$pre = 1;}
    elsif ($opt =~ /-post$/){$post = 1;}
    elsif ($opt =~ /-inv$/){$inv = 1;}
    elsif ($opt =~ /-empty$/){$empty = 1;}
    elsif ($opt =~ /-out:/){
      $out = 1; 
      $outfile = $opt; 
      $outfile =~ s/-out://g;
    }
    else {Error("unknow option ".$opt); PrintUsage();}
  }
  if (($pre + $post + $inv) eq 0) {
    Error ("Nothing to diff. Choose one or more from {-pre, -post. -inv}."); 
    PrintUsage();
  }
}
sub PrintContractsPerMethod {
  my $arg = shift;
  my %tmp = %$arg;
  while (($key,$value) = each(%tmp)) {
    print "Method = $key, Contracts = $value\n";
  }	
}
sub GetContracts{
  my $v = shift;
  my $tmp  = shift;
  my %contracts = %$tmp;
  open (V,"<$v") or die "Can't open $v\n";  
  # Each vi looks as
  # 
  # Prelude 
  # ...
  # Method <k> : <method(args)>
  # [<method(args)>[0x<m>]: message : Suggested <ContractType>: <ContractString>] //optional
  # ...
  #
  # # [<method(args)>[0x<m>]: message : warning : <ContractString>] //optional  
  while (<V>) {
    next unless ($_ =~ /Suggested/);
    chomp $_;
    my $method = $_;
    $method =~ s/\[0x.*//g;
    my $contract = $_;
    $contract =~ s/.*Suggested//g;
    next if (($contract =~ /requires/) && ($pre eq 0));
    next if (($contract =~ /ensures/) && ($post eq 0));
    next if (($contract =~ /invariant/) && ($inv eq 0));
    if (exists $contracts{$method}) {
      $contract = $contracts{$method} . "\$\$\$" . $contract;
    }	
    $contracts{$method} = $contract;
  }	
  close V;
  %contracts;
}
sub SortContracts{
  my $arg = shift;
  my %tmp = %$arg;
  while (($key,$value) = each(%tmp)) {
    my $a = SortMethodContracts($key, $value);
    delete $tmp{$key};
    $tmp{$key} = $a;
  }	  
  %tmp;
}
sub SortMethodContracts{
  #sort the strings separated by "\t"
  my $b = shift;
  my $c = shift;
  my @d = split(/\$\$\$/, $c);
  my @e = sort @d;
  my $ret = "";
  foreach my $f (@e){
    if ($ret eq "") {$ret = $f;} 
    else {$ret = $ret . "\$\$\$" . $f ;}
  }	
  if (not ($ret eq $c)) {
#    print "Method = $b\n Original = $c\n Sorted   = $ret\n ";
  }
  chomp($ret);
  $ret;
}

####### Main starts here ############

ProcessOptions();
# read v1 and v2 log files
my %contracts1 = (), %contracts2 = ();
%contracts1 = GetContracts($v1, \%contracts1);
%contracts2 = GetContracts($v2, \%contracts2);
%contracts1 =  SortContracts(\%contracts1);
%contracts2 =  SortContracts(\%contracts2);
#PrintContractsPerMethod(\%contracts1);
#PrintContractsPerMethod(\%contracts2);
while(($m,$c) = each(%contracts1)){
  if (exists $contracts2{$m}) {
    $d = $contracts2{$m};
    if (not ($c eq $d)) {
      print "\nMISMATCH1 :: $m \n \t $c \n \t $d \n";
    } 
  } elsif ($empty eq 1) {
    print "\nMISMATCH2 :: $m \n \t $c \n \t EMPTY \n";
  }
}
