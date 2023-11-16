using System;
using System.Collections.Generic;

namespace DataExplorerApi.Models;

public partial class StatementIndex
{
    public string? Object { get; set; }

    public string? Name { get; set; }

    public string? VastCreatedAt { get; set; }

    public string? VastUpdatedAt { get; set; }

    public string? Subject { get; set; }

    public string? SubjectNameDescription { get; set; }

    public string? ObjectRelation { get; set; }

    public string? ObjectRelationNameDescription { get; set; }

    public string? Predicate { get; set; }

    public string? PredicateNameDescription { get; set; }

    public string? StatementObject { get; set; }

    public string? StatementObjectNameDescription { get; set; }

    public string? Context { get; set; }

    public string? VastProduct { get; set; }
}
