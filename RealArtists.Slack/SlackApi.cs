namespace RealArtists.Slack {
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using System.Text;
  using System.Threading.Tasks;

  public class SlackApi : SlackBotApi {
    public SlackApi(string token)
      : base(token) { }
  }
}
