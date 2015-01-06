@echo Off
set target=%1
if "%target%" == "" (
   set target=Go
)
if "%Configuration%" == "" (
   set Configuration=Debug
)
%WINDIR%\Microsoft.NET\Framework\v4.0.30319\msbuild Build\Build.proj /t:"%target%" /p:Configuration="%Configuration%" /fl /flp:LogFile=msbuild.log;Verbosity=Detailed /nr:false