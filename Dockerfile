# Step 1: Build the App using .NET SDK
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy everything from the root directory
COPY . .

# Solution file publish warning fix karne ke liye csproj ko direct publish karenge
RUN dotnet restore QuizManagemant.API.csproj
RUN dotnet publish QuizManagemant.API.csproj -c Release -o /app/out

# Step 2: Run the App using .NET Runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

# Copy the published files from build step
COPY --from=build /app/out .

# SQLite Database file ko directly container mein copy karne ke liye (taaki table ka error na aaye)
COPY --from=build /src/*.db ./ 2>/dev/null || :
COPY --from=build /src/QuizManagemant.API/*.db ./ 2>/dev/null || :

EXPOSE 8080
ENTRYPOINT ["dotnet", "QuizManagemant.API.dll"]
