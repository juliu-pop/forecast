FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["WebAPITestAppl.csproj", "."]
RUN dotnet restore "./WebAPITestAppl.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "WebAPITestAppl.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "WebAPITestAppl.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WebAPITestAppl.dll"]