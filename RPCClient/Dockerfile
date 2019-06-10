FROM microsoft/dotnet:2.2-runtime AS base
WORKDIR /app

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY RPCClient/RPCClient.csproj RPCClient/
RUN dotnet restore RPCClient/RPCClient.csproj
COPY . .
WORKDIR /src/RPCClient
RUN dotnet build RPCClient.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish RPCClient.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "RPCClient.dll"]
