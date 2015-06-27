namespace RealArtists.Slack {
  using System;
  using System.Collections.Generic;
  using System.Net;
  using System.Net.Http;
  using System.Net.Http.Formatting;
  using System.Net.Http.Headers;
  using System.Text.RegularExpressions;
  using System.Threading.Tasks;
  using Newtonsoft.Json;
  using RealArtists.Slack.Wire.Types;
  using Wire;

  public class SlackApi : IDisposable {
    private static readonly string _Version = typeof(SlackApi).Assembly.GetName().Version.ToString();
    private static readonly Uri _SlackApiRoot = new Uri("https://slack.com/api/");
    private static readonly JsonSerializerSettings _SlackJsonSettings;
    private static readonly JsonMediaTypeFormatter[] _SlackJsonMediaTypeFormatters;

    static SlackApi() {
      _SlackJsonSettings = new JsonSerializerSettings() {
        ContractResolver = new SlackPropertyNamesContractResolver(),
        Formatting = Formatting.Indented,
        NullValueHandling = NullValueHandling.Ignore,
      };
      //_SlackJsonSettings.Converters.Add(new StringEnumConverter());
      _SlackJsonSettings.Converters.Add(new EpochDateTimeConverter());

      _SlackJsonMediaTypeFormatters = new[] { new JsonMediaTypeFormatter() {
        SerializerSettings = _SlackJsonSettings,
      }};
    }

    private readonly string _token;
    private HttpClient _httpClient;

    public SlackApi(string token) {
      _token = token;

      var handler = new HttpClientHandler() {
        AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip,
      };
      _httpClient = HttpClientFactory.Create(handler);

      var headers = _httpClient.DefaultRequestHeaders;
      headers.AcceptEncoding.Clear();
      headers.AcceptEncoding.ParseAdd("gzip");
      headers.AcceptEncoding.ParseAdd("deflate");

      headers.AcceptCharset.Clear();
      headers.AcceptCharset.ParseAdd("utf-8");

      headers.Accept.Clear();
      headers.Accept.ParseAdd("application/json");

      headers.UserAgent.Clear();
      headers.UserAgent.Add(new ProductInfoHeaderValue("RealArtist.Slack", _Version));
    }

    protected static KeyValuePair<string, string> Pair(string key, object value) {
      return Pair(key, value.ToString());
    }

    protected static KeyValuePair<string, string> Pair(string key, DateTime value) {
      throw new NotImplementedException();
    }

    protected static KeyValuePair<string, string> Pair(string key, string value) {
      return new KeyValuePair<string, string>(key, value);
    }

    protected async Task<T> MakeRequest<T>(string methodName, params KeyValuePair<string, string>[] args)
      where T : SlackResponse {
      var url = new Uri(_SlackApiRoot, string.Format("{0}?token={1}", methodName, _token));
      var request = new HttpRequestMessage(HttpMethod.Post, url);

      if (args != null && args.Length > 0) {
        request.Content = new FormUrlEncodedContent(args);
      }

      var response = await _httpClient.SendAsync(request);
      response.EnsureSuccessStatusCode();

      return await response.Content.ReadAsAsync<T>(_SlackJsonMediaTypeFormatters);
    }

    /// <summary>
    /// This method checks authentication and tells you who you are.
    /// </summary>
    /// <returns></returns>
    public Task<AuthTestResponse> AuthTest() {
      return MakeRequest<AuthTestResponse>("auth.test");
    }

    /// <summary>
    /// This method deletes a message from a channel.
    /// </summary>
    /// <param name="channelId">Channel containing the message to be deleted.</param>
    /// <param name="timestamp">Timestamp of the message to be deleted.</param>
    /// <returns></returns>
    public Task<ChatDeleteResponse> ChatDelete(string channelId, DateTime timestamp) {
      return MakeRequest<ChatDeleteResponse>("chat.delete",
        Pair("ts", timestamp),
        Pair("channel", channelId)
      );
    }

    /// <summary>
    /// This method posts a message to a channel.
    /// </summary>
    /// <param name="channelId">Channel to send message to. Can be a public channel, 
    /// private group or IM channel. Can be an encoded ID, or a name.</param>
    /// <param name="text">Text of the message to send.</param>
    /// <returns></returns>
    public Task<ChatPostMessageResponse> ChatPostMessage(string channelId, string text) {
      return MakeRequest<ChatPostMessageResponse>("chat.postMessage",
        Pair("channel", channelId),
        Pair("text", EscapeMessageText(text)),
        Pair("as_user", true)
      );
    }

    /// <summary>
    /// This method updates a message in a channel.
    /// </summary>
    /// <param name="channelId">Channel containing the message to be updated.</param>
    /// <param name="timestamp">Timestamp of the message to be updated.</param>
    /// <param name="text">New text for the message, using the default formatting rules.</param>
    /// <returns></returns>
    public Task<ChatUpdateResponse> ChatUpdate(string channelId, DateTime timestamp, string text) {
      return MakeRequest<ChatUpdateResponse>("chat.update",
        Pair("ts", timestamp),
        Pair("channel", channelId),
        Pair("text", EscapeMessageText(text))
      );
    }

    /// <summary>
    /// This method lists the custom emoji for a team.
    /// </summary>
    /// <returns></returns>
    public Task<SlackResponse> EmojiList() {
      return MakeRequest<SlackResponse>("emoji.list");
    }

    /// <summary>
    /// This method closes a private group.
    /// </summary>
    /// <param name="channelId">Group to close.</param>
    /// <returns></returns>
    public Task<GroupsCloseResponse> GroupsClose(string channelId) {
      return MakeRequest<GroupsCloseResponse>("groups.close", Pair("channel", channelId));
    }

    /// <summary>
    /// This method returns information about a private group.
    /// </summary>
    /// <param name="channelId">Group to get info on.</param>
    /// <returns></returns>
    public Task<GroupsInfoResponse> GroupsInfo(string channelId) {
      return MakeRequest<GroupsInfoResponse>("groups.info", Pair("channel", channelId));
    }

    /// <summary>
    /// This method returns a list of groups in the team that the caller is in and
    ///  archived groups that the caller was in. The list of (non-deactivated)
    ///  members in each group is also returned.
    /// </summary>
    /// <returns></returns>
    public Task<GroupsListResponse> GroupsList() {
      return GroupsList(false);
    }

    /// <summary>
    /// This method returns a list of groups in the team that the caller is in and
    ///  archived groups that the caller was in. The list of (non-deactivated)
    ///  members in each group is also returned.
    /// </summary>
    /// <param name="excludeArchived">Don't return archived groups.</param>
    /// <returns></returns>
    public Task<GroupsListResponse> GroupsList(bool excludeArchived) {
      return MakeRequest<GroupsListResponse>("groups.list",
        Pair("exclude_archived", excludeArchived ? 1 : 0));
    }

    /// <summary>
    /// This method moves the read cursor in a private group.
    /// 
    /// After making this call, the mark is saved to the database and broadcast via
    ///  the message server to all open connections for the calling user.
    ///
    /// Clients should try to avoid making this call too often.When needing to mark a
    ///  read position, a client should set a timer before making the call. In this way,
    ///  any further updates needed during the timeout will not generate extra calls (just
    ///  one per channel). This is useful for when reading scroll-back history, or
    ///  following a busy live channel. A timeout of 5 seconds is a good starting point.
    ///  Be sure to flush these calls on shutdown/logout.
    /// </summary>
    /// <param name="channelId">Group to set reading cursor in.</param>
    /// <param name="timestamp">Timestamp of the most recently seen message.</param>
    /// <returns></returns>
    public Task<SlackResponse> GroupsMark(string channelId, DateTime timestamp) {
      return MakeRequest<SlackResponse>("groups.mark",
        Pair("channel", channelId),
        Pair("ts", timestamp));
    }

    /// <summary>
    /// This method opens a private group.
    /// </summary>
    /// <param name="channelId">Group to open.</param>
    /// <returns></returns>
    public Task<GroupsOpenResponse> GroupsOpen(string channelId) {
      return MakeRequest<GroupsOpenResponse>("groups.open", Pair("channel", channelId));
    }

    /// <summary>
    /// This method is used to change the purpose of a private group. The calling user must be a member of the private group.
    /// </summary>
    /// <param name="channelId">Private group to set the purpose of</param>
    /// <param name="purpose">The new purpose</param>
    /// <returns></returns>
    public Task<GroupsSetPurposeResponse> GroupsSetPurpose(string channelId, string purpose) {
      return MakeRequest<GroupsSetPurposeResponse>("groups.setPurpose",
        Pair("channel", channelId),
        Pair("purpose", purpose));
    }

    /// <summary>
    /// This method is used to change the topic of a private group. The calling user must be a member of the private group.
    /// </summary>
    /// <param name="channelId"></param>
    /// <param name="topic"></param>
    /// <returns></returns>
    public Task<GroupsSetTopicResponse> GroupsSetTopic(string channelId, string topic) {
      return MakeRequest<GroupsSetTopicResponse>("groups.setTopic",
        Pair("channel", channelId),
        Pair("topic", topic));
    }

    /// <summary>
    /// This method closes a direct message channel.
    /// </summary>
    /// <param name="channelId">Direct message channel to close.</param>
    /// <returns></returns>
    public Task<IMCloseResponse> IMClose(string channelId) {
      return MakeRequest<IMCloseResponse>("im.close", Pair("channel", channelId));
    }

    //public Task<SlackResponse> IMHistory(string channel, DateTime? latest, DateTime? oldest, bool) {
    //  return MakeRequest<SlackResponse>("im.list");
    //}

    /// <summary>
    /// This method returns a list of all im channels that the user has.
    /// </summary>
    /// <returns></returns>
    public Task<IMListResponse> IMList() {
      return MakeRequest<IMListResponse>("im.list");
    }

    /// <summary>
    /// This method moves the read cursor in a direct message channel.
    /// 
    /// After making this call, the mark is saved to the database and broadcast via the
    ///  message server to all open connections for the calling user.
    ///
    /// Clients should try to avoid making this call too often.When needing to mark a read
    ///  position, a client should set a timer before making the call. In this way, any
    ///  further updates needed during the timeout will not generate extra calls (just
    ///  one per channel). This is useful for when reading scroll-back history, or following
    ///  a busy live channel. A timeout of 5 seconds is a good starting point. Be sure to
    ///  flush these calls on shutdown/logout.
    /// </summary>
    /// <param name="channelId">Direct message channel to set reading cursor in.</param>
    /// <param name="timestamp">Timestamp of the most recently seen message.</param>
    /// <returns></returns>
    public Task<SlackResponse> IMMark(string channelId, DateTime timestamp) {
      return MakeRequest<SlackResponse>("im.mark",
        Pair("channel", channelId),
        Pair("ts", timestamp));
    }

    /// <summary>
    /// This method opens a direct message channel with another member of your Slack team.
    /// </summary>
    /// <param name="userId">User to open a direct message channel with.</param>
    /// <returns></returns>
    public Task<IMOpenResponse> IMOpen(string userId) {
      return MakeRequest<IMOpenResponse>("im.open", Pair("user", userId));
    }

    /// <summary>
    /// This method lets you find out information about a user's presence.
    /// </summary>
    /// <param name="userId">User to get presence info on. Defaults to the authed user.</param>
    /// <returns></returns>
    public Task<UsersGetPresenceResponse> UsersGetPresence(string userId) {
      return MakeRequest<UsersGetPresenceResponse>("users.getPresence", Pair("user", userId));
    }

    /// <summary>
    /// This method returns information about a team member.
    /// </summary>
    /// <param name="userId">User to get info on.</param>
    /// <returns></returns>
    public Task<UsersInfoResponse> UsersInfo(string userId) {
      return MakeRequest<UsersInfoResponse>("users.info", Pair("user", userId));
    }

    /// <summary>
    /// This method returns a list of all users in the team. This includes deleted/deactivated users.
    /// </summary>
    /// <returns></returns>
    public Task<UsersListResponse> UsersList() {
      return MakeRequest<UsersListResponse>("users.list");
    }

    /// <summary>
    /// This method lets you set the calling user's manual presence.
    /// </summary>
    /// <param name="presence">Either auto or away.</param>
    /// <returns></returns>
    public Task<SlackResponse> UsersSetPresence(Presence presence) {
      return MakeRequest<SlackResponse>("users.setPresence",
        Pair("presence", presence.ToString().ToLowerInvariant())
      );
    }

    private static readonly Regex _TextReplaceRegex = new Regex("[&<>]", RegexOptions.CultureInvariant | RegexOptions.Compiled);
    protected string EscapeMessageText(string text) {
      return _TextReplaceRegex.Replace(text, m => {
        switch (m.Value) {
          case "&":
            return "&amp;";
          case "<":
            return "&lt;";
          case ">":
            return "&gt;";
        }
        throw new Exception("Unrecognized match value.");
      });
    }

    #region IDisposable Support
    private bool disposedValue = false;

    protected virtual void Dispose(bool disposing) {
      if (!disposedValue) {
        if (disposing) {
          _httpClient.Dispose();
          _httpClient = null;
        }

        disposedValue = true;
      }
    }
    public void Dispose() {
      Dispose(true);
    }
    #endregion
  }
}
