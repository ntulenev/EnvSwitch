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
    /// Initializes a new instance of the <see cref="WorkstationManager"/> class with the specified configuration and environment provider.
    /// </summary>
    /// <param name="config">
    /// An <see cref="IOptions{TOptions}"/> containing the <see cref="WorkstationConfiguration"/>
    /// that determines the environment variable scope (user or machine).
    /// </param>
    /// <param name="environment">
    /// An implementation of <see cref="IEnvironmentProvider"/> used to interact with environment variables in a testable and decoupled way.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// Thrown if <paramref name="config"/> or <paramref name="environment"/> is <c>null</c>.
    /// </exception>
    public WorkstationManager(IOptions<WorkstationConfiguration> config, IEnvironmentProvider environment)
    {
        ArgumentNullException.ThrowIfNull(config);
        ArgumentNullException.ThrowIfNull(environment);

        _target = config.Value.Scope == EnvironmentScope.User
                              ? EnvironmentVariableTarget.User
                              : EnvironmentVariableTarget.Machine;
        _environment = environment;
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
                _environment.SetVariable(variable.Name.Value, variable.Payload, _target);
            }
            else
            {
                _environment.SetVariable(variable.Name.Value, string.Empty, _target);
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
            var value = _environment.GetVariable(name.Value, _target);

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
    private readonly IEnvironmentProvider _environment;
}

