# Use the official .NET Core SDK image to build the app
# This will be used to restore, build, and publish the application
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

# Set the working directory inside the container
WORKDIR /app

# Copy the project file and restore the dependencies
COPY *.csproj ./
RUN dotnet restore

# Copy the rest of the application code
COPY . ./

# Build the application
RUN dotnet build Attorneys.csproj -c Release -o out

# Publish the application
RUN dotnet publish Attorneys.csproj -c Release -o out

# Use the official .NET Core runtime image to run the app
# This is a smaller image optimized for running apps
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime

# Set the working directory inside the container
WORKDIR /app

# Copy the published output from the build stage to the runtime stage
COPY --from=build /app/out .

# Expose the port that your application is listening on
EXPOSE 80

# Specify the entry point for the application
ENTRYPOINT ["dotnet", "Attorneys.dll"]

