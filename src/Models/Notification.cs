namespace Models;

/// <summary>
/// Represents a notification with a text message.
/// </summary>
public sealed class Notification
{
    /// <summary>
    /// Gets the text message of the notification.
    /// </summary>
    public string Text { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="Notification"/> class with the specified text message.
    /// </summary>
    /// <param name="text">The text message of the notification.</param>
    /// <exception cref="ArgumentException">Thrown when the <paramref name="text"/> is null or consists only of white-space characters.</exception>
    public Notification(string text)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(text);

        Text = text;
    }
}