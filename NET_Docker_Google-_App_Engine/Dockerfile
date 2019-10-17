FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY NET_Docker_Google-_App_Engine/NET_Docker_Google-_App_Engine.csproj NET_Docker_Google-_App_Engine/
RUN dotnet restore "NET_Docker_Google-_App_Engine/NET_Docker_Google-_App_Engine.csproj"
COPY . .
WORKDIR "/src/NET_Docker_Google-_App_Engine"
RUN dotnet build "NET_Docker_Google-_App_Engine.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "NET_Docker_Google-_App_Engine.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "NET_Docker_Google-_App_Engine.dll"]
