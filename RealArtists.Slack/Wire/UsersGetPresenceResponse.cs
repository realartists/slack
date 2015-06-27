namespace RealArtists.Slack.Wire {
  using System;
  using Types;

  public class UsersGetPresenceResponse : SlackResponse {
    /// <summary>
    /// Indicates the user's overall presence, it will either be active or away.
    /// </summary>
    public Presence Presence { get; set; }

    /// <summary>
    /// Will be true if they have a client currently connected to Slack. auto_away
    /// will be true if our servers haven't detected any activity from the user in the last 30 minutes.
    /// </summary>
    public bool? Online { get; set; }
    public bool? AutoAway { get; set; }

    /// <summary>
    /// Will be true if the user has manually set their presence to away.
    /// </summary>
    public bool? ManualAway { get; set; }

    /// <summary>
    /// Gives a count of total connections.
    /// </summary>
    public int? ConnectionCount { get; set; }

    /// <summary>
    /// Indicates the last activity seen by our servers. If a user has no connected clients
    /// then this property will be absent.
    /// </summary>
    public DateTime? LastActivity { get; set; }
  }
}
