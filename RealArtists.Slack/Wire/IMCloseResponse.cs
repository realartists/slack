namespace RealArtists.Slack.Wire {
  public class IMCloseResponse : Response {
    public bool NoOp { get; set; }
    public bool AlreadyClosed { get; set; }
  }
}
