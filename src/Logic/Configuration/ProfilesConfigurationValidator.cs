using Microsoft.Extensions.Options;

namespace Logic.Configuration;
/// <summary>
/// Validates the <see cref="ProfilesConfiguration"/> for correctness.
/// </summary>
public sealed class ProfilesConfigurationValidator : IValidateOptions<ProfilesConfiguration>
{

    /// <inheritdoc/>
    public ValidateOptionsResult Validate(string? name, ProfilesConfiguration options)
    {
        if (options is null)
        {
            return ValidateOptionsResult.Fail("ProfilesConfiguration is null.");
        }

        if (options.Profiles is null || options.Profiles.Count == 0)
        {
            return ValidateOptionsResult.Fail("ProfilesConfiguration.Profiles must contain at least one entry.");
        }

        if (options.EnvironmentVariables is null || options.EnvironmentVariables.Count == 0)
        {
            return ValidateOptionsResult.Fail("ProfilesConfiguration.EnvironmentVariables must contain at least one entry.");
        }

        foreach (var profile in options.Profiles)
        {
            if (string.IsNullOrWhiteSpace(profile))
            {
                return ValidateOptionsResult.Fail("ProfilesConfiguration.Profiles contains an invalid (null/empty/whitespace) profile name.");
            }
        }

        foreach (var (variableName, valuesByProfile) in options.EnvironmentVariables)
        {
            if (string.IsNullOrWhiteSpace(variableName))
            {
                return ValidateOptionsResult.Fail("Environment variable name cannot be null or whitespace.");
            }

            if (valuesByProfile is null)
            {
                return ValidateOptionsResult.Fail($"Environment variable '{variableName}' contains null profile map.");
            }

            foreach (var (profileKey, value) in valuesByProfile)
            {
                if (string.IsNullOrWhiteSpace(profileKey))
                {
                    return ValidateOptionsResult.Fail($"Environment variable '{variableName}' contains null or whitespace profile name key.");
                }

                if (value is null)
                {
                    return ValidateOptionsResult.Fail($"Environment variable '{variableName}' has null value for profile '{profileKey}'.");
                }

                if (!options.Profiles.Contains(profileKey))
                {
                    return ValidateOptionsResult.Fail($"Environment variable '{variableName}' references unknown profile '{profileKey}'. " +
                        $"All profile keys must match those declared in Profiles.");
                }
            }
        }

        return ValidateOptionsResult.Success;
    }
}
