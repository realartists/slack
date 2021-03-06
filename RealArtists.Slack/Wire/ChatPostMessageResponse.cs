﻿namespace RealArtists.Slack.Wire {
  using System;
  using Newtonsoft.Json;
  using Types;

  public class ChatPostMessageResponse : SlackResponse {
    [JsonProperty(PropertyName = "ts")]
    public DateTime Timestamp { get; set; }
    public string Channel { get; set; }
    public Message Message { get; set; }
  }
}
