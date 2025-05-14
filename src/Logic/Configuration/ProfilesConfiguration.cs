using System.Collections.Frozen;

using Models;

namespace Logic.Configuration;

/// <summary>
/// Represents the configuration for profiles and environment variables.
/// </summary>
public class ProfilesConfiguration
{
    /// <summary>
    /// Gets or sets the list of profiles (e.g., Dev, Stage, Prod).
    /// </summary>
    /// <value>
    /// A list of strings representing the profile names.
    /// </value>
    public required HashSet<string> Profiles { get; init; }

    /// <summary>
    /// Gets or sets the environment variables for each profile.
    /// The first level key is the variable name, and the second level is
    /// a dictionary with profile names as keys and their corresponding values.
    /// </summary>
    /// <value>
    /// A dictionary where the key is the name of the environment variable,
    /// and the value is another dictionary where the key is the profile name,
    /// and the value is a string representing the variable value.
    /// </value>
    public required Dictionary<string, Dictionary<string, string>> EnvironmentVariables { get; init; }

    /// <summary>
    /// Create Profiles from config.
    /// </summary>
    public IReadOnlyDictionary<ProfileName, EnvironmentProfile> CreateProfiles()
    {
        List<EnvironmentProfile> items = [];
        foreach (var profileName in Profiles)
        {
            var set = new HashSet<EnvironmentVariable>();
            foreach (var variable in EnvironmentVariables)
            {
                var envsForVariable = variable.Value;

                _ = envsForVariable.TryGetValue(profileName, out var payload)
                    ? set.Add(new EnvironmentVariable(new VariableName(variable.Key), payload))
                    : set.Add(EnvironmentVariable.CreateEmpty(new VariableName(variable.Key)));
            }

            var profile = new EnvironmentProfile(new ProfileName(profileName), set);
            items.Add(profile);
        }

        return items.ToDictionary(x => x.Name, x => x).ToFrozenDictionary();
    }
}


