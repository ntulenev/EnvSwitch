using FluentAssertions;


namespace Models.Tests;
#pragma warning disable CA1707 // Identifiers should not contain underscores
#pragma warning disable IDE0058 // Expression value is never used

public class VariableNameTests
{
    [Fact(DisplayName = "Constructor assigns value correctly")]
    [Trait("Category", "Unit")]
    public void Constructor_ValidName_AssignsValue()
    {
        // Arrange
        var name = "MY_ENV_VAR";

        // Act
        var variableName = new VariableName(name);

        // Assert
        variableName.Value.Should().Be(name);
    }

    [Theory(DisplayName = "Constructor throws for empty, or whitespace input")]
    [Trait("Category", "Unit")]
    [InlineData("")]
    [InlineData("   ")]
    public void Constructor_EmptyOrWhitespace_ThrowsArgumentException(string? input)
    {
        // Act
        var exception = Record.Exception(() => _ = new VariableName(input!));

        // Assert
        exception.Should().BeOfType<ArgumentException>();
    }

    [Fact(DisplayName = "Constructor throws for null, empty, or whitespace input")]
    public void Constructor_NullOrWhitespace_ThrowsArgumentException()
    {
        // Act
        var exception = Record.Exception(() => _ = new VariableName(null!));

        // Assert
        exception.Should().BeOfType<ArgumentNullException>();
    }

    [Fact(DisplayName = "Constructor throws if variable name exceeds 255 characters")]
    [Trait("Category", "Unit")]
    public void Constructor_NameTooLong_ThrowsArgumentException()
    {
        // Arrange
        var longName = new string('A', 256);

        // Act
        var exception = Record.Exception(() => _ = new VariableName(longName));

        // Assert
        exception.Should().BeOfType<ArgumentException>();
    }

    [Theory(DisplayName = "Constructor throws if name does not match regex")]
    [Trait("Category", "Unit")]
    [InlineData("9INVALID")]
    [InlineData("-BADNAME")]
    [InlineData("invalid-char!")]
    public void Constructor_InvalidFormat_ThrowsArgumentException(string invalidName)
    {
        // Act
        var exception = Record.Exception(() => _ = new VariableName(invalidName));

        // Assert
        exception.Should().BeOfType<ArgumentException>();
    }
}

#pragma warning restore CA1707 // Identifiers should not contain underscores
#pragma warning restore IDE0058 // Expression value is never used