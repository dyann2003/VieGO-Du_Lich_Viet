# Giai đoạn build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

WORKDIR /app

# Copy file solution
COPY EXE201_VieGo.sln ./

# Copy từng project .csproj
COPY VieGo/VieGo.csproj ./VieGo/
COPY Model/Model.csproj ./Model/
COPY Data/Data.csproj ./Data/
COPY Business/Business.csproj ./Business/

RUN dotnet restore EXE201_VieGo.sln

# Copy toàn bộ source code
COPY . .

RUN dotnet build EXE201_VieGo.sln -c Release -o /app/build

RUN dotnet publish VieGo/VieGo.csproj -c Release -o /app/publish

# Giai đoạn runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final

WORKDIR /app

COPY --from=build /app/publish .

EXPOSE 80

ENTRYPOINT ["dotnet", "VieGo.dll"]
