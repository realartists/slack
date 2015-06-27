namespace RealArtists.Slack.Wire {
  using System.Collections.Generic;
  using Types;

  public class UsersListResponse : SlackResponse {
    public IEnumerable<User> Members { get; set; }
  }
}
