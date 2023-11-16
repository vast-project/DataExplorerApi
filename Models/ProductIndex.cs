using System;
using System.Collections.Generic;

namespace DataExplorerApi.Models;

public partial class ProductIndex
{
    public string? Object { get; set; }

    public string? VastMadeBy { get; set; }

    public string? Name { get; set; }

    public string? VastCreatedAt { get; set; }

    public string? VastUpdatedAt { get; set; }

    public int? VastResourceId { get; set; }

    public string? VastResourceType { get; set; }

    public string? VastImageUriref { get; set; }

    public string? Comment { get; set; }

    public string? VastDocumentUriref { get; set; }
}
