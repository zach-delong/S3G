FROM mcr.microsoft.com/dotnet/sdk
WORKDIR /source
COPY StaticSiteGenerator/* StaticSiteGenerator/
COPY StaticSiteGenerator.UnitTests/* StaticSiteGenerator.UnitTests/
COPY StaticSiteGenerator.IntegrationTests/* StaticSiteGenerator.IntegrationTests/
COPY StaticSiteGenerator.sln StaticSiteGenerator.sln
