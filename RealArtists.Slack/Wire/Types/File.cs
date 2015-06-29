namespace RealArtists.Slack.Wire.Types {
  using System;
  using System.Collections.Generic;

  public class File {
    public string Id { get; set; }

    /// <summary>
    /// When the file was created.
    /// </summary>
    public DateTime Created { get; set; }

    /// <summary>
    /// The name parameter may be null for unnamed files.
    /// </summary>
    public string Name { get; set; }
    public string Title { get; set; }

    /// <summary>
    /// The mimetype and filetype props do not have a 1-to-1 mapping, as multiple different
    ///  files types ('html', 'js', etc.) share the same mime type.
    /// </summary>
    public string Mimetype { get; set; }

    /// <summary>
    /// The mimetype and filetype props do not have a 1-to-1 mapping, as multiple different
    ///  files types ('html', 'js', etc.) share the same mime type.
    /// </summary>
    public string Filetype { get; set; }

    /// <summary>
    /// The pretty_type property contains a human-readable version of the type.
    /// </summary>
    public string PrettyType { get; set; }
    public string User { get; set; }

    /// <summary>
    /// The mode property contains one of hosted, external, snippet or post.
    /// </summary>
    public FileMode Mode { get; set; }

    /// <summary>
    /// The editable property indicates that files are stored in editable mode.
    /// </summary>
    public bool Editable { get; set; }

    /// <summary>
    /// The is_external property indicates whether the master copy of a file
    ///  is stored within the system or not. If the file is_external, then the
    ///  url property will point to the externally-hosted master file.
    ///  
    /// </summary>
    public bool IsExternal { get; set; }

    /// <summary>
    /// The external_type property will indicate what kind of external file it
    ///  is, e.g. dropbox or gdoc.
    /// </summary>
    public string ExternalType { get; set; }

    /// <summary>
    /// The size parameter is the filesize in bytes. 
    /// </summary>
    public int Size { get; set; }

    /// <summary>
    /// The url property points to a publically sharable URL directly to the file contents.
    /// </summary>
    public string Url { get; set; }

    /// <summary>
    /// Only editable-mode files currently have a url_download parameter. This URL includes headers which force a browser download.
    /// </summary>
    public string UrlDownload { get; set; }

    /// <summary>
    /// The url_private and url_private_download props are the same, but require cookie auth
    ///  to access. These URLs should be used by the web client, but can be ignored by other clients.
    /// </summary>
    public string UrlPrivate { get; set; }

    /// <summary>
    /// The url_private and url_private_download props are the same, but require cookie auth
    ///  to access. These URLs should be used by the web client, but can be ignored by other clients.
    /// </summary>
    public string UrlPrivateDownload { get; set; }

    /// <summary>
    /// If a thumbnail is available for the file, the URL to a 64x64 pixel will be returned as the thumb_64 prop.
    /// </summary>
    public string Thumb64 { get; set; }

    /// <summary>
    /// The thumb_80 prop (when present) contains the URL of an 80x80 thumb. Unlike the 64px thumb, this
    ///  size is guaranteed to be 80x80, even when the source image was smaller (it's padded with
    ///  transparent pixels). 
    /// </summary>
    public string Thumb80 { get; set; }

    /// <summary>
    ///  A variable sized thumb will be returned as thumb_360, with its longest size no bigger than 
    /// 360 (although it might be smaller depending on the source size). Dimensions for this thumb 
    /// are returned in thumb_360_w and thumb_360_h. In the case where the original image was an 
    /// animated gif with dimensions greater than 360 pixels, we also created an animated thumbnail 
    /// and pass it as thumb_360_gif.
    /// </summary>
    public string Thumb360 { get; set; }
    public string Thumb360Gif { get; set; }
    public int Thumb360W { get; set; }
    public int Thumb360H { get; set; }

    /// <summary>
    /// The permalink URL points to a single page for the file containing details, comments and a download link. 
    /// </summary>
    public string Permalink { get; set; }

    /// <summary>
    ///  The edit_link is only present for posts and snippets and is the page at which the file can be edited.
    /// </summary>
    public string EditLink { get; set; }

    /// <summary>
    /// For posts, we also include a short plain-text preview than can be shown in place of a tumbnail.
    /// </summary>
    public string Preview { get; set; }

    /// <summary>
    /// For snippets, we include a simple preview of the contents (a few truncated lines of plaintext),
    ///  as well as a more complex syntaxed highlighted preview (preview_highlight) in HTML. The total
    ///  count of lines in the snippet if returned in lines, while lines_more contains a count of
    ///  lines not shown in the preview.
    /// </summary>
    public string PreviewHighlight { get; set; }

    /// <summary>
    /// For snippets, we include a simple preview of the contents (a few truncated lines of plaintext),
    ///  as well as a more complex syntaxed highlighted preview (preview_highlight) in HTML. The total
    ///  count of lines in the snippet if returned in lines, while lines_more contains a count of
    ///  lines not shown in the preview.
    /// </summary>
    public int Lines { get; set; }

    /// <summary>
    /// For snippets, we include a simple preview of the contents (a few truncated lines of plaintext),
    ///  as well as a more complex syntaxed highlighted preview (preview_highlight) in HTML. The total
    ///  count of lines in the snippet if returned in lines, while lines_more contains a count of
    ///  lines not shown in the preview.
    /// </summary>
    public int LinesMore { get; set; }

    /// <summary>
    /// If a file is public, the is_public flag will be set.
    /// </summary>
    public bool IsPublic { get; set; }

    /// <summary>
    /// If a file's public URL has been shared, the public_url_shared flag will be set.
    /// </summary>
    public bool PublicUrlShared { get; set; }

    /// <summary>
    /// The channels array contains the IDs of any channels into which the file is currently shared. 
    /// </summary>
    public IEnumerable<string> Channels { get; set; }

    /// <summary>
    /// The groups array is the same but for private groups. Groups are only returned if the caller is a member of that group. 
    /// </summary>
    public IEnumerable<string> Groups { get; set; }

    /// <summary>
    /// The ims array is the same but for IM channels. IMs are only returned if the caller is a member of that IM channel.
    /// </summary>
    public IEnumerable<string> Ims { get; set; }

    /// <summary>
    /// The initial_comment will be a comment from the file uploader, and will only be set when the uploader commented
    ///  on the file at the time of upload. Clients can use this to display the comment with the file when announcing
    ///  new file uploads.
    /// </summary>
    public Message InitialComment { get; set; }

    /// <summary>
    /// The num_stars property contains the number of users who have starred this file. It is not present if no users have starred it. 
    /// </summary>
    public int? NumStars { get; set; }

    /// <summary>
    /// The is_starred property is present and true if the calling user has starred the file, else it is omitted.
    /// </summary>
    public bool IsStarred { get; set; }
  }
}
