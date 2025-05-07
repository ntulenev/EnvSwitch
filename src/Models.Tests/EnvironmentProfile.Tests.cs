using FluentAssertions;

namespace Models.Tests;
#pragma warning disable CA1707 // Identifiers should not contain underscores
#pragma warning disable IDE0058 // Expression value is never used

public class EnvironmentProfileTests
{
    [Fact(DisplayName = "Constructor assigns name and variables correctly")]
    [Trait("Category", "Unit")]
    public void Constructor_ValidInputs_AssignsProperties()
    {
        // Arrange
        var profileNameValue = "TestProfile";
        var name = new ProfileName(profileNameValue);
        var variables = new HashSet<EnvironmentVariable>
            {
                new(new VariableName("VAR1"), "Value1"),
                new(new VariableName("VAR2"), "Value2")
            };

        // Act
        var profile = new EnvironmentProfile(name, variables);

        // Assert
        profile.Name.Should().Be(name);
        profile.Variables.Should().BeEquivalentTo(variables);
    }

    [Fact(DisplayName = "Constructor throws when name is null")]
    [Trait("Category", "Unit")]
    public void Constructor_NullName_ThrowsArgumentNullException()
    {
        // Arrange
        var variables = new HashSet<EnvironmentVariable>
            {
                new(new VariableName("VAR1"), "Value1")
            };

        // Act
        var exception = Record.Exception(() => _ = new EnvironmentProfile(null!, variables));

        // Assert
        exception.Should().BeOfType<ArgumentNullException>();
    }

    [Fact(DisplayName = "Constructor throws when variables is null")]
    [Trait("Category", "Unit")]
    public void Constructor_NullVariables_ThrowsArgumentNullException()
    {
        // Arrange
        var name = new ProfileName("TestProfile");

        // Act
        var exception = Record.Exception(() => _ = new EnvironmentProfile(name, null!));

        // Assert
        exception.Should().BeOfType<ArgumentNullException>();
    }

    [Fact(DisplayName = "Constructor throws when variables collection is empty")]
    [Trait("Category", "Unit")]
    public void Constructor_EmptyVariables_ThrowsArgumentException()
    {
        // Arrange
        var name = new ProfileName("TestProfile");
        var variables = new HashSet<EnvironmentVariable>();

        // Act
        var exception = Record.Exception(() => _ = new EnvironmentProfile(name, variables));

        // Assert
        exception.Should().BeOfType<ArgumentException>()
            .Which.ParamName.Should().Be("variables");
    }

    [Fact(DisplayName = "Name property returns correct profile name")]
    [Trait("Category", "Unit")]
    public void Name_Property_ReturnsCorrectValue()
    {
        // Arrange
        var name = new ProfileName("MyProfile");
        var variables = new HashSet<EnvironmentVariable>
            {
                new(new VariableName("VAR1"), "Value1")
            };

        var profile = new EnvironmentProfile(name, variables);

        // Act & Assert
        profile.Name.Should().Be(name);
    }

    [Fact(DisplayName = "Variables property returns correct variable set")]
    [Trait("Category", "Unit")]
    public void Variables_Property_ReturnsCorrectSet()
    {
        // Arrange
        var name = new ProfileName("EnvProfile");
        var expectedVariables = new HashSet<EnvironmentVariable>
            {
                new(new VariableName("API_KEY"), "123"),
                new(new VariableName("DB_CONN"), "abc")
            };

        var profile = new EnvironmentProfile(name, expectedVariables);

        // Act & Assert
        profile.Variables.Should().BeEquivalentTo(expectedVariables);
    }
}



#pragma warning restore CA1707 // Identifiers should not contain underscores
#pragma warning restore IDE0058 // Expression value is never used