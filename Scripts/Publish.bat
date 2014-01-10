@echo off

echo.
echo Checking out master...
echo.
git checkout master
if not "%errorlevel%" == "0" exit %errorlevel%

echo.
echo Merging develop into master...
echo.
git merge test
if not "%errorlevel%" == "0" exit %errorlevel%

echo.
echo Pushing master...
echo.
git push
if not "%errorlevel%" == "0" exit %errorlevel%

echo.
echo Switching back to develop...
echo.
git checkout develop
exit %errorlevel%