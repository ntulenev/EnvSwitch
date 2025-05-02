using Models;

namespace Abstractions;

/// <summary>
/// Defines methods for displaying and applying environment profiles and variables.
/// </summary>
public interface IOutputProcessor
{
    /// <summary>
    /// Displays a list of available profile names.
    /// </summary>
    /// <param name="profiles">The collection of profile names to display.</param>
    void DisplayProfileNames(IEnumerable<ProfileName> profiles);

    /// <summary>
    /// Shows a list of environment variables.
    /// </summary>
    /// <param name="variables">The collection of environment variables to show.</param>
    void ShowVariables(IEnumerable<EnvironmentVariable> variables);

    /// <summary>
    /// Applies the specified environment profile.
    /// </summary>
    /// <param name="profileName">The profile to apply.</param>
    void ApplyProfile(ProfileName profileName);

    /// <summary>
    /// Display  error
    /// </summary>
    /// <param name="message">Error message</param>
    void ShowError(string message);
}
