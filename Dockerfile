# https://hub.docker.com/_/microsoft-dotnet
FROM mcr.microsoft.com/dotnet/sdk:5.0 AS s3g-test

WORKDIR /source
COPY . ./

RUN dotnet test

RUN dotnet publish -c release -o /app

# TODO: This is currently broken
# It doesn't have the input files or a location for the output in the image
FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS s3g
WORKDIR /app
COPY --from=s3g-test /app ./

ENTRYPOINT ["dotnet", "StaticSiteGenerator.dll"]
