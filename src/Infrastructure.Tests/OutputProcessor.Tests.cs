using FluentAssertions;

using Models;

namespace Infrastructure.Tests;
#pragma warning disable CA1707 // Identifiers should not contain underscores
#pragma warning disable IDE0058 // Expression value is never used

/// <summary>
/// Unit tests for <see cref="OutputProcessor"/>.
/// These tests capture and assert basic console output text.
/// Note: Styling (e.g., ConsoleColor or Spectre.Console markup) is not validated.
/// </summary>
public class OutputProcessorTests
{
    [Fact(DisplayName = "ApplyProfile writes correct output to console")]
    [Trait("Category", "Unit")]
    public void ApplyProfile_WritesExpectedMessage()
    {
        // Arrange
        var processor = new OutputProcessor();
        var profileName = new ProfileName("Dev");
        using var writer = new StringWriter();
        var original = Console.Out;
        Console.SetOut(writer);

        // Act
        processor.ApplyProfile(profileName);
        Console.SetOut(original);

        // Assert
        var output = writer.ToString();
        output.Should().Contain("Profile '");
        output.Should().Contain("Dev");
        output.Should().Contain("applied successfully");
    }

    [Fact(DisplayName = "DisplayProfileNames writes all profile names")]
    [Trait("Category", "Unit")]
    public void DisplayProfileNames_WritesProfileList()
    {
        // Arrange
        var processor = new OutputProcessor();
        var profiles = new List<ProfileName> { new("Dev"), new("Prod") };
        using var writer = new StringWriter();
        var original = Console.Out;
        Console.SetOut(writer);

        // Act
        processor.DisplayProfileNames(profiles);
        Console.SetOut(original);

        // Assert
        var output = writer.ToString();
        output.Should().Contain("Profiles:");
        output.Should().Contain("- Dev");
        output.Should().Contain("- Prod");
    }

    [Fact(DisplayName = "ShowVariables writes correct output for set and unset variables")]
    [Trait("Category", "Unit")]
    public void ShowVariables_WritesSetAndUnsetVariables()
    {
        // Arrange
        var processor = new OutputProcessor();
        var variables = new List<EnvironmentVariable>
        {
            new(new VariableName("FOO"), "123"),
            EnvironmentVariable.CreateEmpty(new VariableName("BAR"))
        };
        using var writer = new StringWriter();
        var original = Console.Out;
        Console.SetOut(writer);

        // Act
        processor.ShowVariables(variables);
        Console.SetOut(original);

        // Assert
        var output = writer.ToString();
        output.Should().Contain("FOO : 123");
        output.Should().Contain("BAR is not set");
    }

    [Fact(DisplayName = "ProcessNotification writes notification text")]
    [Trait("Category", "Unit")]
    public void ProcessNotification_WritesNotification()
    {
        // Arrange
        var processor = new OutputProcessor();
        var message = new Notification("Something went wrong.");
        using var writer = new StringWriter();
        var original = Console.Out;
        Console.SetOut(writer);

        // Act
        processor.ProcessNotification(message);
        Console.SetOut(original);

        // Assert
        var output = writer.ToString();
        output.Should().Contain("Something went wrong.");
    }

    [Fact(DisplayName = "DisplayHello writes figlet")]
    [Trait("Category", "Unit")]
    public void DisplayHello_WritesHeader()
    {
        // Arrange
        var processor = new OutputProcessor();
        using var writer = new StringWriter();
        var original = Console.Out;
        Console.SetOut(writer);

        // Act
        processor.DisplayHello();
        Console.SetOut(original);

        // Assert
        var output = writer.ToString();
        output.Should().NotBeNullOrWhiteSpace();
    }
}

#pragma warning restore CA1707 // Identifiers should not contain underscores
#pragma warning restore IDE0058 // Expression value is never used