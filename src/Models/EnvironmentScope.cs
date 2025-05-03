namespace Models;

/// <summary>
/// Defines the possible scopes for environment variables.
/// </summary>
public enum EnvironmentScope
{
    /// <summary>
    /// Represents the user-level environment variables.
    /// These variables apply to the current user and are available for all applications running under that user profile.
    /// </summary>
    User,

    /// <summary>
    /// Represents the workstation-level (machine-level) environment variables.
    /// These variables apply to all users and processes running on the machine.
    /// </summary>
    Workstation
}

