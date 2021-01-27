FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["MyCompany.Services.SurveyAPI.csproj", "./"]
RUN dotnet restore "MyCompany.Services.SurveyAPI.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "MyCompany.Services.SurveyAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MyCompany.Services.SurveyAPI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
#ENTRYPOINT ["dotnet", "MyCompany.Services.SurveyAPI.dll"]
CMD ASPNETCORE_URLS=http://*:$PORT dotnet MyCompany.Services.SurveyAPI.dll