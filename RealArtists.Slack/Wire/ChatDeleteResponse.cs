namespace RealArtists.Slack.Wire {
  using System;
  using Newtonsoft.Json;

  public class ChatDeleteResponse : SlackResponse {
    [JsonProperty(PropertyName = "ts")]
    public DateTime Timestamp { get; set; }
    public string Channel { get; set; }
  }
}
