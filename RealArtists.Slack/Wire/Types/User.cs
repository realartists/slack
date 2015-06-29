namespace RealArtists.Slack.Wire.Types {
  using Newtonsoft.Json;

  public class User {
    public string Id { get; set; }

    /// <summary>
    /// The name parameter indicates the username for this user, without a leading @ sign.
    /// </summary>
    public string Name { get; set; }
    public bool Deleted { get; set; }

    /// <summary>
    /// The color field is used in some clients to display a colored username.
    /// </summary>
    public string Color { get; set; }
    public UserProfile Profile { get; set; }
    public bool IsAdmin { get; set; }
    public bool IsOwner { get; set; }
    public bool IsPrimaryOwner { get; set; }
    public bool IsRestricted { get; set; }
    public bool IsUltraRestricted { get; set; }

    /// <summary>
    /// The has_2fa field describes whether two-step verification is enabled for this user.
    ///  This field will always be displayed if you are looking at your own user information.
    ///  If you are looking at another user's information this field will only be displayed
    ///  if you are team admin or owner.
    /// </summary>
    [JsonProperty(PropertyName = "Has2fa")]
    public bool HasTwoFactorAuth { get; set; }
    public bool HasFiles { get; set; }
  }
}
