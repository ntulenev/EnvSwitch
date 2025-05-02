using Models;

namespace Abstractions;

/// <summary>
/// Interface for managing environment profiles and variables.
/// </summary>
public interface IEnvManager
{
    /// <summary>
    /// Lists all available environment profiles.
    /// </summary>
    void ListProfiles();

    /// <summary>
    /// Shows the values of variables for a specific environment profile.
    /// </summary>
    /// <param name="name">The name of the profile to display values for.</param>
    void ShowProfileValues(ProfileName name);

    /// <summary>
    /// Applies a specific environment profile, activating its associated settings.
    /// </summary>
    /// <param name="name">The name of the profile to apply.</param>
    void ApplyProfile(ProfileName name);

    /// <summary>
    /// Displays the current real values of environment variables.
    /// </summary>
    void ShowRealValues();
}
