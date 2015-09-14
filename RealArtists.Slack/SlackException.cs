namespace RealArtists.Slack {
  using System;
  using System.Runtime.Serialization;
  using System.Security.Permissions;
  using Newtonsoft.Json;
  using Wire;

  class SlackException : Exception {
    public SlackException() {
    }

    public SlackException(string message) : base(message) {
    }

    public SlackException(string message, Exception innerException) : base(message, innerException) {
    }

    protected SlackException(SerializationInfo info, StreamingContext context) : base(info, context) {
      Response = JsonConvert.DeserializeObject<SlackResponse>(info.GetString("SlackResponse"), SlackBotApi.JsonSettings);
    }

    [SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
    public override void GetObjectData(SerializationInfo info, StreamingContext context) {
      base.GetObjectData(info, context);
      info.AddValue("SlackResponse", JsonConvert.SerializeObject(Response, SlackBotApi.JsonSettings));
    }

    public SlackResponse Response { get; set; }
  }
}
