rem %1 is cci2 root dir %2 is configuration

set config=%1
set root=%2

if "%1" == "" set config=Release
if "%2" == "" set root=..\..\..\cci2

sd revert ...
del /s /f /q *.dll *.exe *.pdb *.xml *.exe.config *~
rem copy  /y %1\Samples\asmmeta\bin\release\* .
rem copy  /y %1\Samples\ccdoc\bin\release\* .

copy /y %root%\Samples\AsmMeta\bin\%config%\asmmeta.exe .
copy /y %root%\Samples\AsmMeta\bin\%config%\asmmeta.pdb .
copy /y %root%\Samples\AsmMeta\bin\%config%\asmmeta.exe.config .

copy /y %root%\Samples\CCDoc\bin\%config%\CCDoc.exe .
copy /y %root%\Samples\CCDoc\bin\%config%\CCDoc.pdb .
copy /y %root%\Samples\CCDoc\bin\%config%\CCDoc.exe.config .

copy /y %root%\Converters\NewILToCodeModel\bin\%config%\Microsoft.Cci.NewILToCodeModel.dll
copy /y %root%\Converters\NewILToCodeModel\bin\%config%\Microsoft.Cci.NewILToCodeModel.pdb

copy /y %root%\CoreObjectModel\CodeModel\bin\%config%\Microsoft.Cci.CodeModel.dll .
copy /y %root%\CoreObjectModel\CodeModel\bin\%config%\Microsoft.Cci.CodeModel.pdb .
copy /y %root%\CoreObjectModel\CodeModel\bin\%config%\Microsoft.Cci.CodeModel.xml .

copy /y %root%\Converters\CodeModelToIL\bin\%config%\Microsoft.Cci.CodeModelToIL.dll .
copy /y %root%\Converters\CodeModelToIL\bin\%config%\Microsoft.Cci.CodeModelToIL.pdb .

copy /y %root%\Converters\ContractExtractor\bin\%config%\Microsoft.Cci.ContractExtractor.dll .
copy /y %root%\Converters\ContractExtractor\bin\%config%\Microsoft.Cci.ContractExtractor.pdb .
copy /y %root%\Converters\ContractExtractor\bin\%config%\Microsoft.Cci.ContractExtractor.xml .

copy /y %root%\CodeGenerators\ControlAndDataFlowGraph\bin\%config%\Microsoft.Cci.Analysis.ControlAndDataFlowGraph.dll .
copy /y %root%\CodeGenerators\ControlAndDataFlowGraph\bin\%config%\Microsoft.Cci.Analysis.ControlAndDataFlowGraph.pdb .

copy /y %root%\Converters\ILGenerator\bin\%config%\Microsoft.Cci.ILGenerator.dll .
copy /y %root%\Converters\ILGenerator\bin\%config%\Microsoft.Cci.ILGenerator.pdb .

copy /y %root%\CoreObjectModel\MetadataHelper\bin\%config%\Microsoft.Cci.MetadataHelper.dll .
copy /y %root%\CoreObjectModel\MetadataHelper\bin\%config%\Microsoft.Cci.MetadataHelper.pdb .
copy /y %root%\CoreObjectModel\MetadataHelper\bin\%config%\Microsoft.Cci.MetadataHelper.xml .

copy /y %root%\CoreObjectModel\MetadataModel\bin\%config%\Microsoft.Cci.MetadataModel.dll .
copy /y %root%\CoreObjectModel\MetadataModel\bin\%config%\Microsoft.Cci.MetadataModel.pdb .
copy /y %root%\CoreObjectModel\MetadataModel\bin\%config%\Microsoft.Cci.MetadataModel.xml .

copy /y %root%\CoreObjectModel\MutableMetadataModel\bin\%config%\Microsoft.Cci.MutableMetadataModel.dll .
copy /y %root%\CoreObjectModel\MutableMetadataModel\bin\%config%\Microsoft.Cci.MutableMetadataModel.pdb .
copy /y %root%\CoreObjectModel\MutableMetadataModel\bin\%config%\Microsoft.Cci.MutableMetadataModel.xml .

copy /y %root%\CoreObjectModel\MutableCodeModel\bin\%config%\Microsoft.Cci.MutableCodeModel.dll .
copy /y %root%\CoreObjectModel\MutableCodeModel\bin\%config%\Microsoft.Cci.MutableCodeModel.pdb .
copy /y %root%\CoreObjectModel\MutableCodeModel\bin\%config%\Microsoft.Cci.MutableCodeModel.xml .

copy /y %root%\PEReaderAndWriter\PEReader\bin\%config%\Microsoft.Cci.PEReader.dll .
copy /y %root%\PEReaderAndWriter\PEReader\bin\%config%\Microsoft.Cci.PEReader.pdb .
copy /y %root%\PEReaderAndWriter\PEReader\bin\%config%\Microsoft.Cci.PEReader.xml .

copy /y %root%\PEReaderAndWriter\PEWriter\bin\%config%\Microsoft.Cci.PeWriter.dll .
copy /y %root%\PEReaderAndWriter\PEWriter\bin\%config%\Microsoft.Cci.PeWriter.pdb .
copy /y %root%\PEReaderAndWriter\PEWriter\bin\%config%\Microsoft.Cci.PeWriter.xml .

copy /y %root%\PDBReaderAndWriter\PdbReader\bin\%config%\Microsoft.Cci.PdbReader.dll .
copy /y %root%\PDBReaderAndWriter\PdbReader\bin\%config%\Microsoft.Cci.PdbReader.pdb .
copy /y %root%\PDBReaderAndWriter\PdbReader\bin\%config%\Microsoft.Cci.PdbReader.xml .

copy /y %root%\PDBReaderAndWriter\PdbWriter\bin\%config%\Microsoft.Cci.PdbWriter.dll .
copy /y %root%\PDBReaderAndWriter\PdbWriter\bin\%config%\Microsoft.Cci.PdbWriter.pdb .
copy /y %root%\PDBReaderAndWriter\PdbWriter\bin\%config%\Microsoft.Cci.PdbWriter.xml .

copy /y %root%\CoreObjectModel\SourceModel\bin\%config%\Microsoft.Cci.SourceModel.dll .
copy /y %root%\CoreObjectModel\SourceModel\bin\%config%\Microsoft.Cci.SourceModel.pdb .

rem copy /y %root%\Converters\RoslynToCodeModel\bin\%config%\RoslynToCodeModel.exe .
rem copy /y %root%\Converters\RoslynToCodeModel\bin\%config%\RoslynToCodeModel.pdb .

copy /y %root%\SourceEmitters\CSharpSourceEmitter\bin\%config%\Microsoft.Cci.CSharpSourceEmitter.dll .
copy /y %root%\SourceEmitters\CSharpSourceEmitter\bin\%config%\Microsoft.Cci.CSharpSourceEmitter.pdb .

copy /y %root%\SourceEmitters\VB\VBSourceEmitter\bin\%config%\Microsoft.Cci.VBSourceEmitter.dll .
copy /y %root%\SourceEmitters\VB\VBSourceEmitter\bin\%config%\Microsoft.Cci.VBSourceEmitter.pdb .

copy /y %root%\CoreObjectModel\AstsProjectedAsCodeModel\bin\%config%\Microsoft.Cci.AstsProjectedAsCodeModel.dll .
copy /y %root%\CoreObjectModel\AstsProjectedAsCodeModel\bin\%config%\Microsoft.Cci.AstsProjectedAsCodeModel.pdb .

mkdir CodeContracts
copy  /y %root%\Converters\NewILToCodeModel\bin\debug\CodeContracts\* CodeContracts
copy  /y %root%\CoreObjectModel\CodeModel\bin\debug\CodeContracts\* CodeContracts
copy  /y %root%\Converters\CodeModelToIL\bin\debug\CodeContracts\* CodeContracts
copy  /y %root%\Converters\ContractExtractor\bin\debug\CodeContracts\* CodeContracts
copy  /y %root%\CodeGenerators\ControlAndDataFlowGraph\bin\debug\CodeContracts\* CodeContracts
copy  /y %root%\Converters\ILGenerator\bin\debug\CodeContracts\* CodeContracts
copy  /y %root%\CoreObjectModel\MetadataHelper\bin\debug\CodeContracts\* CodeContracts
copy  /y %root%\CoreObjectModel\MetadataModel\bin\debug\CodeContracts\* CodeContracts
copy  /y %root%\CoreObjectModel\MutableMetadataModel\bin\debug\CodeContracts\* CodeContracts
copy  /y %root%\CoreObjectModel\MutableCodeModel\bin\debug\CodeContracts\* CodeContracts
copy  /y %root%\PEReaderAndWriter\PEReader\bin\debug\CodeContracts\* CodeContracts
copy  /y %root%\PEReaderAndWriter\PEWriter\bin\debug\CodeContracts\* CodeContracts
copy  /y %root%\PDBReaderAndWriter\PdbReader\bin\debug\CodeContracts\* CodeContracts
copy  /y %root%\PDBReaderAndWriter\PdbWriter\bin\debug\CodeContracts\* CodeContracts
copy  /y %root%\CoreObjectModel\SourceModel\bin\debug\CodeContracts\* CodeContracts
copy  /y %root%\SourceEmitters\CSharpSourceEmitter\bin\debug\CodeContracts\* CodeContracts
copy  /y %root%\SourceEmitters\VB\VBSourceEmitter\bin\debug\CodeContracts\* CodeContracts
copy  /y %root%\CoreObjectModel\AstsProjectedAsCodeModel\debug\CodeContracts\* CodeContracts

sd online ...
sd add ...
sd revert -a  ...
