namespace RealArtists.Slack.Wire {
  using System.Collections.Generic;
  using Newtonsoft.Json;
  using Newtonsoft.Json.Linq;

  public class SlackResponse {
    public bool Ok { get; set; }
    public string Error { get; set; }

    [JsonExtensionData]
    public IDictionary<string, JToken> _extensionData = new Dictionary<string, JToken>();
  }
}
