using System.Diagnostics.CodeAnalysis;

using Models;

namespace Abstractions;

/// <summary>
/// Defines the operations for managing environment profiles.
/// </summary>
public interface IProfileManager
{
    /// <summary>
    /// Gets the collection of profile names.
    /// </summary>
    /// <returns>A read-only collection of <see cref="ProfileName"/> representing the names of all available profiles.</returns>
    IEnumerable<ProfileName> GetProfileNames();

    /// <summary>
    /// Attempts to retrieve a profile by its name.
    /// </summary>
    /// <param name="name">The name of the profile to retrieve.</param>
    /// <param name="profile">When this method returns, contains the profile associated with the provided name if found, otherwise <see langword="null"/>.</param>
    /// <returns><see langword="true"/> if the profile was found; otherwise, <see langword="false"/>.</returns>
    bool TryGetProfile(ProfileName name, [MaybeNullWhen(false)] out EnvironmentProfile profile);
}
