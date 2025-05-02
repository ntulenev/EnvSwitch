namespace Models;
/// <summary>
/// Represents the name of an environment variable.
/// </summary>
public sealed record class VariableName
{
    /// <summary>
    /// Gets the name of the variable.
    /// </summary>
    public string Value { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="VariableName"/> class with the specified variable name.
    /// </summary>
    /// <param name="name">The name of the variable. Cannot be null or whitespace.</param>
    /// <exception cref="ArgumentException">Thrown when the <paramref name="name"/>
    /// is null, empty, or consists only of white-space characters.</exception>
    public VariableName(string name)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name);
        Value = name;
    }
}
