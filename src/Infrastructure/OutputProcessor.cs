using Abstractions;

using Models;

namespace Infrastructure;

/// <summary>
/// Displays output data.
/// </summary>
public sealed class OutputProcessor : IOutputProcessor
{
    /// <inheritdoc/>
    public void ApplyProfile(ProfileName profileName)
    {
        ArgumentNullException.ThrowIfNull(profileName);
        Console.Write("Profile '");
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.Write(profileName.Value);
        Console.ResetColor();
        Console.WriteLine("' applied successfully.");
    }

    /// <inheritdoc/>
    public void DisplayProfileNames(IEnumerable<ProfileName> profiles)
    {
        ArgumentNullException.ThrowIfNull(profiles);

        Console.WriteLine("Profiles:");
        Console.ForegroundColor = ConsoleColor.Green;
        foreach (var profileName in profiles)
        {
            Console.WriteLine($"- {profileName.Value}");
        }
        Console.ResetColor();
    }

    /// <inheritdoc/>
    public void ShowVariables(IEnumerable<EnvironmentVariable> variables)
    {
        ArgumentNullException.ThrowIfNull(variables);

        foreach (var profileVar in variables)
        {
            if (profileVar.IsSet)
            {
                Console.Write($"{profileVar.Name.Value} : ");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine(profileVar.Payload);
                Console.ResetColor();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write(profileVar.Name.Value);
                Console.WriteLine(" is not set");
                Console.ResetColor();
            }
        }
    }

    /// <inheritdoc/>
    public void ProcessNotification(Notification message)
    {
        ArgumentNullException.ThrowIfNull(message);
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine(message.Text);
        Console.ResetColor();
    }
}
