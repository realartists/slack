namespace RealArtists.Slack.Wire.Types {
  using System;

  public class IMChannel {
    public string Id { get; set; }
    public bool IsIm { get; set; }
    public string User { get; set; }
    public DateTime Created { get; set; }

    /// <summary>
    /// is_user_deleted will be true if the other user's account has been disabled.
    /// </summary>
    public bool IsUserDeleted { get; set; }
    
    /// <summary>
    /// is_open shows if the DM channel is open.
    /// </summary>
    public bool? IsOpen { get; set; }

    /// <summary>
    /// last_read is the timestamp for the last message the calling user has read in this channel.
    /// </summary>
    public DateTime? LastRead { get; set; }

    /// <summary>
    /// unread_count is a count of messages that the calling user has yet to read. 
    /// </summary>
    public int? UnreadCount { get; set; }

    /// <summary>
    /// latest is the latest message in the channel
    /// </summary>
    public Message Latest { get; set; }
  }
}
