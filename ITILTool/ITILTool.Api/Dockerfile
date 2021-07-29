#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["ITILTool.Api/ITILTool.Api.csproj", "ITILTool.Api/"]
RUN dotnet restore "ITILTool.Api/ITILTool.Api.csproj"
COPY . .
WORKDIR "/src/ITILTool.Api"
RUN dotnet build "ITILTool.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ITILTool.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ITILTool.Api.dll"]