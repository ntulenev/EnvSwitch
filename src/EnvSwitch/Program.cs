using System.CommandLine;
using System.CommandLine.NamingConventionBinder;

var profilesListCommand = new Command("profiles", "List available profiles")
{
    Handler = CommandHandler.Create(() => ListProfiles())
};

var profileCommand = new Command("profile", "Show values for a specific profile")
{
    Handler = CommandHandler.Create<string>(ShowProfileValues)
};
profileCommand.AddOption(new Option<string>("--name", "Name of the profile")
{
    IsRequired = true
});
var applyCommand = new Command("apply", "Apply a specific profile")
{
    Handler = CommandHandler.Create<string>(ApplyProfile)
};
applyCommand.AddOption(new Option<string>("--name", "Name of the profile to apply")
{
    IsRequired = true
});
var variablesCommand = new Command("variables", "Show real environment variable values")
{
    Handler = CommandHandler.Create(() => ShowRealValues())
};

var rootCommand = new RootCommand
        {
            profilesListCommand,
            profileCommand,
            applyCommand,
            variablesCommand,
        };

await rootCommand.InvokeAsync(args);

//Stub methods

void ListProfiles()
{
    Console.WriteLine("Profiles:");
    Console.WriteLine("- Dev");
    Console.WriteLine("- Stage");
    Console.WriteLine("- Prod");
}

void ShowProfileValues(string profileName)
{
    Console.WriteLine($"Profile: {profileName}");
    if (profileName == "Dev")
    {
        Console.WriteLine("MyDatabaseConnectionString: DevConnectionString");
        Console.WriteLine("MyApiEndpoint: https://dev.example.com/api");
        Console.WriteLine("MyLogLevel: Debug");
    }
    else if (profileName == "Stage")
    {
        Console.WriteLine("MyDatabaseConnectionString: StageConnectionString");
        Console.WriteLine("MyApiEndpoint: https://stage.example.com/api");
        Console.WriteLine("MyLogLevel: Information");
    }
    else if (profileName == "Prod")
    {
        Console.WriteLine("MyDatabaseConnectionString: ProdConnectionString");
        Console.WriteLine("MyApiEndpoint: https://prod.example.com/api");
        Console.WriteLine("MyLogLevel: Error");
    }
    else
    {
        Console.WriteLine("Profile not found.");
    }
}

void ApplyProfile(string profileName)
{
    Console.WriteLine($"Profile '{profileName}' applied successfully.");
}

void ShowRealValues()
{
    Console.WriteLine("Real Variables:");
    Console.WriteLine("MyDatabaseConnectionString: StageConnectionString");
    Console.WriteLine("MyApiEndpoint: https://stage.example.com/api");
    Console.WriteLine("MyLogLevel: Information");
}