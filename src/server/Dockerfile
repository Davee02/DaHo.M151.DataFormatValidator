FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-bionic AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-bionic AS build
COPY ["./DaHo.M151.DataFormatValidator.csproj", "src/DaHo.M151.DataFormatValidator/"]
WORKDIR "/src/DaHo.M151.DataFormatValidator"
RUN dotnet restore "DaHo.M151.DataFormatValidator.csproj"
COPY . .
RUN dotnet build "DaHo.M151.DataFormatValidator.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "DaHo.M151.DataFormatValidator.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "DaHo.M151.DataFormatValidator.dll"]