using Abstractions;

using Models;

namespace Logic;
#pragma warning disable CA1303 // Do not pass literals as localized parameters
/// <summary>
/// Manager for environment profiles and variables.
/// </summary>
public sealed class EnvManager : IEnvManager
{

    /// <inheritdoc/>
    public void ListProfiles()
    {

        Console.WriteLine("Profiles:");
        Console.WriteLine("- Dev");
        Console.WriteLine("- Stage");
        Console.WriteLine("- Prod");

    }

    /// <inheritdoc/>
    public void ShowProfileValues(ProfileName name)
    {
        ArgumentNullException.ThrowIfNull(name);
        var profileName = name.Value;
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

    /// <inheritdoc/>
    public void ApplyProfile(ProfileName name)
    {
        ArgumentNullException.ThrowIfNull(name);
        var profileName = name.Value;
        Console.WriteLine($"Profile '{profileName}' applied successfully.");
    }

    /// <inheritdoc/>
    public void ShowRealValues()
    {
        Console.WriteLine("Real Variables:");
        Console.WriteLine("MyDatabaseConnectionString: StageConnectionString");
        Console.WriteLine("MyApiEndpoint: https://stage.example.com/api");
        Console.WriteLine("MyLogLevel: Information");
    }
}

#pragma warning restore CA1303 // Do not pass literals as localized parameters