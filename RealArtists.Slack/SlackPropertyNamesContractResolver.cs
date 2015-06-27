namespace RealArtists.Slack {
  using Newtonsoft.Json.Serialization;

  public class SlackPropertyNamesContractResolver : DefaultContractResolver {
    public SlackPropertyNamesContractResolver()
      : base() {
    }

    protected override string ResolvePropertyName(string propertyName) {
      return SnakeCaseUtils.ToSnakeCase(propertyName);
    }
  }
}
