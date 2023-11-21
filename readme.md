# Quickclubs

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
