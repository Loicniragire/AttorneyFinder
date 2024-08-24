# Use the official .NET Core SDK image to build the app
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

# Set the working directory inside the container
WORKDIR /app/Attorneys

# Copy the project file and restore the dependencies
COPY src/Attorneys/Attorneys.csproj ./
RUN dotnet restore ./Attorneys.csproj --no-cache

# Copy the rest of the application code
COPY src/Attorneys/. ./

# Build the application (output to /app/build_output)
RUN dotnet build Attorneys.csproj -c Release -o /app/build_output --no-restore

# Publish the application (use the output from build)
RUN dotnet publish Attorneys.csproj -c Release -o /app/publish_output --no-restore

# Use the official .NET Core runtime image to run the app
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime

# Set the working directory inside the container
WORKDIR /app

# Copy the published output from the build stage to the runtime stage
COPY --from=build /app/publish_output .

# Expose the port that your application is listening on
EXPOSE 80

# Specify the entry point for the application
ENTRYPOINT ["dotnet", "Attorneys.dll"]

