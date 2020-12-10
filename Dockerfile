# https://hub.docker.com/_/microsoft-dotnet
FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /source

# copy csproj and restore as distinct layers
COPY *.sln .
COPY src/*.csproj ./src/
RUN dotnet restore

# Copy example stuff
COPY exampleMarkdownDirectory/ ./exampleMarkdownDirectory/

# Copy templates
COPY templates/ ./templates/

# copy the remaining code 
COPY src/ ./src/

# copy tests
COPY test/ ./test

ENTRYPOINT dotnet run --project src && dotnet test ./test
