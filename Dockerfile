# Giai đoạn build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

WORKDIR /app

# Copy solution file
COPY EXE201_VieGo.sln ./

# Copy các project .csproj
COPY VieGo/VieGo.csproj ./VieGo/
COPY Model/Model.csproj ./Model/
COPY Data/Data.csproj ./Data/
COPY Business/Business.csproj ./Business/

# Restore các package
RUN dotnet restore EXE201_VieGo.sln

# Copy toàn bộ source code
COPY . .

# Build project ở chế độ Release
RUN dotnet build EXE201_VieGo.sln -c Release -o /app/build

# Publish dự án VieGo
RUN dotnet publish VieGo/VieGo.csproj -c Release -o /app/publish

# Giai đoạn runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final

WORKDIR /app

# Copy các file đã publish từ giai đoạn build
COPY --from=build /app/publish .

# Expose cổng 80 cho HTTP
EXPOSE 80

# Cấu hình ASP.NET Core lắng nghe tại địa chỉ 0.0.0.0 (cần thiết để health check thành công)
ENV ASPNETCORE_URLS=http://+:80

# Lệnh chạy ứng dụng
ENTRYPOINT ["dotnet", "VieGo.dll"]
