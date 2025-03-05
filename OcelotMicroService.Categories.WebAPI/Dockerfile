FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 5002

ENV ASPNETCORE_URLS=http://+:5002

USER app
FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG configuration=Release
WORKDIR /src
COPY ["OcelotMicroService.Categories.WebAPI/OcelotMicroService.Categories.WebAPI.csproj", "OcelotMicroService.Categories.WebAPI/"]
RUN dotnet restore "OcelotMicroService.Categories.WebAPI/OcelotMicroService.Categories.WebAPI.csproj"
COPY . .
WORKDIR "/src/OcelotMicroService.Categories.WebAPI"
RUN dotnet build "OcelotMicroService.Categories.WebAPI.csproj" -c $configuration -o /app/build

FROM build AS publish
ARG configuration=Release
RUN dotnet publish "OcelotMicroService.Categories.WebAPI.csproj" -c $configuration -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "OcelotMicroService.Categories.WebAPI.dll"]
