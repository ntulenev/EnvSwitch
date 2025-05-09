using Abstractions;

using FluentAssertions;

using Models;

using Moq;

namespace Logic.Tests;
#pragma warning disable CA1707 // Identifiers should not contain underscores
#pragma warning disable IDE0058 // Expression value is never used

public class EnvManagerTests
{
    [Fact(DisplayName = "SayHello calls DisplayHello once")]
    [Trait("Category", "Unit")]
    public void SayHello_Calls_DisplayHello()
    {
        // Arrange
        var profileManager = new Mock<IProfileManager>(MockBehavior.Strict);
        var output = new Mock<IOutputProcessor>(MockBehavior.Strict);
        var workstation = new Mock<IWorkstationManager>(MockBehavior.Strict);

        var helloCallCount = 0;
        output.Setup(x => x.DisplayHello()).Callback(() => helloCallCount++);

        var manager = new EnvManager(profileManager.Object, output.Object, workstation.Object);

        // Act
        manager.SayHello();

        // Assert
        helloCallCount.Should().Be(1);
    }

    [Fact(DisplayName = "ListProfiles calls DisplayProfileNames with names from IProfileManager")]
    [Trait("Category", "Unit")]
    public void ListProfiles_Calls_DisplayProfileNames()
    {
        // Arrange
        var names = new HashSet<ProfileName> { new("Dev"), new("Prod") };

        var profileManager = new Mock<IProfileManager>(MockBehavior.Strict);
        profileManager.Setup(x => x.GetProfileNames()).Returns(names);

        var output = new Mock<IOutputProcessor>(MockBehavior.Strict);
        var callCount = 0;
        output.Setup(x => x.DisplayProfileNames(names)).Callback(() => callCount++);

        var workstation = new Mock<IWorkstationManager>(MockBehavior.Strict);

        var manager = new EnvManager(profileManager.Object, output.Object, workstation.Object);

        // Act
        manager.ListProfiles();

        // Assert
        callCount.Should().Be(1);
    }

    [Fact(DisplayName = "ShowProfileValues shows variables if profile exists")]
    [Trait("Category", "Unit")]
    public void ShowProfileValues_ProfileExists_ShowsVariables()
    {
        // Arrange
        var name = new ProfileName("Dev");
        var variables = new HashSet<EnvironmentVariable>
        {
            new(new VariableName("KEY"), "value")
        };
        var profile = new EnvironmentProfile(name, variables);

        var profileManager = new Mock<IProfileManager>(MockBehavior.Strict);
        profileManager.Setup(x => x.TryGetProfile(name, out profile)).Returns(true);

        var output = new Mock<IOutputProcessor>(MockBehavior.Strict);
        var callCount = 0;
        output.Setup(x => x.ShowVariables(variables)).Callback(() => callCount++);

        var workstation = new Mock<IWorkstationManager>(MockBehavior.Strict);

        var manager = new EnvManager(profileManager.Object, output.Object, workstation.Object);

        // Act
        manager.ShowProfileValues(name);

        // Assert
        callCount.Should().Be(1);
    }

    [Fact(DisplayName = "ShowProfileValues displays error if profile not found")]
    [Trait("Category", "Unit")]
    public void ShowProfileValues_ProfileMissing_ShowsNotification()
    {
        // Arrange
        var name = new ProfileName("Missing");
        EnvironmentProfile? profile;

        var profileManager = new Mock<IProfileManager>(MockBehavior.Strict);
        profileManager.Setup(x => x.TryGetProfile(name, out profile)).Returns(false);

        var output = new Mock<IOutputProcessor>(MockBehavior.Strict);
        var callCount = 0;
        output.Setup(x => x.ProcessNotification(It.Is<Notification>(n => n.Text.Contains("Missing"))))
            .Callback(() => callCount++);

        var workstation = new Mock<IWorkstationManager>(MockBehavior.Strict);

        var manager = new EnvManager(profileManager.Object, output.Object, workstation.Object);

        // Act
        manager.ShowProfileValues(name);

        // Assert
        callCount.Should().Be(1);
    }

    [Fact(DisplayName = "ApplyProfile applies and reports profile if it exists")]
    [Trait("Category", "Unit")]
    public void ApplyProfile_ProfileExists_AppliesProfile()
    {
        // Arrange
        var name = new ProfileName("Dev");
        var variables = new HashSet<EnvironmentVariable> { new(new VariableName("X"), "1") };
        var profile = new EnvironmentProfile(name, variables);

        var profileManager = new Mock<IProfileManager>(MockBehavior.Strict);
        profileManager.Setup(x => x.TryGetProfile(name, out profile)).Returns(true);

        var output = new Mock<IOutputProcessor>(MockBehavior.Strict);
        var applyCount = 0;
        output.Setup(x => x.ApplyProfile(name)).Callback(() => applyCount++);

        var workstation = new Mock<IWorkstationManager>(MockBehavior.Strict);
        workstation.Setup(x => x.ApplyVariables(variables));

        var manager = new EnvManager(profileManager.Object, output.Object, workstation.Object);

        // Act
        manager.ApplyProfile(name);

        // Assert
        applyCount.Should().Be(1);
    }

    [Fact(DisplayName = "ApplyProfile shows error if profile not found")]
    [Trait("Category", "Unit")]
    public void ApplyProfile_ProfileMissing_ShowsNotification()
    {
        // Arrange
        var name = new ProfileName("Ghost");
        EnvironmentProfile? profile;

        var profileManager = new Mock<IProfileManager>(MockBehavior.Strict);
        profileManager.Setup(x => x.TryGetProfile(name, out profile)).Returns(false);

        var output = new Mock<IOutputProcessor>(MockBehavior.Strict);
        var callCount = 0;
        output.Setup(x => x.ApplyProfile(name));
        output.Setup(x => x.ProcessNotification(It.Is<Notification>(n => n.Text.Contains("Ghost"))))
            .Callback(() => callCount++);

        var workstation = new Mock<IWorkstationManager>(MockBehavior.Strict);

        var manager = new EnvManager(profileManager.Object, output.Object, workstation.Object);

        // Act
        manager.ApplyProfile(name);

        // Assert
        callCount.Should().Be(1);
    }

    [Fact(DisplayName = "ShowRealValues fetches variables from workstation and displays them")]
    [Trait("Category", "Unit")]
    public void ShowRealValues_GetsAndDisplaysVariables()
    {
        // Arrange
        var variableNames = new HashSet<VariableName> { new("A"), new("B") };
        var envVars = new List<EnvironmentVariable>
        {
            new(new VariableName("A"), "1"),
            new(new VariableName("B"), "2")
        };

        var profileManager = new Mock<IProfileManager>(MockBehavior.Strict);
        profileManager.Setup(x => x.Variables).Returns(variableNames);

        var workstation = new Mock<IWorkstationManager>(MockBehavior.Strict);
        workstation.Setup(x => x.GetVariables(variableNames)).Returns(envVars);

        var callCount = 0;
        var output = new Mock<IOutputProcessor>(MockBehavior.Strict);
        output.Setup(x => x.ShowVariables(envVars)).Callback(() => callCount++);

        var manager = new EnvManager(profileManager.Object, output.Object, workstation.Object);

        // Act
        manager.ShowRealValues();

        // Assert
        callCount.Should().Be(1);
    }
}

#pragma warning restore CA1707 // Identifiers should not contain underscores
#pragma warning restore IDE0058 // Expression value is never used