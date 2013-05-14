C:\WINDOWS\Microsoft.NET\Framework\v4.0.30319\msbuild.exe build.proj
@IF %ERRORLEVEL% NEQ 0 GOTO err
@exit /B 0
:err
@PAUSE
@exit /B 1