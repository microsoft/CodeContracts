rem To create an installer (which automatically puts it on Codebox/Contracts as well):
rem ==============================================================
cd ManagedContract.Setup
msbuild /p:CCNetLabel=1.0.1020.1 buildMSI.xml /p:ReleaseConfig=debug /t:ILMergeFoxtrot

