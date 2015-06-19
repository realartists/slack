namespace RealArtists.Slack.Test {
  using System;
  using System.Linq;
  using System.Threading.Tasks;

  class Program {
    static void Main(string[] args) {
      DoStuff().Wait();
      Console.ReadLine();
    }

    public static async Task DoStuff() {
      using (var slack = new SlackApi("")) {
        var users = await slack.UsersList();
        var user = users.Members.Where(x => x.Name == "nick").Single();
        var channel = await slack.IMOpen(user.Id);
        var message = await slack.ChatPostMessage(channel.Channel.Id, "Sent from Nick's slick new slack library. <>&!");
      }
    }
  }
}
