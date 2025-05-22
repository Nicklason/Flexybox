# Flexybox

This repository contains my solution to the developer task for the Flexybox.com Dotnet Developer position.

## Prerequisites

- Docker
- .NET 9 SDK

## Running the app

1. Create copies of the environment files and remove "template" from their names
    - `.env.app` (used by the app)
    - `.env.mysql` (used by docker compose to configure MySQL database)
2. Start a MySQL database using `docker-compose up -d` (or create your own).
3. Start the app using `dotnet run`.
