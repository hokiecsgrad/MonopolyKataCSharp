# Use the official .NET SDK image
FROM mcr.microsoft.com/dotnet/sdk:9.0

# Update the package list and install git and ssh
RUN apt-get update && apt-get install -y \
    git \
    openssh-client

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
