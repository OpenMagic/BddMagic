@echo off

cmd /c Publish.bat

if "%errorlevel%" == "0" goto Finish

echo.
echo.
echo **************************
echo *** An error occurred. ***
echo **************************

:Finish
echo.
echo.
pause
