# https://hub.docker.com/_/microsoft-dotnet
ARG VARIANT=7.0.403-bookworm-slim-amd64
FROM mcr.microsoft.com/dotnet/sdk:${VARIANT} AS build
WORKDIR /source

COPY Api/*.csproj Api/
COPY Infraestructura/*.csproj Infraestructura/
COPY Core/*.csproj Core/
RUN dotnet restore Api/Api.csproj

COPY Api/ Api/
COPY Infraestructura/ Infraestructura/
COPY Core/ Core/

FROM build AS publish
WORKDIR /source/Api
RUN dotnet publish --no-restore -o /app

FROM mcr.microsoft.com/dotnet/aspnet:7.0.13-bookworm-slim-amd64
WORKDIR /app
COPY --from=publish /app .

ENTRYPOINT ["dotnet"]
CMD ["Api.dll", "--urls", "http://+:5000"]
