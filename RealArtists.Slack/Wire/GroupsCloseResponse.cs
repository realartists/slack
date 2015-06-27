namespace RealArtists.Slack.Wire {
  public class GroupsCloseResponse : SlackResponse {
    public bool NoOp { get; set; }
    public bool AlreadyClosed { get; set; }
  }
}
