using System.CommandLine.NamingConventionBinder;
using System.CommandLine;
using Abstractions;

namespace EnvSwitch.Logic;

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

    /// <inheritdoc/>
    /// <exception cref="OperationCanceledException">
    /// Throws on cancelation token is canceled.
    /// </exception>
    public async Task RunAsync(string[] args, CancellationToken ct)
    {
        ct.ThrowIfCancellationRequested();
        var profilesListCommand = new Command("profiles", "List available profiles")
        {
            Handler = CommandHandler.Create(() => _envManager.ListProfiles())
        };
        var profileCommand = new Command("profile", "Show values for a specific profile")
        {
            Handler = CommandHandler.Create<string>(profile => _envManager.ShowProfileValues(new Models.ProfileName(profile)))
        };
        profileCommand.AddOption(new Option<string>("--name", "Name of the profile")
        {
            IsRequired = true
        });
        var applyCommand = new Command("apply", "Apply a specific profile")
        {
            Handler = CommandHandler.Create<string>(profile => _envManager.ApplyProfile(new Models.ProfileName(profile)))
        };
        applyCommand.AddOption(new Option<string>("--name", "Name of the profile to apply")
        {
            IsRequired = true
        });
        var variablesCommand = new Command("variables", "Show real environment variable values")
        {
            Handler = CommandHandler.Create(() => _envManager.ShowRealValues())
        };
        var rootCommand = new RootCommand
        {
            profilesListCommand,
            profileCommand,
            applyCommand,
            variablesCommand,
        };
        _ = await rootCommand.InvokeAsync(args);
    }

    private readonly IEnvManager _envManager;
}
