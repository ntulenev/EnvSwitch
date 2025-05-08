using FluentAssertions;


namespace Models.Tests;
#pragma warning disable CA1707 // Identifiers should not contain underscores
#pragma warning disable IDE0058 // Expression value is never used

public class NotificationTests
{
    [Fact(DisplayName = "Constructor assigns text correctly")]
    [Trait("Category", "Unit")]
    public void Constructor_ValidText_AssignsText()
    {
        // Arrange
        var message = "Operation completed successfully.";

        // Act
        var notification = new Notification(message);

        // Assert
        notification.Text.Should().Be(message);
    }


    [Theory(DisplayName = "Constructor throws for empty or whitespace text")]
    [Trait("Category", "Unit")]
    [InlineData("")]
    [InlineData("   ")]
    public void Constructor_EmptyOrWhitespaceText_ThrowsArgumentException(string? input)
    {
        // Act
        var exception = Record.Exception(() => _ = new Notification(input!));

        // Assert
        exception.Should().BeOfType<ArgumentException>();
    }

    [Fact(DisplayName = "Constructor throws for null text")]
    [Trait("Category", "Unit")]
    public void Constructor_NullText_ThrowsArgumentException()
    {
        // Act
        var exception = Record.Exception(() => _ = new Notification(null!));

        // Assert
        exception.Should().BeOfType<ArgumentNullException>();
    }
}


#pragma warning restore CA1707 // Identifiers should not contain underscores
#pragma warning restore IDE0058 // Expression value is never used