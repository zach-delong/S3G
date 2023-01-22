FROM mcr.microsoft.com/dotnet/sdk:6.0
WORKDIR /source

# Copy project files and nuget restore to cache with things that don't change often
COPY StaticSiteGenerator.sln StaticSiteGenerator.sln
COPY StaticSiteGenerator/*.csproj StaticSiteGenerator/
COPY StaticSiteGenerator.UnitTests/*.csproj StaticSiteGenerator.UnitTests/
COPY StaticSiteGenerator.IntegrationTests/*.csproj StaticSiteGenerator.IntegrationTests/
RUN dotnet restore

# Copy all the code
COPY StaticSiteGenerator/ StaticSiteGenerator/
COPY StaticSiteGenerator.UnitTests/ StaticSiteGenerator.UnitTests/
COPY StaticSiteGenerator.IntegrationTests/ StaticSiteGenerator.IntegrationTests/

# Build and test
RUN dotnet build
RUN dotnet test
