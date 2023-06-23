FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Financial_App/Financial_App.csproj", "Financial_App/"]
RUN dotnet restore "Financial_App/Financial_App.csproj"
COPY . .
WORKDIR "/src/Financial_App"
RUN dotnet build "Financial_App.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Financial_App.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Financial_App.dll"]