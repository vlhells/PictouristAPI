FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Pictourist/PictouristAPI.csproj", "Pictourist/"]
RUN dotnet restore "Pictourist/PictouristAPI.csproj"
COPY . .
WORKDIR "/src/Pictourist"
RUN dotnet build "PictouristAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "PictouristAPI.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PictouristAPI.dll"]
