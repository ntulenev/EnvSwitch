<img src="Logo.png" width="80" />

# EnvSwitch
Utility for quickly switching environment variables between multiple profiles (e.g. Dev, Stage, Prod)

## Config example:

```yaml
{
  "ProfilesConfiguration": {
    "Profiles": [
      "Dev",
      "Stage",
      "Prod"
    ],
    "EnvironmentVariables": {
      "MyDatabaseConnectionString": {
        "Dev": "DevConnectionString",
        "Stage": "StageConnectionString",
        "Prod": "ProdConnectionString"
      },
      "MyApiEndpoint": {
        "Dev": "https://dev.example.com/api",
        "Stage": "https://stage.example.com/api"
      },
      "MyLogLevel": {
        "Dev": "Debug",
        "Stage": "Information",
        "Prod": "Error"
      }
    }
  }
}
```

Filling in the profile for a variable is optional. 
If a variable is not provided for a specific profile, its value will be deleted when the profile is applied.

## Commands

### 1. List available profiles
Command:
```bash
envswitch profiles
```

Example output:
```bash
Profiles:
- Dev
- Stage
- Prod
```

### 2. Show variable values for a profile
Command:
```bash
envswitch profile --name <profile-name>
```

Example usage:
```bash
envswitch profile --name Dev
```

Example output:
```bash
MyApiEndpoint : https://dev.example.com/api
MyDatabaseConnectionString : DevConnectionString
MyLogLevel : Debug
```

### 3. Apply a profile
Command:
```bash
envswitch apply --name <profile-name>
```

Example usage:
```bash
envswitch apply --name Stage
```

Example output:
```bash
Profile 'Stage' applied successfully.
```

### 4.  Show currentreal variable values
Command:
```bash
envswitch variables
```

Example output:
```bash
MyApiEndpoint : https://stage.example.com/api
MyDatabaseConnectionString : StageConnectionString
MyLogLevel : Information
```


