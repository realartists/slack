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

    protected static KeyValuePair<string, string> Pair(string key, string value) {
      return new KeyValuePair<string, string>(key, value);
    }

    protected async Task<T> MakeRequest<T>(string methodName, params KeyValuePair<string, string>[] args)
      where T : Response {
      var url = new Uri(_SlackApiRoot, string.Format("{0}?token={1}", methodName, _token));
      var request = new HttpRequestMessage(HttpMethod.Post, url);

      if (args != null && args.Length > 0) {
        request.Content = new FormUrlEncodedContent(args);
      }

      var response = await _httpClient.SendAsync(request);
      response.EnsureSuccessStatusCode();

      return await response.Content.ReadAsAsync<T>(_SlackJsonMediaTypeFormatters);
    }

    public Task<AuthTestResponse> AuthTest() {
      return MakeRequest<AuthTestResponse>("auth.test");
    }

    public Task<IMOpenResponse> IMOpen(string userId) {
      return MakeRequest<IMOpenResponse>("im.open", Pair("user", userId));
    }

    public Task<IMListResponse> IMList() {
      return MakeRequest<IMListResponse>("im.list");
    }

    public Task<IMCloseResponse> IMClose(string channelId) {
      return MakeRequest<IMCloseResponse>("im.close", Pair("channel", channelId));
    }

    public Task<UserInfoResponse> UserInfo(string userId) {
      return MakeRequest<UserInfoResponse>("user.info", Pair("user", userId));
    }

    public Task<UsersListResponse> UsersList() {
      return MakeRequest<UsersListResponse>("users.list");
    }

    public Task<ChatPostMessageResponse> ChatPostMessage(string channelId, string text) {
      return MakeRequest<ChatPostMessageResponse>("chat.postMessage",
        Pair("channel", channelId),
        Pair("text", EscapeMessageText(text)),
        Pair("as_user", true)
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
    private bool disposedValue = false; // To detect redundant calls

    protected virtual void Dispose(bool disposing) {
      if (!disposedValue) {
        if (disposing) {
          _httpClient.Dispose();
          _httpClient = null;
        }

        // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
        // TODO: set large fields to null.

        disposedValue = true;
      }
    }

    // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
    // ~SlackApi() {
    //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
    //   Dispose(false);
    // }

    // This code added to correctly implement the disposable pattern.
    public void Dispose() {
      // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
      Dispose(true);
      // TODO: uncomment the following line if the finalizer is overridden above.
      // GC.SuppressFinalize(this);
    }
    #endregion
  }
}
