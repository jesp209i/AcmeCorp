# Acme Corporation Task
The following ports needs to be available locally:
- 1433, 5000, 5001, 8080

The following frameworks are needed to run project:
- Docker
- npm
- Dotnet sdk or Visual Studio

## Getting ready to run
- Clone this repository.
- in the project root directory run `dotnet restore`.
- goto the `src/acmecorp` folder and run `npm i` to install js dependencies.
- go back to the project root and into `database/`
- run the command: `docker-compose up -d`
  - this might start a download of the needed docker image. 
  - let it download and spin up.
- to apply the database migrations run the command `dotnet ef database update -s ..\src\AcmeCorp.WebAPI\ -p ..\src\AcmeCorp.Persistence`

## Start the projects
### Backend
In one terminal navigate to the `src/AcmeCorp.WebApi` folder and run `dotnet run`
### Frontend
In another terminal navigate to the `src/acmecorp` folder and run `npm run serve`

- [Navigate to localhost:8080](http://localhost:8080)

## External Identity Provider
When clicking on login you will have the oportunity to create a new user. This is done externally on the Otka service.

If you do NOT want to do this, you can use this fake user:
- username: ummic@umacme.com
- pass: umbraCo123

## Entering the competition
You can start submitting entries on the [competition-page](http://localhost:8080/competition) but you will need product Serial Number.
To get a valid one, you need to log in.

## When logged in
Navigate to the [Products-page](http://localhost:8080/products) to se all the serial numbers. Each number can only be used once.

You can generate entries, by going to the [Fake Contestants-page](http://localhost:8080/fakes). The functionality is made sp you can fill up the entries with fake data to try out the pagination on the [entries-page](http://localhost:8080/Entries). Now you don't have to poste a gazillion entries via the competition form.
