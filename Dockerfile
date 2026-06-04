# Step 1: Build the App using .NET SDK
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy everything from the root directory
COPY . .

# Restore dependencies and publish
RUN dotnet restore
RUN dotnet publish -c Release -o /app/out

# Step 2: Run the App using .NET Runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/out .

# Copy SQLite database file if it exists
COPY *.db ./ 2>/dev/null || :

EXPOSE 8080
ENTRYPOINT ["dotnet", "QuizManagemant.API.dll"]
