# https://hub.docker.com/_/microsoft-dotnet
FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build

# Unsure if I actually need to add a user but we'll do it for now
RUN useradd -m -s $(which bash) developer

RUN mkdir /source_code

RUN chown developer:developer /source_code

USER developer
