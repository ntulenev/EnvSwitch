using FluentAssertions;

using Logic.Configuration;

namespace Logic.Tests;

public class ProfilesConfigurationValidatorTests
#pragma warning disable CA1707 // Identifiers should not contain underscores
#pragma warning disable IDE0058 // Expression value is never used
{
    [Fact(DisplayName = "Returns failure if config is null")]
    public void Validate_NullConfig_Fails()
    {
        // Arrange
        var validator = new ProfilesConfigurationValidator();

        // Act
        var result = validator.Validate(null, null!);

        // Assert
        result.Failed.Should().BeTrue();
    }

    [Fact(DisplayName = "Returns failure if profiles section is empty")]
    public void Validate_EmptyProfiles_Fails()
    {
        // Arrange
        var config = new ProfilesConfiguration
        {
            Profiles = [],
            EnvironmentVariables = new() { { "VAR", new() { { "Dev", "value" } } } }
        };
        var validator = new ProfilesConfigurationValidator();

        // Act
        var result = validator.Validate(null, config);

        // Assert
        result.Failed.Should().BeTrue();
    }

    [Fact(DisplayName = "Returns failure if EnvironmentVariables is null or empty")]
    public void Validate_EmptyVariables_Fails()
    {
        // Arrange
        var config = new ProfilesConfiguration
        {
            Profiles = ["Dev"],
            EnvironmentVariables = []
        };
        var validator = new ProfilesConfigurationValidator();

        // Act
        var result = validator.Validate(null, config);

        // Assert
        result.Failed.Should().BeTrue();
    }

    [Fact(DisplayName = "Returns failure if profile names contain whitespace")]
    public void Validate_ProfilesContainWhitespace_Fails()
    {
        // Arrange
        var config = new ProfilesConfiguration
        {
            Profiles = ["Dev", "  "],
            EnvironmentVariables = new() { { "VAR", new() { { "Dev", "value" } } } }
        };
        var validator = new ProfilesConfigurationValidator();

        // Act
        var result = validator.Validate(null, config);

        // Assert
        result.Failed.Should().BeTrue();
    }

    [Fact(DisplayName = "Returns failure if variable name is whitespace")]
    public void Validate_VariableNameWhitespace_Fails()
    {
        // Arrange
        var config = new ProfilesConfiguration
        {
            Profiles = ["Dev"],
            EnvironmentVariables = new() { { " ", new() { { "Dev", "value" } } } }
        };
        var validator = new ProfilesConfigurationValidator();

        // Act
        var result = validator.Validate(null, config);

        // Assert
        result.Failed.Should().BeTrue();
    }

    [Fact(DisplayName = "Returns failure if value profile is not declared in Profiles")]
    public void Validate_UnknownProfileInVariable_Fails()
    {
        // Arrange
        var config = new ProfilesConfiguration
        {
            Profiles = ["Prod"],
            EnvironmentVariables = new() { { "VAR", new() { { "Dev", "value" } } } }
        };
        var validator = new ProfilesConfigurationValidator();

        // Act
        var result = validator.Validate(null, config);

        // Assert
        result.Failed.Should().BeTrue();
    }

    [Fact(DisplayName = "Returns success if configuration is valid")]
    public void Validate_ValidConfiguration_Succeeds()
    {
        // Arrange
        var config = new ProfilesConfiguration
        {
            Profiles = ["Dev", "Prod"],
            EnvironmentVariables = new()
            {
                { "VAR1", new() { { "Dev", "abc" }, { "Prod", "xyz" } } },
                { "VAR2", new() { { "Dev", "true" } } }
            }
        };
        var validator = new ProfilesConfigurationValidator();

        // Act
        var result = validator.Validate(null, config);

        // Assert
        result.Succeeded.Should().BeTrue();
    }
}

#pragma warning restore CA1707 // Identifiers should not contain underscores
#pragma warning restore IDE0058 // Expression value is never used

