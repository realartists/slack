namespace RealArtists.Slack.Wire.Types {
  using System;
  using System.Collections.Generic;

  public abstract class AbstractChannel {
    public string Id { get; set; }
    public string Name { get; set; }
    public DateTime Created { get; set; }
    public string Creator { get; set; }
    public bool IsArchived { get; set; }
    public IEnumerable<string> Members { get; set; }
    public SettableValue Topic { get; set; }
    public SettableValue Purpose { get; set; }
    public DateTime LastRead { get; set; }
    public IMChannel Latest { get; set; }
    public int UnreadCount { get; set; }
    public int UnreadCountDisplay { get; set; }
  }
}
