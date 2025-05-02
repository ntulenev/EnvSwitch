using Models;

namespace Abstractions;

/// <summary>
/// Defines methods for managing environment variables within a workstation.
/// </summary>
public interface IWorkstationManager
{
    /// <summary>
    /// Retrieves a collection of environment variables based on the specified variable names.
    /// </summary>
    /// <param name="names">The names of the environment variables to retrieve.</param>
    /// <returns>A collection of <see cref="EnvironmentVariable"/> instances corresponding to the specified names.</returns>
    IEnumerable<EnvironmentVariable> GetVariables(IReadOnlySet<VariableName> names);

    /// <summary>
    /// Applies a collection of environment variables to the workstation.
    /// </summary>
    /// <param name="variables">A collection of <see cref="EnvironmentVariable"/> instances to apply.</param>
    void ApplyVariables(IReadOnlySet<EnvironmentVariable> variables);
}

