using Abstractions;

using Models;

namespace Infrastructure;

/// <summary>
/// Displays output data
/// </summary>
public sealed class OutputProcessor : IOutputProcessor
{
    /// <inheritdoc/>
    public void ApplyProfile(ProfileName profileName)
    {
        ArgumentNullException.ThrowIfNull(profileName);
        Console.WriteLine($"Profile '{profileName.Value}' applied successfully.");
    }

    /// <inheritdoc/>
    public void DisplayProfileNames(IEnumerable<ProfileName> profiles)
    {
        ArgumentNullException.ThrowIfNull(profiles);

        Console.WriteLine("Profiles:");
        foreach (var profileName in profiles)
        {
            Console.WriteLine($"- {profileName.Value}");
        }
    }

    /// <inheritdoc/>
    public void ShowVariables(IEnumerable<EnvironmentVariable> variables)
    {
        ArgumentNullException.ThrowIfNull(variables);

        foreach (var profileVar in variables)
        {
            if (profileVar.IsSet)
            {
                Console.WriteLine($"{profileVar} : {profileVar.Payload}");
            }
            {
                Console.WriteLine($"{profileVar} is not set");
            }
        }
    }

    /// <inheritdoc/>
    public void ShowError(string message)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(message);
        Console.WriteLine(message);
    }
}
