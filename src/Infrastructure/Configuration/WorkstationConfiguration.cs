using Models;

namespace Infrastructure.Configuration;

/// <summary>
/// Represents the configuration settings for managing environment variable scopes.
/// </summary>
public sealed class WorkstationConfiguration
{
    /// <summary>
    /// Gets or sets the scope of the environment variables.
    /// Defines whether the environment variables should be applied at the user level or workstation level.
    /// </summary>
    /// <value>
    /// The scope can be one of the <see cref="EnvironmentScope"/> values: 
    /// <see cref="EnvironmentScope.User"/> for user-level variables, or 
    /// <see cref="EnvironmentScope.Workstation"/> for machine-level variables.
    /// </value>
    public required EnvironmentScope Scope { get; init; }
}

