namespace RealArtists.Slack.Wire {
  using System.Collections.Generic;

  public class EmojiListResponse : SlackResponse {
    public IDictionary<string, string> Emoji { get; set; }
  }
}
