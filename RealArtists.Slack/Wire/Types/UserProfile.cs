namespace RealArtists.Slack.Wire.Types {
  /// <summary>
  /// The profile hash contains as much profile information as the user has
  ///  supplied - only the image_* fields are guaranteed to be included.
  ///  Data that has not been supplied may not be present at all, may be
  ///  null or may contain the empty string ("").
  /// 
  /// The image_* fields will always contain https URLs to square, web-viewable
  ///  images (GIFs, JPEGs or PNGs).
  /// </summary>
  public class UserProfile {
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string RealName { get; set; }
    public string Email { get; set; }
    public string Skype { get; set; }
    public string Phone { get; set; }
    public string Image24 { get; set; }
    public string Image32 { get; set; }
    public string Image48 { get; set; }
    public string Image72 { get; set; }
    public string Image192 { get; set; }
  }
}
