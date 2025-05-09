using System.Collections.ObjectModel;

using FluentAssertions;

using Logic.Configuration;

using Microsoft.Extensions.Options;

using Models;

using Moq;

namespace Logic.Tests;
#pragma warning disable CA1707 // Identifiers should not contain underscores
#pragma warning disable IDE0058 // Expression value is never used

public class ProfileManagerTests
{
    [Fact(DisplayName = "Constructor initializes profile names and variables correctly")]
    [Trait("Category", "Unit")]
    public void Constructor_InitializesProfilesCorrectly()
    {
        // Arrange
        var profiles = new Collection<string> { "Dev", "Prod" };
        var envVars = new Dictionary<string, Dictionary<string, string>>
        {
            ["API_KEY"] = new() { ["Dev"] = "dev-key", ["Prod"] = "prod-key" },
            ["DB_CONN"] = new() { ["Dev"] = "dev-db", ["Prod"] = "prod-db" }
        };

        var config = new ProfilesConfiguration
        {
            Profiles = profiles,
            EnvironmentVariables = envVars
        };

        var accessCount = 0;
        var optionsMock = new Mock<IOptions<ProfilesConfiguration>>(MockBehavior.Strict);
        optionsMock.Setup(x => x.Value).Returns(() => { accessCount++; return config; });

        // Act
        var manager = new ProfileManager(optionsMock.Object);

        // Assert
        manager.GetProfileNames().Should().BeEquivalentTo(
        [
            new ProfileName("Dev"),
            new ProfileName("Prod")
        ]);

        manager.Variables.Should().BeEquivalentTo(
        [
            new VariableName("API_KEY"),
            new VariableName("DB_CONN")
        ]);

        accessCount.Should().Be(1);
    }

    [Fact(DisplayName = "TryGetProfile returns true and correct profile if found")]
    [Trait("Category", "Unit")]
    public void TryGetProfile_ProfileExists_ReturnsTrueAndProfile()
    {
        // Arrange
        var profiles = new Collection<string> { "Dev" };
        var envVars = new Dictionary<string, Dictionary<string, string>>
        {
            ["TOKEN"] = new() { ["Dev"] = "abc123" }
        };

        var config = new ProfilesConfiguration
        {
            Profiles = profiles,
            EnvironmentVariables = envVars
        };

        var optionsMock = new Mock<IOptions<ProfilesConfiguration>>(MockBehavior.Strict);
        var accessCount = 0;
        optionsMock.Setup(x => x.Value).Returns(() => { accessCount++; return config; });

        var manager = new ProfileManager(optionsMock.Object);
        var name = new ProfileName("Dev");

        // Act
        var result = manager.TryGetProfile(name, out var profile);

        // Assert
        result.Should().BeTrue();
        profile.Should().NotBeNull();
        profile!.Name.Should().Be(name);
        accessCount.Should().Be(1);
    }

    [Fact(DisplayName = "TryGetProfile returns false if profile does not exist")]
    [Trait("Category", "Unit")]
    public void TryGetProfile_ProfileDoesNotExist_ReturnsFalse()
    {
        // Arrange
        var profiles = new Collection<string> { "Dev" };
        var envVars = new Dictionary<string, Dictionary<string, string>>
        {
            ["TOKEN"] = new() { ["Dev"] = "abc123" }
        };

        var config = new ProfilesConfiguration
        {
            Profiles = profiles,
            EnvironmentVariables = envVars
        };

        var optionsMock = new Mock<IOptions<ProfilesConfiguration>>(MockBehavior.Strict);
        var accessCount = 0;
        optionsMock.Setup(x => x.Value).Returns(() => { accessCount++; return config; });

        var manager = new ProfileManager(optionsMock.Object);
        var unknownName = new ProfileName("Stage");

        // Act
        var result = manager.TryGetProfile(unknownName, out var profile);

        // Assert
        result.Should().BeFalse();
        profile.Should().BeNull();
        accessCount.Should().Be(1);
    }

    [Fact(DisplayName = "Constructor throws when options is null")]
    [Trait("Category", "Unit")]
    public void Constructor_NullOptions_ThrowsArgumentNullException()
    {
        // Act
        var exception = Record.Exception(() => _ = new ProfileManager(null!));

        // Assert
        exception.Should().BeOfType<ArgumentNullException>();
    }
}

#pragma warning restore CA1707 // Identifiers should not contain underscores
#pragma warning restore IDE0058 // Expression value is never used