FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 5001

ENV ASPNETCORE_URLS=http://+:5001

USER app
FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG configuration=Release
WORKDIR /src
COPY ["OcelotMicroService.Todos.WebAPI/OcelotMicroService.Todos.WebAPI.csproj", "OcelotMicroService.Todos.WebAPI/"]
RUN dotnet restore "OcelotMicroService.Todos.WebAPI/OcelotMicroService.Todos.WebAPI.csproj"
COPY . .
WORKDIR "/src/OcelotMicroService.Todos.WebAPI"
RUN dotnet build "OcelotMicroService.Todos.WebAPI.csproj" -c $configuration -o /app/build

FROM build AS publish
ARG configuration=Release
RUN dotnet publish "OcelotMicroService.Todos.WebAPI.csproj" -c $configuration -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "OcelotMicroService.Todos.WebAPI.dll"]
