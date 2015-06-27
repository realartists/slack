namespace RealArtists.Slack.Wire {
  using System.Collections.Generic;
  using RealArtists.Slack.Wire.Types;

  public class GroupsListResponse : SlackResponse {
    public IEnumerable<Group> Groups { get; set; }
  }
}
