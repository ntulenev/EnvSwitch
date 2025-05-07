using FluentAssertions;

namespace Models.Tests;

#pragma warning disable CA1707 // Identifiers should not contain underscores
#pragma warning disable IDE0058 // Expression value is never used

public class ProfileNameTests
{
    [Fact(DisplayName = "Constructor assigns value correctly")]
    [Trait("Category", "Unit")]

    public void Constructor_ValidName_AssignsValue()
    {
        // Arrange
        var name = "Development";

        // Act
        var profileName = new ProfileName(name);

        // Assert
        profileName.Value.Should().Be(name);
    }

    [Theory(DisplayName = "Constructor throws for empty or whitespace input")]
    [Trait("Category", "Unit")]
    [InlineData("")]
    [InlineData("   ")]
    public void Constructor_NullOrWhitespace_ThrowsArgumentException(string? input)
    {
        // Act
        var exception = Record.Exception(() => _ = new ProfileName(input!));

        // Assert
        exception.Should().BeOfType<ArgumentException>();

    }

    [Fact(DisplayName = "Constructor throws for null")]
    [Trait("Category", "Unit")]
    public void Constructor_NullOrWhitespace_ThrowsArgumentNullException()
    {
        // Act
        var exception = Record.Exception(() => _ = new ProfileName(null!));

        // Assert
        exception.Should().BeOfType<ArgumentNullException>();

    }
}

#pragma warning restore CA1707 // Identifiers should not contain underscores
#pragma warning restore IDE0058 // Expression value is never used