﻿namespace RealArtists.Slack.Wire {
  public class IMCloseResponse : SlackResponse {
    public bool NoOp { get; set; }
    public bool AlreadyClosed { get; set; }
  }
}
