namespace RealArtists.Slack.Wire.Types {
  public class AttachmentField {
    /// <summary>
    /// Shown as a bold heading above the value text. It cannot contain markup
    ///  and will be escaped for you.
    /// </summary>
    public string Title { get; set; }

    /// <summary>
    /// The text value of the field. It may contain standard message markup
    ///  (see details below) and must be escaped as normal. May be multi-line.
    /// </summary>
    public string Value { get; set; }

    /// <summary>
    /// An optional flag indicating whether the value is short enough to be
    ///  displayed side-by-side with other values.
    /// </summary>
    public bool Short { get; set; }
  }
}
