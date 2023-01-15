#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["News-App-API/News-App-API.csproj", "."]
RUN dotnet restore "./News-App-API.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "News-App-API/News-App-API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "News-App-API/News-App-API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "News-App-API.dll"]