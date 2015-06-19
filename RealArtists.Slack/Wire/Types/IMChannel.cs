namespace RealArtists.Slack.Wire.Types {
  using System;

  public class IMChannel {
    public string Id { get; set; }
    public bool IsIm { get; set; }
    public string User { get; set; }
    public DateTime Created { get; set; }
    public bool IsUserDeleted { get; set; }
  }
}
