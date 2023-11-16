using System;
using System.Collections.Generic;

namespace DataExplorerApi.Models;

public partial class AnnotationIndex
{
    public string? ColName { get; set; }

    public string? DocName { get; set; }

    public string? Annotation { get; set; }

    public string? Keyword { get; set; }

    public string? LinkedKeywordConcept { get; set; }
}
