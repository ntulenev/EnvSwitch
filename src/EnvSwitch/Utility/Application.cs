using System.CommandLine.NamingConventionBinder;
using System.CommandLine;
using Abstractions;
using Models;

namespace EnvSwitch.Utility;

/// <summary>
/// Entry point of the app logic
/// </summary>
#pragma warning disable CA1515 // Consider making public types internal
public sealed class Application : IApplication
#pragma warning restore CA1515 // Consider making public types internal
{

    /// <summary>
    /// Creates application
    /// </summary>
    /// <param name="manager">Environment manager</param>
    public Application(IEnvManager manager)
    {
        ArgumentNullException.ThrowIfNull(manager);
        _envManager = manager;
    }

    private Command AddListCommand()
    {
        var profilesListCommand = new Command("profiles", "List available profiles")
        {
            Handler = CommandHandler.Create(() => _envManager.ListProfiles())
        };
        var profileCommand = new Command("profile")
        {
            new Option<string>("--name", "Name of the profile to apply")
            {
                IsRequired = true
            }
        };

        return profilesListCommand;
    }

    private Command AddProfileCommand()
    {
        var profileCommand = new Command("profile")
        {
            new Option<string>("--name", "Name of the profile to apply")
            {
                IsRequired = true
            }
        };
        profileCommand.Description = "Show values for a specific profile";
        profileCommand.Handler = CommandHandler.Create<string>(name => _envManager.ShowProfileValues(new ProfileName(name)));
        return profileCommand;

    }

    private Command AddApplyCommand()
    {
        var applyCommand = new Command("apply")
        {
            new Option<string>("--name", "Name of the profile to apply")
            {
                IsRequired = true
            }
        };
        applyCommand.Description = "Apply a specific profile";
        applyCommand.Handler = CommandHandler.Create<string>((name) => _envManager.ApplyProfile(new ProfileName(name)));
        return applyCommand;
    }

    private Command AddVariablesCommand()
    {
        var variablesCommand = new Command("variables", "Show real environment variable values")
        {
            Handler = CommandHandler.Create(() => _envManager.ShowRealValues())
        };
        return variablesCommand;
    }

    /// <inheritdoc/>
    /// <exception cref="OperationCanceledException">
    /// Throws on cancelation token is canceled.
    /// </exception>
    public async Task RunAsync(string[] args, CancellationToken ct)
    {
        ct.ThrowIfCancellationRequested();
        var rootCommand = new RootCommand
        {
            AddListCommand(),
            AddProfileCommand(),
            AddApplyCommand(),
            AddVariablesCommand(),
        };
        _ = await rootCommand.InvokeAsync(args);
    }

    private readonly IEnvManager _envManager;
}
