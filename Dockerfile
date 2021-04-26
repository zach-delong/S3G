# https://hub.docker.com/_/microsoft-dotnet
FROM mcr.microsoft.com/dotnet/sdk:5.0 AS s3g-test
RUN useradd -m -s $(which bash) developer
USER developer
RUN chown developer:developer /home/developer

WORKDIR /home/developer/source_code
COPY . ./

RUN dotnet test

RUN dotnet publish -c relase -o /app 

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS s3g
WORKDIR /app
COPY --from=s3g-test /home/developer/developer/source_code ./

ENTRYPOINT ["dotnet", "StaticSiteGenerator.dll"]
