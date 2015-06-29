namespace RealArtists.Slack.Wire.Types {
  using System.Runtime.Serialization;

  [DataContract]
  public enum ParseMode {
    /// <summary>
    /// If you don't want Slack to perform any processing on your message.
    /// </summary>
    [EnumMember(Value = "none")]
    None,

    /// <summary>
    /// If you want Slack to treat your message as completely unformatted,
    ///  pass parse=full. This will ignore any markup formatting you added
    ///  to your message.
    /// </summary>
    [EnumMember(Value = "full")]
    Full,
  }
}
