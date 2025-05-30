using Abstractions;

using Models;

namespace Logic;
#pragma warning disable CA1303 // Do not pass literals as localized parameters
/// <summary>
/// Manager for environment profiles and variables.
/// </summary>
public sealed class EnvManager : IEnvManager
{

    /// <summary>
    /// Creates <see cref="EnvManager"/>.
    /// </summary>
    /// <param name="profileManager">An instance of <see cref="IProfileManager"/> used to manage profiles.</param>
    /// <param name="outputProcessor">An instance of <see cref="IOutputProcessor"/> responsible for processing output.</param>
    /// <param name="workstationManager">An instance of <see cref="IWorkstationManager"/> used to manage workstations.</param>
    public EnvManager(IProfileManager profileManager, IOutputProcessor outputProcessor, IWorkstationManager workstationManager)
    {
        ArgumentNullException.ThrowIfNull(profileManager);
        ArgumentNullException.ThrowIfNull(outputProcessor);
        ArgumentNullException.ThrowIfNull(workstationManager);

        _profileManager = profileManager;
        _outputProcessor = outputProcessor;
        _workstationManager = workstationManager;
    }

    /// <inheritdoc/>
    public void SayHello() => _outputProcessor.DisplayHello();

    /// <inheritdoc/>
    public void ListProfiles()
    {
        var names = _profileManager.GetProfileNames();
        _outputProcessor.DisplayProfileNames(names);
    }

    /// <inheritdoc/>
    public void ShowProfileValues(ProfileName name)
    {
        ArgumentNullException.ThrowIfNull(name);

        if (_profileManager.TryGetProfile(name, out var profile))
        {
            _outputProcessor.ShowVariables(profile.Variables);
        }
        else
        {
            _outputProcessor.ProcessNotification(new Notification($"Profile '{name.Value}' not found."));
        }
    }

    /// <inheritdoc/>
    public void ApplyProfile(ProfileName name)
    {
        ArgumentNullException.ThrowIfNull(name);

        if (_profileManager.TryGetProfile(name, out var profile))
        {
            _workstationManager.ApplyVariables(profile.Variables);
        }
        else
        {
            _outputProcessor.ProcessNotification(new Notification($"Profile '{name.Value}' not found."));
        }
        _outputProcessor.ApplyProfile(name);
    }

    /// <inheritdoc/>
    public void ShowRealValues()
    {
        var variables = _workstationManager.GetVariables(_profileManager.Variables);
        _outputProcessor.ShowVariables(variables);
    }

    private readonly IProfileManager _profileManager;
    private readonly IOutputProcessor _outputProcessor;
    private readonly IWorkstationManager _workstationManager;
}

