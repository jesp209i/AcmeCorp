# Acme Corporation Task

## To run the Database
> Docker Desktop is needed to run the database.

- Use the terminal to navigate to the root of the project.
- Run `dotnet restore`.
- Navigate to the `database` folder 
- run the command: `docker-compose up -d`
  - this might start a download of the needed docker image. 
  - let it download and spin up
- to apply the database migrations run the command `dotnet ef database update -s ..\src\AcmeCorp.WebAPI\ -p ..\src\AcmeCorp.Persistance`