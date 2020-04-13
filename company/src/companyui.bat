@echo off
cd E:/work/program/company/src/Company.UI
echo "dotnet clean"
dotnet clean
echo "dotnet clean finsh"
echo "dotnet build"
dotnet build
echo "dotnet build finsh"
echo "dotnet publish"
dotnet publish -o "e:\work\iis\companyui"
echo "dotnet publish finsh"