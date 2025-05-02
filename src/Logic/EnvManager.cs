using Abstractions;

using Models;

namespace Logic;
#pragma warning disable CA1303 // Do not pass literals as localized parameters
/// <summary>
/// Manager for environment profiles and variables.
/// </summary>
public sealed class EnvManager : IEnvManager
{

    /// <summary>
    /// Creates <see cref="EnvManager"/>.
    /// </summary>
    /// <param name="profileManager"></param>
    /// <param name="outputProcessor"></param>
    public EnvManager(IProfileManager profileManager, IOutputProcessor outputProcessor)
    {
        ArgumentNullException.ThrowIfNull(profileManager);
        ArgumentNullException.ThrowIfNull(outputProcessor);
        _profileManager = profileManager;
        _outputProcessor = outputProcessor;
    }

    /// <inheritdoc/>
    public void ListProfiles()
    {
        var names = _profileManager.GetProfileNames();
        _outputProcessor.DisplayProfileNames(names);
    }

    /// <inheritdoc/>
    public void ShowProfileValues(ProfileName name)
    {
        ArgumentNullException.ThrowIfNull(name);

        if (_profileManager.TryGetProfile(name, out var profile))
        {
            _outputProcessor.ShowVariables(profile.Variables);
        }
        else
        {
            _outputProcessor.ShowError("Profile not found.");
        }
    }

    /// <inheritdoc/>
    public void ApplyProfile(ProfileName name)
    {
        ArgumentNullException.ThrowIfNull(name);
        var profileName = name.Value;
        _outputProcessor.ApplyProfile(name);
    }

    /// <inheritdoc/>
    public void ShowRealValues()
    {
        Console.WriteLine("Real Variables:");
        Console.WriteLine("MyDatabaseConnectionString: StageConnectionString");
        Console.WriteLine("MyApiEndpoint: https://stage.example.com/api");
        Console.WriteLine("MyLogLevel: Information");
    }

    private readonly IProfileManager _profileManager;
    private readonly IOutputProcessor _outputProcessor;
}

