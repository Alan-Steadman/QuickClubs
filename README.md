# QuickClubs

## Architecture

- Domain Driven Design by Eric Evans
- Clean Architecture by Robert C Martin ('Uncle Bob')
- CQRS by Greg Young
- Screaming Architecture / Vertical Slice Architecture

## Tech Stack

- Sql Server
- EF Core
- Dapper
- Blazor
- MediatR
- Docker Compose
- Fluent Validation
- Serilog
- Mapster
- Email/SMTP

## Installation

### Environment file for docker compose
Create a .env file in the same folder as the docker-compose.yml to hold the sa password.  The file contents should be:

```
SA_PASSWORD=MyStrongPassword
```
Note this same sa password must match the one set in the user secrets below

### User Secrets

Edit the user secrets of the QuickClubs.WebApi project by right clicking it and selecting 'Manage User Secrets'.  Add the following user secrets:

#### Connection String
Add a connection string using the values below, replacing the sa password to something more suitable: that matches the sa_password in the docker compose environemtn file (.env) above.

```json
{
  "ConnectionStrings:Database": "server=quickclubs.db;database=quickclubs;user id=sa;password=MyStrongPassword;encrypt=false;"
}
```

#### JWT Secret:
```json
{
  "JwtSettings:Secret": "Strong_and_at_least_32_charas_long"
}
```

#### Email SMTP Username & Password:

Included in the dev setup is a smtp4dev docker container.  This smtp4dev does not use username & password for authentication, however, any 'real' smtp server such as SendInBlue does user a username & password.  Hence if using a 'real' smtp server, the username & password secrets will need to be set, if using smtp4dev, they do not need to be set.

```json
{
  "EmailSettings:Username": "SmtpUsername",
  "EmailSettings:Password": "SmtpPassword"
}
```

Debug Email To:
this sends all email to the address provided, for use during development.  It can either be set to empty string to have email routed normally, or enter an email address to have all email sent there.
```json
{
  "EmailSettings:DebugEmailTo": "my.personal.test.address@email.com"
}
```

## Running the project

Set the startup project to 'docker-compose' and press F5.

### Links

- Admin UI: https://localhost:5442/
- Web Api: https://localhost:5443/api/
- Swagger : https://localhost:5443/swagger/index.html
- smtp4dev : http://localhost:3000/

## Next Steps and TODOs

### Replace existing Blazor AdminUI

The current QuickClubs.AdminUI Blazor project is just a quick and dirty exploratory prototype.  This will soon be replaced with a cleaner, three project implementation, consisting of:

- An ApiClient generated using NSwag
- A client infrastructure project, containing all infrastructure that can be pulled out of the blazor project
- A thin Blazor AdminUI, using vertical slice feature folders

### Minimal APIs
- Convert controllers to Minimal APIs

### Tests
- Integration Tests
- Functional Tests 

### Refresh Tokens
- Implement Refresh Tokens

### Other TODOs

See Task View in Visual Studio for individually commented TODO items

## Licensing

This software is unlicensed.  This means that whilst the source code is currently in the public domain on github, it is not licensed for any use.  The copyright belongs to the creator.  It is illegal to copy, modify or distribute this software.
