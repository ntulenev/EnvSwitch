using Abstractions;

namespace Infrastructure;

/// <summary>
/// Default implementation of <see cref="IEnvironmentProvider"/> that uses the system <see cref="Environment"/> class.
/// </summary>
public sealed class SystemEnvironmentProvider : IEnvironmentProvider
{
    /// <inheritdoc />
    public void SetVariable(string name, string value, EnvironmentVariableTarget target)
        => Environment.SetEnvironmentVariable(name, value, target);

    /// <inheritdoc />
    public string? GetVariable(string name, EnvironmentVariableTarget target)
        => Environment.GetEnvironmentVariable(name, target);
}