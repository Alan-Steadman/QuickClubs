# QuickClubs

## Tech Stack

- SqlServer
- Dapper
- EF Core
- Email/SMTP
- Serilog
- MediatR
- Docker Compose
- Fluent Validation

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

## Licensing

This software is unlicensed.  This means that whilst the source code is currently in the public domain on github, it is not licensed for any use.  The copyright belongs to the creator.  It is illegal to copy, modify or distribute this software.