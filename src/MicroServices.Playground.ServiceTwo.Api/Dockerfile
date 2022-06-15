#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/MicroServices.Playground.ServiceOne.Api/MicroServices.Playground.ServiceOne.Api.csproj", "src/MicroServices.Playground.ServiceOne.Api/"]
RUN dotnet restore "src/MicroServices.Playground.ServiceOne.Api/MicroServices.Playground.ServiceOne.Api.csproj"
COPY . .
WORKDIR "/src/src/MicroServices.Playground.ServiceOne.Api"
RUN dotnet build "MicroServices.Playground.ServiceOne.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MicroServices.Playground.ServiceOne.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MicroServices.Playground.ServiceOne.Api.dll"]