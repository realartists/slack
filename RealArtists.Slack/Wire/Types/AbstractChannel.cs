namespace RealArtists.Slack.Wire.Types {
  using System;
  using System.Collections.Generic;

  public abstract class AbstractChannel {
    public string Id { get; set; }

    /// <summary>
    /// The name parameter indicates the name of the channel, without a leading hash sign.
    /// </summary>
    public string Name { get; set; }
    public DateTime Created { get; set; }

    /// <summary>
    /// creator is the user ID of the member that created this channel. 
    /// </summary>
    public string Creator { get; set; }

    /// <summary>
    /// is_archived will be true if the channel is archived.
    /// </summary>
    public bool IsArchived { get; set; }

    /// <summary>
    /// members is a list of user ids for all users in this channel. This includes any
    ///  disabled accounts that were in this channel when they were disabled.
    /// </summary>
    public IEnumerable<string> Members { get; set; }
    public SettableValue Topic { get; set; }
    public SettableValue Purpose { get; set; }

    /// <summary>
    /// last_read is the timestamp for the last message the calling user has read in this channel
    /// </summary>
    public DateTime? LastRead { get; set; }

    /// <summary>
    /// latest is the latest message in the channel.
    /// </summary>
    public Message Latest { get; set; }

    /// <summary>
    /// unread_count is a full count of visible messages that the calling user has yet to read.
    /// </summary>
    public int? UnreadCount { get; set; }

    /// <summary>
    /// unread_count_display is a count of messages that the calling user has yet to read that
   ///  matter to them (this means it excludes things like join/leave messages). 
    /// </summary>
    public int? UnreadCountDisplay { get; set; }
  }
}
