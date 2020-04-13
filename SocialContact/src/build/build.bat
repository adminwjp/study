@echo off

echo "Linux Docker build"

cd E:/work/program/SocialContact/src/SocialContact.Api

dotnet publish -c Release -o E:/work/docker/SocialContact.Api

cd E:/work/docker/SocialContact.Api

echo "publish SocialContact.Api success"

docker build -t aspnetcoredocker .