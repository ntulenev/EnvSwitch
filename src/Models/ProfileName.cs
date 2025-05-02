namespace Models;

/// <summary>
/// Represents the name of an environment profile.
/// </summary>
public sealed class ProfileName
{
    /// <summary>
    /// Gets the name of the profile.
    /// </summary>
    public string Value { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="ProfileName"/> class with the specified profile name.
    /// </summary>
    /// <param name="name">The name of the profile. Cannot be null or whitespace.</param>
    /// <exception cref="ArgumentException">Thrown when the <paramref name="name"/> is null, empty, or consists only of white-space characters.</exception>
    public ProfileName(string name)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name);
        Value = name;
    }
}
