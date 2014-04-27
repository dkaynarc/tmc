@echo off

set WebServiceDir="WebService"

call "C:\Program Files (x86)\Microsoft Visual Studio 12.0\VC\vcvarsall.bat" x86_amd64
svcutil.exe /language:cs /out:%WebServiceDir%\TmcWcfProxy.cs /config:%WebServiceDir%\App.config http://localhost:8000/TMC/
pause