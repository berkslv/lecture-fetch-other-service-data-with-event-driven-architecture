FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["src/Organizations/Organizations.API/Organizations.API.csproj", "src/Organizations/Organizations.API/"]
RUN dotnet restore "src/Organizations/Organizations.API/Organizations.API.csproj"
COPY . .
WORKDIR "/src/src/Organizations/Organizations.API"
RUN dotnet build "Organizations.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Organizations.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Organizations.API.dll"]
