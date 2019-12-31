# NuGet restore
FROM mcr.microsoft.com/dotnet/core/sdk:3.0 AS build
WORKDIR /src
COPY *.sln .
COPY SwamiAPI/*.csproj SwamiAPI/
RUN dotnet restore
COPY . .


# publish
FROM build AS publish
WORKDIR /src/SwamiAPI
RUN dotnet publish -c Release -o /src/publish

FROM mcr.microsoft.com/dotnet/core/aspnet:3.0 AS runtime
WORKDIR /app
COPY --from=publish /src/publish .
# ENTRYPOINT ["dotnet", "SwamiAPI.dll"]
# heroku uses the following
CMD ASPNETCORE_URLS=http://*:$PORT dotnet SwamiAPI.dll