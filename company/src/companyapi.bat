@echo off
cd E:/work/program/company/src/Company.Api
echo "dotnet clean"
dotnet clean
echo "dotnet clean finsh"
echo "dotnet build"
dotnet build
echo "dotnet build finsh"
echo "dotnet publish"
dotnet publish -o "e:\work\iis\companyapi"
echo "dotnet publish finsh"