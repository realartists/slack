namespace RealArtists.Slack.Wire {
  using System.Collections.Generic;
  using Types;

  public class GroupsListResponse : SlackResponse {
    public IEnumerable<Group> Groups { get; set; }
  }
}
