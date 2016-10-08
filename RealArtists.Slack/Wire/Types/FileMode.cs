namespace RealArtists.Slack.Wire.Types {
  using System.Runtime.Serialization;

  [DataContract]
  public enum FileMode {
    // Default value when not assigned.
    None,

    [EnumMember(Value = "hosted")]
    Hosted,

    [EnumMember(Value = "external")]
    External,

    [EnumMember(Value = "snippet")]
    Snippet,

    [EnumMember(Value = "post")]
    Post,
  }
}
