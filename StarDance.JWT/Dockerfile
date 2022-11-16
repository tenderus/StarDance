FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["StarDance.JWT/StarDance.JWT.csproj", "StarDance.JWT/"]
RUN dotnet restore "StarDance.JWT/StarDance.JWT.csproj"
COPY . .
WORKDIR "/src/StarDance.JWT"
RUN dotnet build "StarDance.JWT.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "StarDance.JWT.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "StarDance.JWT.dll"]
