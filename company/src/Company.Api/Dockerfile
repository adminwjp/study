#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.
#
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["Company.Api/Company.Api.csproj", "Company.Api/"]
COPY ["Company.Domain/Company.Domain.csproj", "Company.Domain/"]
COPY ["Infrastructure/Infrastructure.csproj", "Infrastructure/"]
RUN dotnet restore "Company.Api/Company.Api.csproj"
COPY . .
WORKDIR "/src/Company.Api"
RUN dotnet build "Company.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Company.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Company.Api.dll"]

#FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build-env
#WORKDIR /app
## Copy csproj and restore as distinct layers
##WORKDIR /src
#COPY ["Company.Api/Company.Api.csproj", "Company.Api/"]
#COPY ["Company.Domain/Company.Domain.csproj", "Company.Domain/"]
#COPY ["Infrastructure/Infrastructure.csproj", "Infrastructure/"]
##COPY *.csproj ./
##RUN dotnet restore 
#RUN dotnet restore "Company.Api/Company.Api.csproj"
#
## Copy everything else and build
#COPY . ./
#RUN dotnet publish -c Release -o out
#
## Build runtime image
#FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim
#WORKDIR /app
#COPY --from=build-env /app/out .
#ENTRYPOINT ["dotnet", "Company.Api.dll"]