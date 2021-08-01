#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["Summary.API/Summary.API.csproj", "Summary.API/"]
RUN dotnet restore "Summary.API/Summary.API.csproj"
COPY . .
WORKDIR "/src/Summary.API"
RUN dotnet build "Summary.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Summary.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Summary.API.dll"]