using System.Collections.Frozen;

namespace Models;

/// <summary>
/// Represents a profile containing a set of environment variables.
/// </summary>
public class EnvironmentProfile
{
    /// <summary>
    /// Gets the name of the profile.
    /// </summary>
    public ProfileName Name { get; }

    /// <summary>
    /// Gets a read-only dictionary of environment variables associated with this profile.
    /// </summary>
    public IReadOnlySet<EnvironmentVariable> Variables { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="EnvironmentProfile"/> class.
    /// </summary>
    /// <param name="name">The name of the profile.</param>
    /// <param name="variables">The collection of environment variables associated with the profile.</param>
    /// <exception cref="ArgumentNullException">Thrown when the <paramref name="name"/> or <paramref name="variables"/> is null.</exception>
    /// <exception cref="ArgumentException">Thrown when the <paramref name="variables"/> collection is empty.</exception>
    public EnvironmentProfile(ProfileName name, IReadOnlySet<EnvironmentVariable> variables)
    {
        ArgumentNullException.ThrowIfNull(name);
        ArgumentNullException.ThrowIfNull(variables);

        if (variables.Count == 0)
        {
            throw new ArgumentException("Variables collection has no items");
        }

        Name = name;
        Variables = variables.ToFrozenSet();
    }
}

