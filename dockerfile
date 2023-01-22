FROM mcr.microsoft.com/dotnet/sdk:6.0
WORKDIR /source
COPY StaticSiteGenerator/ StaticSiteGenerator/
COPY StaticSiteGenerator.UnitTests/ StaticSiteGenerator.UnitTests/
COPY StaticSiteGenerator.IntegrationTests/ StaticSiteGenerator.IntegrationTests/
COPY StaticSiteGenerator.sln StaticSiteGenerator.sln

RUN dotnet build

RUN dotnet test
