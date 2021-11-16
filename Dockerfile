# https://hub.docker.com/_/microsoft-dotnet
FROM mcr.microsoft.com/dotnet/sdk:5.0 AS s3g-test

WORKDIR /source

COPY *.sln .
COPY ./StaticSiteGenerator/*.csproj ./StaticSiteGenerator/
COPY ./StaticSiteGenerator.UnitTests/*.csproj ./StaticSiteGenerator.UnitTests/
COPY ./StaticSiteGenerator.IntegrationTests/*.csproj ./StaticSiteGenerator.IntegrationTests/

RUN dotnet restore

COPY . ./

RUN dotnet test
