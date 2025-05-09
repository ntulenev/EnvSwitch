using FluentAssertions;

using Logic.Configuration;

using Models;

namespace Logic.Tests;
#pragma warning disable CA1707 // Identifiers should not contain underscores
#pragma warning disable IDE0058 // Expression value is never used

public class ProfilesConfigurationTests
{
    [Fact(DisplayName = "CreateProfiles creates correct number of profiles")]
    [Trait("Category", "Unit")]
    public void CreateProfiles_CreatesExpectedProfiles()
    {
        // Arrange
        var config = new ProfilesConfiguration
        {
            Profiles = ["Dev", "Prod"],
            EnvironmentVariables = new Dictionary<string, Dictionary<string, string>>
            {
                ["API_KEY"] = new() { ["Dev"] = "dev-key", ["Prod"] = "prod-key" },
                ["DEBUG"] = new() { ["Dev"] = "true" }
            }
        };

        // Act
        var result = config.CreateProfiles();

        // Assert
        result.Should().HaveCount(2);
        result.Keys.Should().BeEquivalentTo([new ProfileName("Dev"), new ProfileName("Prod")]);
    }

    [Fact(DisplayName = "CreateProfiles populates environment variables correctly")]
    [Trait("Category", "Unit")]
    public void CreateProfiles_EnvironmentVariables_AreCorrectlyAssigned()
    {
        // Arrange
        var config = new ProfilesConfiguration
        {
            Profiles = ["Stage"],
            EnvironmentVariables = new Dictionary<string, Dictionary<string, string>>
            {
                ["CONN_STRING"] = new() { ["Stage"] = "staging-db" },
                ["LOGGING"] = [] // missing value for Stage
            }
        };

        // Act
        var result = config.CreateProfiles();

        // Assert
        var profile = result[new ProfileName("Stage")];
        profile.Variables.Should().ContainEquivalentOf(new EnvironmentVariable(new VariableName("CONN_STRING"), "staging-db"));
        profile.Variables.Should().Contain(x => x.Name == new VariableName("LOGGING") && !x.IsSet);
    }

    [Fact(DisplayName = "CreateProfiles handles empty configuration gracefully")]
    [Trait("Category", "Unit")]
    public void CreateProfiles_EmptyConfig_ReturnsEmptyDictionary()
    {
        // Arrange
        var config = new ProfilesConfiguration
        {
            Profiles = [],
            EnvironmentVariables = []
        };

        // Act
        var result = config.CreateProfiles();

        // Assert
        result.Should().BeEmpty();
    }
}

#pragma warning restore CA1707 // Identifiers should not contain underscores
#pragma warning restore IDE0058 // Expression value is never used