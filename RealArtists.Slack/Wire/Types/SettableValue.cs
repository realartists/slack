namespace RealArtists.Slack.Wire.Types {
  using System;

  public class SettableValue {
    public string Value { get; set; }
    public string Creator { get; set; }
    public DateTime LastSet { get; set; }
  }
}
