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

Enter a connection string using the values below, replacing the sa password to something more suitable:
```json
{
  "ConnectionStrings:Database": "server=quickclubs.db;database=quickclubs;user id=sa;password=MyStrongPassword;encrypt=false;"
}
```

JWT Secret:
```json
{
  "JwtSettings:Secret": "Strong_and_at_least_16_charas"
}
```

Email SMTP Username & Password:
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

## Next Steps and TODOs

### Replace existing Blazor AdminUI

The current QuickClubs.AdminUI Blazor project is just a quick and dirty exploratory prototype.  This will soon be replaced with a cleaner, three project implementation, consisting of:

- An ApiClient generated using NSwag
- A client infrastructure project, containing all infrastructure that can be pulled out of the blazor project
- A thin Blazor AdminUI, using vertical slice feature folders

### Tests
- Integration Tests
- Functional Tests 

### Other TODOs

See Task View in Visual Studio for individually commented TODO items

## Licensing

This software is unlicensed.  This means that whilst the source code is currently in the public domain on github, it is not licensed for any use.  The copyright belongs to the creator.  It is illegal to copy, modify or distribute this software.
