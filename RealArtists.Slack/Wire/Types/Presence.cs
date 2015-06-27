namespace RealArtists.Slack.Wire.Types {
  using System.Runtime.Serialization;

  [DataContract]
  public enum Presence {
    [EnumMember(Value = "active")]
    Active,

    [EnumMember(Value = "auto")]
    Auto,

    [EnumMember(Value = "away")]
    Away,
  }
}
