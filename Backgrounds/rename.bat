@echo off
setlocal enabledelayedexpansion

set /p baseName=Enter base name for files: 

cd %~dp0

set count=1
for %%f in (*.png) do (
    set newName=!baseName!!count!.png
    ren "%%f" "!newName!"
    set /a count+=1
)

echo Renaming completed.

endlocal