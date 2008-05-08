@echo off
cls
cd build\scripts
..\..\tools\nant-0.86-beta1\bin\nant.exe -buildfile:default.build %*
cd ..\..