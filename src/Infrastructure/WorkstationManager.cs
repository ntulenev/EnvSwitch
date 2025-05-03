using Abstractions;

using Infrastructure.Configuration;

using Microsoft.Extensions.Options;

using Models;

namespace Infrastructure;

/// <summary>
/// Implements the <see cref="IWorkstationManager"/> interface for managing environment variables in a workstation.
/// </summary>
public sealed class WorkstationManager : IWorkstationManager
{
    /// <summary>
    /// Creates <see cref="WorkstationManager"/>.
    /// </summary>
    /// <param name="config"></param>
    public WorkstationManager(IOptions<WorkstationConfiguration> config)
    {
        ArgumentNullException.ThrowIfNull(config);

        _target = config.Value.Scope == EnvironmentScope.User
                              ? EnvironmentVariableTarget.User
                              : EnvironmentVariableTarget.Machine;
    }

    /// <summary>
    /// Applies a collection of environment variables to the workstation.
    /// </summary>
    /// <param name="variables">A collection of <see cref="EnvironmentVariable"/> instances to apply.</param>
    /// <exception cref="ArgumentNullException">Thrown when the <paramref name="variables"/> collection is null.</exception>
    /// <exception cref="ArgumentException">Thrown when the <paramref name="variables"/> collection is empty.</exception>
    /// <remarks>
    /// The method will throw an <see cref="ArgumentException"/> if no variables are provided in the <paramref name="variables"/> collection.
    /// </remarks>
    public void ApplyVariables(IReadOnlySet<EnvironmentVariable> variables)
    {
        ArgumentNullException.ThrowIfNull(variables);
        if (variables.Count == 0)
        {
            throw new ArgumentException("The collection of environment variables cannot be empty.", nameof(variables));
        }

        foreach (var variable in variables)
        {
            if (variable.IsSet)
            {
                Environment.SetEnvironmentVariable(variable.Name.Value, variable.Payload, _target);
            }
            else
            {
                Environment.SetEnvironmentVariable(variable.Name.Value, string.Empty, _target);
            }
        }
    }

    /// <summary>
    /// Retrieves a collection of environment variables based on the specified variable names.
    /// </summary>
    /// <param name="names">The set of <see cref="VariableName"/> instances representing the environment variable names to retrieve.</param>
    /// <returns>A collection of <see cref="EnvironmentVariable"/> instances corresponding to the specified names.</returns>
    /// <exception cref="ArgumentNullException">Thrown when the <paramref name="names"/> collection is null.</exception>
    /// <exception cref="ArgumentException">Thrown when the <paramref name="names"/> collection is empty.</exception>
    /// <remarks>
    /// The method will throw an <see cref="ArgumentException"/> if no variable names are provided in the <paramref name="names"/> collection.
    /// </remarks>
    public IEnumerable<EnvironmentVariable> GetVariables(IReadOnlySet<VariableName> names)
    {
        ArgumentNullException.ThrowIfNull(names);
        if (names.Count == 0)
        {
            throw new ArgumentException("The collection of variable names cannot be empty.", nameof(names));
        }

        var environmentVariables = new List<EnvironmentVariable>();

        foreach (var name in names)
        {
            var value = Environment.GetEnvironmentVariable(name.Value, _target);

            if (!string.IsNullOrEmpty(value))
            {
                environmentVariables.Add(new EnvironmentVariable(name, value));
            }
            else
            {
                environmentVariables.Add(EnvironmentVariable.CreateEmpty(name));
            }
        }

        return environmentVariables;
    }

    private readonly EnvironmentVariableTarget _target;
}

