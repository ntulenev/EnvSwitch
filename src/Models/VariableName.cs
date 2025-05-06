using System.Text.RegularExpressions;

namespace Models;
/// <summary>
/// Represents the name of an environment variable.
/// </summary>
public sealed partial record class VariableName
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

        if (name.Length > 255)
        {
            throw new ArgumentException("The environment variable name cannot exceed 255 characters.", nameof(name));
        }

        if (!EnvNameRegex().IsMatch(name))
        {
            throw new ArgumentException("The environment variable name can only contain letters, digits, and " +
                "underscores, and must start with a letter or an underscore.", nameof(name));
        }

        Value = name;
    }

    [GeneratedRegex(@"^[A-Za-z_][A-Za-z0-9_]*$")]
    private static partial Regex EnvNameRegex();
}
