
namespace EnvSwitch.Logic;

/// <summary>
/// Application logic entry point
/// </summary>
#pragma warning disable CA1515 // Consider making public types internal
public interface IApplication
#pragma warning restore CA1515 // Consider making public types internal
{
    /// <summary>
    /// Runs the applications
    /// </summary>
    /// <param name="args">application args.</param>
    /// <param name="ct">Cancelation token.</param>
    public Task RunAsync(string[] args, CancellationToken ct);
}
