namespace RealArtists.Slack.Wire {
  using System.Collections.Generic;
  using Types;

  public class ChannelsListResponse : SlackResponse {
    public IEnumerable<Channel> Channels { get; set; }
  }
}
