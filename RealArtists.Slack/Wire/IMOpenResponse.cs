﻿namespace RealArtists.Slack.Wire {
  using Types;

  public class IMOpenResponse : SlackResponse {
    public ChannelId Channel { get; set; }
    public bool NoOp { get; set; }
    public bool AlreadyOpen { get; set; }
  }
}
