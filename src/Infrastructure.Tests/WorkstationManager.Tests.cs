using Abstractions;

using FluentAssertions;

using Infrastructure.Configuration;

using Microsoft.Extensions.Options;

using Models;

using Moq;

namespace Infrastructure.Tests;
#pragma warning disable CA1707 // Identifiers should not contain underscores
#pragma warning disable IDE0058 // Expression value is never used

public class WorkstationManagerTests
{
    [Fact(DisplayName = "Constructor initializes correctly with valid input")]
    [Trait("Category", "Unit")]
    public void Constructor_ValidInput_InitializesSuccessfully()
    {
        // Arrange
        var config = Options.Create(new WorkstationConfiguration { Scope = EnvironmentScope.User });
        var mockEnv = new Mock<IEnvironmentProvider>();

        // Act
        var ex = Record.Exception(() => _ = new WorkstationManager(config, mockEnv.Object));

        // Assert
        ex.Should().BeNull();
    }

    [Fact(DisplayName = "Constructor throws if config is null")]
    [Trait("Category", "Unit")]
    public void Constructor_NullConfig_ThrowsArgumentNullException()
    {
        // Arrange
        var mockEnv = new Mock<IEnvironmentProvider>();

        // Act
        var ex = Record.Exception(() => _ = new WorkstationManager(null!, mockEnv.Object));

        // Assert
        ex.Should().BeOfType<ArgumentNullException>();
    }

    [Fact(DisplayName = "Constructor throws if environment provider is null")]
    [Trait("Category", "Unit")]
    public void Constructor_NullEnvironmentProvider_ThrowsArgumentNullException()
    {
        // Arrange
        var config = Options.Create(new WorkstationConfiguration { Scope = EnvironmentScope.User });

        // Act
        var ex = Record.Exception(() => _ = new WorkstationManager(config, null!));

        // Assert
        ex.Should().BeOfType<ArgumentNullException>();
    }

    [Fact(DisplayName = "ApplyVariables sets variables with correct values")]
    [Trait("Category", "Unit")]
    public void ApplyVariables_SetsCorrectValues()
    {
        // Arrange
        var config = Options.Create(new WorkstationConfiguration { Scope = EnvironmentScope.User });
        var mockEnv = new Mock<IEnvironmentProvider>(MockBehavior.Strict);

        var setTest1Count = 0;
        var setTest2Count = 0;

        mockEnv.Setup(x => x.SetVariable("TEST_1", "value1", EnvironmentVariableTarget.User))
               .Callback(() => setTest1Count++);
        mockEnv.Setup(x => x.SetVariable("TEST_2", string.Empty, EnvironmentVariableTarget.User))
               .Callback(() => setTest2Count++);

        var variables = new HashSet<EnvironmentVariable>
            {
                new(new VariableName("TEST_1"), "value1"),
                EnvironmentVariable.CreateEmpty(new VariableName("TEST_2"))
            };

        var manager = new WorkstationManager(config, mockEnv.Object);

        // Act
        manager.ApplyVariables(variables);

        // Assert
        setTest1Count.Should().Be(1);
        setTest2Count.Should().Be(1);
    }

    [Fact(DisplayName = "ApplyVariables throws on null input")]
    [Trait("Category", "Unit")]
    public void ApplyVariables_Null_Throws()
    {
        // Arrange
        var config = Options.Create(new WorkstationConfiguration { Scope = EnvironmentScope.User });
        var mockEnv = new Mock<IEnvironmentProvider>(MockBehavior.Strict);
        var manager = new WorkstationManager(config, mockEnv.Object);

        // Act
        var ex = Record.Exception(() => manager.ApplyVariables(null!));

        // Assert
        ex.Should().BeOfType<ArgumentNullException>();
    }

    [Fact(DisplayName = "ApplyVariables throws on empty input")]
    [Trait("Category", "Unit")]
    public void ApplyVariables_Empty_Throws()
    {
        // Arrange
        var config = Options.Create(new WorkstationConfiguration { Scope = EnvironmentScope.User });
        var mockEnv = new Mock<IEnvironmentProvider>();
        var manager = new WorkstationManager(config, mockEnv.Object);

        // Act
        var ex = Record.Exception(() => manager.ApplyVariables(new HashSet<EnvironmentVariable>()));

        // Assert
        ex.Should().BeOfType<ArgumentException>();
    }

    [Fact(DisplayName = "GetVariables retrieves set and unset values")]
    [Trait("Category", "Unit")]
    public void GetVariables_ReturnsCorrectly()
    {
        // Arrange
        var config = Options.Create(new WorkstationConfiguration { Scope = EnvironmentScope.User });
        var mockEnv = new Mock<IEnvironmentProvider>(MockBehavior.Strict);
        var names = new HashSet<VariableName> { new("FOO"), new("BAR") };

        mockEnv.Setup(x => x.GetVariable("FOO", EnvironmentVariableTarget.User)).Returns("abc");
        mockEnv.Setup(x => x.GetVariable("BAR", EnvironmentVariableTarget.User)).Returns((string?)null);

        var manager = new WorkstationManager(config, mockEnv.Object);

        // Act
        var result = manager.GetVariables(names);

        // Assert
        result.Should().ContainSingle(v => v.Name.Value == "FOO" && v.Payload == "abc" && v.IsSet);
        result.Should().ContainSingle(v => v.Name.Value == "BAR" && !v.IsSet);
    }

    [Fact(DisplayName = "GetVariables throws on null input")]
    [Trait("Category", "Unit")]
    public void GetVariables_Null_Throws()
    {
        // Arrange
        var config = Options.Create(new WorkstationConfiguration { Scope = EnvironmentScope.User });
        var mockEnv = new Mock<IEnvironmentProvider>();
        var manager = new WorkstationManager(config, mockEnv.Object);

        // Act
        var ex = Record.Exception(() => manager.GetVariables(null!));

        // Assert
        ex.Should().BeOfType<ArgumentNullException>();
    }

    [Fact(DisplayName = "GetVariables throws on empty input")]
    [Trait("Category", "Unit")]
    public void GetVariables_Empty_Throws()
    {
        // Arrange
        var config = Options.Create(new WorkstationConfiguration { Scope = EnvironmentScope.User });
        var mockEnv = new Mock<IEnvironmentProvider>();
        var manager = new WorkstationManager(config, mockEnv.Object);

        // Act
        var ex = Record.Exception(() => manager.GetVariables(new HashSet<VariableName>()));

        // Assert
        ex.Should().BeOfType<ArgumentException>();
    }
}

#pragma warning restore CA1707 // Identifiers should not contain underscores
#pragma warning restore IDE0058 // Expression value is never used