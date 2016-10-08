namespace RealArtists.Slack.Wire.Types {
  using System.Runtime.Serialization;

  [DataContract]
  public enum Presence {
    // Default value when not assigned.
    None,

    [EnumMember(Value = "active")]
    Active,

    [EnumMember(Value = "auto")]
    Auto,

    [EnumMember(Value = "away")]
    Away,
  }
}
