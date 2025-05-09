using FluentAssertions;

namespace Models.Tests;
#pragma warning disable CA1707 // Identifiers should not contain underscores
#pragma warning disable IDE0058 // Expression value is never used

public class EnvironmentVariableTests
{
    [Fact(DisplayName = "Constructor assigns name and payload correctly")]
    [Trait("Category", "Unit")]
    public void Constructor_ValidInputs_AssignsProperties()
    {
        // Arrange
        var name = new VariableName("VAR_NAME");
        var payload = "SomeValue";

        // Act
        var variable = new EnvironmentVariable(name, payload);

        // Assert
        variable.Name.Should().Be(name);
        variable.Payload.Should().Be(payload);
        variable.IsSet.Should().BeTrue();
    }

    [Fact(DisplayName = "Constructor throws when name is null")]
    [Trait("Category", "Unit")]
    public void Constructor_NullName_ThrowsArgumentNullException()
    {
        // Arrange
        var payload = "Value";

        // Act
        var exception = Record.Exception(() => _ = new EnvironmentVariable(null!, payload));

        // Assert
        exception.Should().BeOfType<ArgumentNullException>();
    }

    [Fact(DisplayName = "Constructor throws when payload is null")]
    [Trait("Category", "Unit")]
    public void Constructor_NullPayload_ThrowsArgumentNullException()
    {
        // Arrange
        var name = new VariableName("VAR_NAME");

        // Act
        var exception = Record.Exception(() => _ = new EnvironmentVariable(name, null!));

        // Assert
        exception.Should().BeOfType<ArgumentNullException>();
    }

    [Fact(DisplayName = "Constructor throws when payload is empty")]
    [Trait("Category", "Unit")]
    public void Constructor_EmptyPayload_ThrowsArgumentException()
    {
        // Arrange
        var name = new VariableName("VAR_NAME");

        // Act
        var exception = Record.Exception(() => _ = new EnvironmentVariable(name, ""));

        // Assert
        exception.Should().BeOfType<ArgumentException>();
    }

    [Fact(DisplayName = "CreateEmpty returns unset variable with correct name")]
    [Trait("Category", "Unit")]
    public void CreateEmpty_ReturnsUnsetVariable()
    {
        // Arrange
        var name = new VariableName("EMPTY_VAR");

        // Act
        var variable = EnvironmentVariable.CreateEmpty(name);

        // Assert
        variable.Name.Should().Be(name);
        variable.IsSet.Should().BeFalse();
        variable.Payload.Should().BeNull();
    }

    [Fact(DisplayName = "Equals returns true for variables with same name")]
    [Trait("Category", "Unit")]
    public void Equals_SameName_ReturnsTrue()
    {
        // Arrange
        var name = new VariableName("ENV_VAR");
        var var1 = new EnvironmentVariable(name, "val1");
        var var2 = new EnvironmentVariable(name, "val2");

        // Act & Assert
        var1.Equals(var2).Should().BeTrue();
    }

    [Fact(DisplayName = "Equals returns false for variables with different names")]
    [Trait("Category", "Unit")]
    public void Equals_DifferentName_ReturnsFalse()
    {
        // Arrange
        var var1 = new EnvironmentVariable(new VariableName("VAR1"), "val1");
        var var2 = new EnvironmentVariable(new VariableName("VAR2"), "val2");

        // Act & Assert
        var1.Equals(var2).Should().BeFalse();
    }

    [Fact(DisplayName = "GetHashCode returns same value for same name")]
    [Trait("Category", "Unit")]
    public void GetHashCode_SameName_SameHash()
    {
        // Arrange
        var name = new VariableName("SAME_VAR");
        var var1 = new EnvironmentVariable(name, "val1");
        var var2 = new EnvironmentVariable(name, "val2");

        // Act & Assert
        var1.GetHashCode().Should().Be(var2.GetHashCode());
    }
}

#pragma warning restore CA1707 // Identifiers should not contain underscores
#pragma warning restore IDE0058 // Expression value is never used