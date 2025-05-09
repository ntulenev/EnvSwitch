namespace Abstractions;

/// <summary>
/// Provides an abstraction layer for accessing and modifying environment variables.
/// </summary>
public interface IEnvironmentProvider
{
    /// <summary>
    /// Sets the value of an environment variable for the specified target.
    /// </summary>
    /// <param name="name">The name of the environment variable.</param>
    /// <param name="value">The value to assign to the environment variable.</param>
    /// <param name="target">The environment variable target (User or Machine).</param>
    void SetVariable(string name, string value, EnvironmentVariableTarget target);

    /// <summary>
    /// Gets the value of an environment variable from the specified target.
    /// </summary>
    /// <param name="name">The name of the environment variable.</param>
    /// <param name="target">The environment variable target (User or Machine).</param>
    /// <returns>The value of the environment variable if it exists; otherwise, <c>null</c>.</returns>
    string? GetVariable(string name, EnvironmentVariableTarget target);
}