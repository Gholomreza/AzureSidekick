// Decompiled with JetBrains decompiler
// Type: Attrice.TeamFoundation.Controls.ItemProperties
// Assembly: Attrice.TeamFoundation.Controls.12, Version=4.5.1.0, Culture=neutral, PublicKeyToken=1a24c887ab53aa6e
// MVID: 393BE899-3C29-4A70-86D7-B4B965E5A8F5
// Assembly location: C:\Program Files (x86)\Attrice Corporation\Team Foundation Sidekicks 2015\Attrice.TeamFoundation.Controls.12.dll

using System.ComponentModel;
using Attrice.TeamFoundation.Common;
using Microsoft.TeamFoundation.VersionControl.Client;

namespace Attrice.TeamFoundation.Controls
{
  internal class ItemProperties
  {
    private Item _item;
    private readonly string _encoding;
    private readonly string _contentLength;
    private readonly string _checkinDate;

    [DisplayName("Encoding")]
    [Category("Latest Version")]
    [Description("Encoding of file latest version contents")]
    public string Encoding => _encoding;

    [DisplayName("Server Path")]
    [Category("General")]
    [Description("Item server path")]
    public string ServerItem => _item.ServerItem;

    [DisplayName("Item Type")]
    [Category("General")]
    [Description("Item type (either folder or file)")]
    public string ItemType => _item.ItemType.ToString();

    [DisplayName("Content Length")]
    [Category("Latest Version")]
    [Description("Content length of the file latest version")]
    public string ContentLength => _contentLength;

    [DisplayName("Latest Version")]
    [Category("Latest Version")]
    [Description("Latest item version ID (changeset ID). For folders ID of the changeset created on explicit folder change")]
    public int ChangesetID => _item.ChangesetId;

    [DisplayName("Latest Checkin Date")]
    [Category("Latest Version")]
    [Description("Date of the item last checkin. For folders date of the changeset created on explicit folder change")]
    public string CheckinDate => _checkinDate;

    public ItemProperties(Item item)
    {
      _item = item;
      _checkinDate = Utilities.FormatDateTimeInvariant(item.CheckinDate);
      if (item.ItemType == Microsoft.TeamFoundation.VersionControl.Client.ItemType.File)
      {
        try
        {
          var encoding = System.Text.Encoding.GetEncoding(item.Encoding);
          _encoding = string.Format("{1} [{0}]", encoding.EncodingName, encoding.HeaderName);
        }
        catch
        {
          _encoding = item.Encoding.ToString();
        }
        if (item.ContentLength < 1024L)
          _contentLength = $"{(object)item.ContentLength} bytes";
        else if (item.ContentLength < 1048576L)
        {
          _contentLength =
              $"{(object)(item.ContentLength / 1024L)}.{(object)(item.ContentLength % 1024L)} KB ({(object)item.ContentLength} bytes)";
        }
        else
        {
          if (item.ContentLength >= 1073741824L)
            return;
          _contentLength =
              $"{(object)(item.ContentLength / 1048576L)}.{(object)(item.ContentLength / 1024L % 1024L)} MB ({(object)item.ContentLength} bytes)";
        }
      }
      else
      {
        _encoding = "Not applicable";
        _contentLength = "Not applicable";
      }
    }
  }
}
