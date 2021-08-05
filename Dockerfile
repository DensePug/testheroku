FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["Summary.API/Summary.API.csproj", "Summary.API/"]
RUN dotnet restore "Summary.API/Summary.API.csproj"
COPY ./Summary.API ./Summary.API
WORKDIR "/src/Summary.API"
RUN dotnet build "Summary.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Summary.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

ENV ASPNETCORE_URLS http://*:$PORT
ENTRYPOINT ["dotnet", "Summary.API.dll"]