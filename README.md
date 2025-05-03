<img src="Logo.png" width="80" />

# EnvSwitch
Utility for quickly switching environment variables between multiple profiles (e.g. Dev, Stage, Prod)

### 📌 Note

This is still an early version of the application.  
Validations and tests are currently in progress.  

- **Planned updates**:  
  More detailed and visually enhanced data visualization.

## Config example:

```yaml
{
  "WorkstationConfiguration": {
    "Scope": "User" 
  },
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

## Configuration Overview

### WorkstationConfiguration
Scope: Defines the target scope for Environment Variables.

| **Scope**      | **Description**                                               |
|----------------|---------------------------------------------------------------|
| **User**       | Applies the configuration on a per-user basis.               |
| **Workstation**| Applies the configuration on a per-workstation (machine) basis. |

### ProfilesConfiguration
Profiles: An array of profile names representing different environments. These profiles can be customized based on your needs and might include environments like Dev, Stage, Prod, or any other environments specific to your setup.

EnvironmentVariables:
Defines the environment variables that will be switched based on the active profile. For each environment variable, you can define specific values for each profile, such as Dev, Stage, Prod, or any custom profiles you define.

The environment variable can be specified for all profiles or only for a subset of them.

If a value is not specified for a profile, the value for that variable will be empty for that profile.


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

### 4.  Show current system variable values
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


