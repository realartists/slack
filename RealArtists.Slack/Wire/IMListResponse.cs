namespace RealArtists.Slack.Wire {
  using System.Collections.Generic;
  using Newtonsoft.Json;
  using Types;

  public class IMListResponse : Response {
    [JsonProperty(PropertyName = "Ims")]
    public IEnumerable<IMChannel> Channels { get; set; }
  }
}
