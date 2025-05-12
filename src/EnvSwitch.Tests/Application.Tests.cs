using Abstractions;

using EnvSwitch.Utility;

using FluentAssertions;

using Models;

using Moq;

namespace EnvSwitch.Tests;
#pragma warning disable CA1707 // Identifiers should not contain underscores
#pragma warning disable IDE0058 // Expression value is never used
#pragma warning disable IDE1006 // Naming Styles

public class ApplicationTests
{
    [Fact(DisplayName = "Constructor throws if IEnvManager is null")]
    [Trait("Category", "Unit")]
    public void Constructor_NullManager_ThrowsArgumentNullException()
    {
        // Act
        var ex = Record.Exception(() => _ = new Application(null!));

        // Assert
        ex.Should().BeOfType<ArgumentNullException>();
    }

    [Fact(DisplayName = "Constructor creates Application with valid manager")]
    [Trait("Category", "Unit")]
    public void Constructor_ValidManager_CreatesSuccessfully()
    {
        // Arrange
        var mockManager = new Mock<IEnvManager>();

        // Act
        var ex = Record.Exception(() => _ = new Application(mockManager.Object));

        // Assert
        ex.Should().BeNull();
    }

    [Fact(DisplayName = "RunAsync invokes SayHello on 'hello' command")]
    [Trait("Category", "Unit")]
    public async Task RunAsync_HelloCommand_CallsSayHello()
    {
        // Arrange
        var envMock = new Mock<IEnvManager>(MockBehavior.Strict);
        var callCount = 0;
        envMock.Setup(m => m.SayHello()).Callback(() => callCount++);
        using var cts = new CancellationTokenSource();

        var app = new Application(envMock.Object);

        // Act
        await app.RunAsync(["hello"], cts.Token);

        // Assert
        callCount.Should().Be(1);
    }

    [Fact(DisplayName = "RunAsync invokes ListProfiles on 'profiles' command")]
    [Trait("Category", "Unit")]
    public async Task RunAsync_ProfilesCommand_CallsListProfiles()
    {
        var envMock = new Mock<IEnvManager>(MockBehavior.Strict);
        var count = 0;
        envMock.Setup(m => m.ListProfiles()).Callback(() => count++);
        using var cts = new CancellationTokenSource();

        var app = new Application(envMock.Object);

        await app.RunAsync(["profiles"], cts.Token);

        count.Should().Be(1);
    }

    [Fact(DisplayName = "RunAsync invokes ShowRealValues on 'variables' command")]
    [Trait("Category", "Unit")]
    public async Task RunAsync_VariablesCommand_CallsShowRealValues()
    {
        var envMock = new Mock<IEnvManager>(MockBehavior.Strict);
        var count = 0;
        envMock.Setup(m => m.ShowRealValues()).Callback(() => count++);
        using var cts = new CancellationTokenSource();

        var app = new Application(envMock.Object);

        await app.RunAsync(["variables"], cts.Token);

        count.Should().Be(1);
    }

    [Fact(DisplayName = "RunAsync invokes ShowProfileValues on 'profile' command")]
    [Trait("Category", "Unit")]
    public async Task RunAsync_ProfileCommand_CallsShowProfileValues()
    {
        var envMock = new Mock<IEnvManager>(MockBehavior.Strict);
        var capturedName = string.Empty;
        envMock.Setup(m => m.ShowProfileValues(It.IsAny<ProfileName>()))
               .Callback<ProfileName>(name => capturedName = name.Value);
        using var cts = new CancellationTokenSource();

        var app = new Application(envMock.Object);

        await app.RunAsync(["profile", "--name", "Dev"], cts.Token);

        capturedName.Should().Be("Dev");
    }

    [Fact(DisplayName = "RunAsync invokes ApplyProfile on 'apply' command")]
    [Trait("Category", "Unit")]
    public async Task RunAsync_ApplyCommand_CallsApplyProfile()
    {
        var envMock = new Mock<IEnvManager>(MockBehavior.Strict);
        var capturedName = string.Empty;
        envMock.Setup(m => m.ApplyProfile(It.IsAny<ProfileName>()))
               .Callback<ProfileName>(name => capturedName = name.Value);
        using var cts = new CancellationTokenSource();

        var app = new Application(envMock.Object);

        await app.RunAsync(["apply", "--name", "Prod"], cts.Token);

        capturedName.Should().Be("Prod");
    }
}

#pragma warning restore CA1707 // Identifiers should not contain underscores
#pragma warning restore IDE0058 // Expression value is never used
#pragma warning restore IDE1006 // Naming Styles
