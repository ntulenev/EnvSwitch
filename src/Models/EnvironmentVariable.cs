namespace Models;

/// <summary>
/// Represents an environment variable with its name, value (payload), and a flag indicating whether it is set.
/// </summary>
public sealed class EnvironmentVariable
{
    /// <summary>
    /// Gets the name of the environment variable.
    /// </summary>
    public VariableName Name { get; }

    /// <summary>
    /// Generate hashcode based on <see cref="VariableName"/>.
    /// </summary>
    public override int GetHashCode() => Name.GetHashCode();

    /// Checks equality of <see cref="VariableName"/>.
    public override bool Equals(object? obj)
    {
        if (obj is EnvironmentVariable other)
        {
            return Name.Equals(other.Name);
        }

        return false;
    }

    /// <summary>
    /// Gets the value (payload) of the environment variable.
    /// </summary>
    public string Payload { get; }

    /// <summary>
    /// Gets a value indicating whether the environment variable has been set.
    /// </summary>
    public bool IsSet { get; }

    // Private constructor for creating an empty variable
    private EnvironmentVariable(VariableName name)
    {
        ArgumentNullException.ThrowIfNull(name);
        Name = name;
        Payload = null!;
        IsSet = true;
    }

    /// <summary>
    /// Factory method to create an empty environment variable (not set).
    /// </summary>
    /// <param name="name">The name of the environment variable.</param>
    /// <returns>An instance of the <see cref="EnvironmentVariable"/> class.</returns>
    public static EnvironmentVariable CreateEmpty(VariableName name) => new(name);

    /// <summary>
    /// Initializes a new instance of the <see cref="EnvironmentVariable"/> class.
    /// </summary>
    /// <param name="name">The name of the environment variable.</param>
    /// <param name="payload">The value of the environment variable.</param>
    /// <exception cref="ArgumentNullException">Thrown when the <paramref name="name"/> or <paramref name="payload"/> is null.</exception>
    public EnvironmentVariable(VariableName name, string payload)
    {
        ArgumentNullException.ThrowIfNull(name);
        ArgumentException.ThrowIfNullOrEmpty(payload);
        Name = name;
        Payload = payload;
        IsSet = true;
    }
}
