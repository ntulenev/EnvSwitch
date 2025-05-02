using System.Collections.Frozen;
using System.Diagnostics.CodeAnalysis;

using Abstractions;

using Logic.Configuration;

using Microsoft.Extensions.Options;

using Models;

namespace Logic;

/// <summary>
/// Contains the operations for managing environment profiles.
/// </summary>
public sealed class ProfileManager : IProfileManager
{
    /// <summary>
    /// Creates <see cref="ProfileManager"/>.
    /// </summary>
    public ProfileManager(IOptions<ProfilesConfiguration> options)
    {
        ArgumentNullException.ThrowIfNull(options);

        var config = options.Value;
        _profiles = config.CreateProfiles();
        Variables = _profiles.SelectMany(x => x.Value.Variables).Select(x => x.Name).ToHashSet().ToFrozenSet();

    }

    /// <inheritdoc/>
    public IEnumerable<ProfileName> GetProfileNames() => _profiles.Keys;

    /// <inheritdoc/>
    public IReadOnlySet<VariableName> Variables { get; }

    /// <inheritdoc/>
    public bool TryGetProfile(ProfileName name, [MaybeNullWhen(false)] out EnvironmentProfile profile) =>
        _profiles.TryGetValue(name, out profile);

    private readonly IReadOnlyDictionary<ProfileName, EnvironmentProfile> _profiles;
}
