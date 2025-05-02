using System.Collections.Frozen;
using System.Diagnostics.CodeAnalysis;

using Abstractions;

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
    public ProfileManager()
    {

        var devProfile = new EnvironmentProfile(new ProfileName("Dev"),
            new HashSet<EnvironmentVariable>()
            {
                new(new VariableName("MyDatabaseConnectionString"),"DevConnectionString"),
                new(new VariableName("MyApiEndpoint"),"https://dev.example.com/api"),
                new(new VariableName("MyLogLevel"),"Debug")
            }
        );

        var stgProfile = new EnvironmentProfile(new ProfileName("Stage"),
             new HashSet<EnvironmentVariable>()
             {
                new(new VariableName("MyDatabaseConnectionString"),"StageConnectionString"),
                new(new VariableName("MyApiEndpoint"),"https://stage.example.com/api"),
                new(new VariableName("MyLogLevel"),"Information")
             }
         );

        var prdProfile = new EnvironmentProfile(new ProfileName("Prod"),
            new HashSet<EnvironmentVariable>()
            {
                new(new VariableName("MyDatabaseConnectionString"),"ProdConnectionString"),
                new(new VariableName("MyApiEndpoint"),"https://prod.example.com/api"),
                new(new VariableName("MyLogLevel"),"Error")
            }
        );

        var items = new List<EnvironmentProfile>()
        {
            devProfile, stgProfile, prdProfile
        };

        _profiles = items.ToDictionary(x => x.Name, x => x).ToFrozenDictionary();
    }

    /// <inheritdoc/>
    public IEnumerable<ProfileName> GetProfileNames() => _profiles.Keys;

    /// <inheritdoc/>
    public bool TryGetProfile(ProfileName name, [MaybeNullWhen(false)] out EnvironmentProfile profile) => _profiles.TryGetValue(name, out profile);

    private readonly IReadOnlyDictionary<ProfileName, EnvironmentProfile> _profiles;
}
