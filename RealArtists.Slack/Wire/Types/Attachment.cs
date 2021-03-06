﻿namespace RealArtists.Slack.Wire.Types {
  using System.Collections.Generic;

  public class Attachment {
    /// <summary>
    /// A plain-text summary of the attachment. This text will be used in clients that
    ///  don't show formatted text (eg. IRC, mobile notifications) and should not
    ///  contain any markup.
    /// </summary>
    public string Fallback { get; set; }

    /// <summary>
    /// An optional value that can either be one of good, warning, danger, or any
    ///  hex color code (eg. #439FE0). This value is used to color the border
    ///  along the left side of the message attachment.
    /// </summary>
    public string Color { get; set; }

    /// <summary>
    /// This is optional text that appears above the message attachment block.
    /// </summary>
    public string Pretext { get; set; }

    /// <summary>
    /// Small text used to display the author's name.
    /// </summary>
    public string AuthorName { get; set; }

    /// <summary>
    /// Small text used to display the author's name.
    /// </summary>
    public string AuthorLink { get; set; }

    /// <summary>
    /// A valid URL that displays a small 16x16px image to the left of the author_name text. 
    /// Will only work if author_name is present.
    /// </summary>
    public string AuthorIcon { get; set; }

    /// <summary>
    /// The title is displayed as larger, bold text near the top of a message attachment.
    /// </summary>
    public string Title { get; set; }

    /// <summary>
    /// By passing a valid URL in the title_link parameter (optional), the title text 
    /// will be hyperlinked.
    /// </summary>
    public string TitleLink { get; set; }

    /// <summary>
    /// This is the main text in a message attachment, and can contain standard message
    ///  markup (see details below). The content will automatically collapse if it
    ///  contains 700+ characters or 5+ linebreaks, and will display a "Show more..."
    ///  link to expand the content.
    /// </summary>
    public string Text { get; set; }

    /// <summary>
    /// Fields are defined as an array, and hashes contained within it will be displayed
    ///  in a table inside the message attachment.
    /// </summary>
    public IEnumerable<AttachmentField> Fields { get; set; }

    /// <summary>
    /// A valid URL to an image file that will be displayed inside a message attachment.
    ///  We currently support the following formats: GIF, JPEG, PNG, and BMP.
    ///
    /// Large images will be resized to a maximum width of 400px or a maximum height
    ///  of 500px, while still maintaining the original aspect ratio.
    /// </summary>
    public string ImageUrl { get; set; }

    /// <summary>
    /// The thumbnail's longest dimension will be scaled down to 75px while maintaining
    ///  the aspect ratio of the image. The filesize of the image must also be less than
    ///  500 KB.
    /// </summary>
    public string ThumbUrl { get; set; }
  }
}
