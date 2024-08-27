# Attorney Finder

This project is comprised of a set of backend Http APIs and an Angular frontend App. 

## Attorneys API

## AttorneyFinder APP

## Database

## Build

## Execution

** Database migration **
dotnet ef database migration add 'init'

** Database update **
dotnet ef database update


** Run the project **
docker compose up --build -d

** Check docker status **
docker ps -a

** Stop docker **
docker compose down

** Jwt Key generation **
openssl rand -base64 32

