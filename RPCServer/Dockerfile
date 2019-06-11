FROM microsoft/dotnet:2.2-runtime AS base
WORKDIR /app

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY ../RPCServer/RPCServer.csproj ../RPCServer/
RUN dotnet restore ../RPCServer/RPCServer.csproj
COPY . .
WORKDIR /src/../RPCServer
RUN dotnet build RPCServer.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish RPCServer.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "RPCServer.dll"]
