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

    private static Option<string> CreateNameOption()
    {
        var option = new Option<string>("--name", ["-n"])
        {
            Required = true,
            Description = "Name of the profile"
        };
        return option;
    }

    private Command AddListCommand()
    {
        var profilesListCommand = new Command("profiles", "List available profiles");
        profilesListCommand.SetAction(_ => _envManager.ListProfiles());
        var profileCommand = new Command("profile")
        {
             CreateNameOption()
        };

        return profilesListCommand;
    }

    private Command AddProfileCommand()
    {
        var profileCommand = new Command("profile")
        {
            CreateNameOption()
        };
        profileCommand.Description = "Show values for a specific profile";
        profileCommand.SetAction(profileCommand =>
        {
            var name = profileCommand.GetValue<string>("--name");
            _envManager.ShowProfileValues(new ProfileName(name!));
        });
        return profileCommand;

    }

    private Command AddApplyCommand()
    {
        var applyCommand = new Command("apply")
        {
            CreateNameOption()
        };
        applyCommand.Description = "Apply a specific profile";
        applyCommand.SetAction(applyCommand =>
        {
            var name = applyCommand.GetValue<string>("--name");
            _envManager.ApplyProfile(new ProfileName(name!));
        });
        return applyCommand;
    }

    private Command AddVariablesCommand()
    {
        var variablesCommand = new Command("variables", "Show real environment variable values");
        variablesCommand.SetAction(_ => _envManager.ShowRealValues());
        return variablesCommand;
    }

    private Command AddHelloCommand()
    {
        var variablesCommand = new Command("hello", "Show info about the env switch cli");
        variablesCommand.SetAction(_ => _envManager.SayHello());
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
            AddHelloCommand()
        };

        _ = await rootCommand.Parse(args).InvokeAsync(cancellationToken: ct).ConfigureAwait(false);
    }

    private readonly IEnvManager _envManager;
}
