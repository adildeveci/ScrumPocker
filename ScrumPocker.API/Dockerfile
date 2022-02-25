#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /
COPY ["ScrumPocker.API/ScrumPocker.API.csproj", "ScrumPocker.API/"]
COPY ["ScrumPocker.Core/ScrumPocker.Core.csproj", "ScrumPocker.Core/"]
COPY ["ScrumPocker.Services/ScrumPocker.Services.csproj", "ScrumPocker.Services/"]
RUN dotnet restore "ScrumPocker.API/ScrumPocker.API.csproj"
COPY . .
WORKDIR "/src/ScrumPocker.API"
RUN dotnet build "ScrumPocker.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ScrumPocker.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ScrumPocker.API.dll"]