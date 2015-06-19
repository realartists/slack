namespace RealArtists.Slack.Wire.Types {
  using System.Collections.Generic;
  using Newtonsoft.Json;
  using Newtonsoft.Json.Linq;

  public class Message {
    public Message() {
      _extraData = new Dictionary<string, JToken>();
    }

    [JsonExtensionData]
    public IDictionary<string, JToken> _extraData { get; set; }
  }
}
