# Use the official .NET SDK image
FROM mcr.microsoft.com/dotnet/sdk:9.0

# Set the working directory
WORKDIR /src
# Copy the project files to the container
COPY . /src
# Restore dependencies
RUN dotnet restore

# Build the application
RUN dotnet build -c Release -o out

# Set the entry point for the container
ENTRYPOINT ["dotnet", "out/MonopolyKata.dll"]
