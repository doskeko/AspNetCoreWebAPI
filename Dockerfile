FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 5000
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["SmartSchool.WebAPI/SmartSchool.WebAPI.csproj", "SmartSchool.WebAPI/"]
RUN dotnet restore "SmartSchool.WebAPI/SmartSchool.WebAPI.csproj"
COPY . .
WORKDIR "/src/SmartSchool.WebAPI"
RUN dotnet build "SmartSchool.WebAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SmartSchool.WebAPI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SmartSchool.WebAPI.dll"]
