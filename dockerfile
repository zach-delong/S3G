FROM mcr.microsoft.com/dotnet/sdk:6.0 as build-img
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
RUN dotnet test

# Publish dlls for consumption later
WORKDIR /source/StaticSiteGenerator
RUN dotnet publish -c release -o /output --no-restore

# Create distribution image
FROM mcr.microsoft.com/dotnet/runtime:6.0
WORKDIR /s3g
COPY --from=build-img /output ./

WORKDIR /input
