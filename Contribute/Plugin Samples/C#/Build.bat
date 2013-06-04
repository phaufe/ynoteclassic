@echo off
rem Build File
rem Requires MSBUILD .NET 3.5
%windir%\Microsoft.NET\Framework\v3.5\MSBuild.exe YnotePluginSamples.sln /p:Configuration=Release /t:Clean,Build
pause