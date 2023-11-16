using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace DataExplorerApi.Models;

public partial class VastDataContext : DbContext
{
    public VastDataContext()
    {
    }

    public VastDataContext(DbContextOptions<VastDataContext> options)
        : base(options)
    {
    }

    public virtual DbSet<ActivityIndex> ActivityIndices { get; set; }

    public virtual DbSet<AnnotationIndex> AnnotationIndices { get; set; }

    public virtual DbSet<EventIndex> EventIndices { get; set; }

    public virtual DbSet<GroupIndex> GroupIndices { get; set; }

    public virtual DbSet<ParticipantIndex> ParticipantIndices { get; set; }

    public virtual DbSet<PrimaryIndex> PrimaryIndices { get; set; }

    public virtual DbSet<ProductIndex> ProductIndices { get; set; }

    public virtual DbSet<StatementIndex> StatementIndices { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ActivityIndex>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("activity-index");

            entity.Property(e => e.Activity).HasColumnName("activity");
            entity.Property(e => e.ActivityName).HasColumnName("activityName");
            entity.Property(e => e.Step).HasColumnName("step");
            entity.Property(e => e.StepName).HasColumnName("stepName");
        });

        modelBuilder.Entity<AnnotationIndex>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("annotation-index");

            entity.Property(e => e.Annotation).HasColumnName("annotation");
            entity.Property(e => e.ColName).HasColumnName("colName");
            entity.Property(e => e.DocName).HasColumnName("docName");
            entity.Property(e => e.Keyword).HasColumnName("keyword");
            entity.Property(e => e.LinkedKeywordConcept).HasColumnName("linkedKeywordConcept");
        });

        modelBuilder.Entity<EventIndex>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("event-index");

            entity.Property(e => e.Activity).HasColumnName("activity");
            entity.Property(e => e.ActivityName).HasColumnName("activityName");
            entity.Property(e => e.Event).HasColumnName("event");
            entity.Property(e => e.EventName).HasColumnName("eventName");
        });

        modelBuilder.Entity<GroupIndex>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("group-index");

            entity.Property(e => e.Comment).HasColumnName("comment");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.Object).HasColumnName("object");
            entity.Property(e => e.VastAge).HasColumnName("vastAge");
            entity.Property(e => e.VastAgeName).HasColumnName("vastAgeName");
            entity.Property(e => e.VastCreatedAt).HasColumnName("vastCreatedAt");
            entity.Property(e => e.VastEducation).HasColumnName("vastEducation");
            entity.Property(e => e.VastEducationName).HasColumnName("vastEducationName");
            entity.Property(e => e.VastMotherTongue).HasColumnName("vastMotherTongue");
            entity.Property(e => e.VastNationality).HasColumnName("vastNationality");
            entity.Property(e => e.VastNationalityName).HasColumnName("vastNationalityName");
            entity.Property(e => e.VastUpdatedAt).HasColumnName("vastUpdatedAt");
            entity.Property(e => e.VastVisitorOrganisation).HasColumnName("vastVisitorOrganisation");
            entity.Property(e => e.VastVisitorOrganisationName).HasColumnName("vastVisitorOrganisationName");
        });

        modelBuilder.Entity<ParticipantIndex>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("participant-index");

            entity.Property(e => e.AgeValue).HasColumnName("ageValue");
            entity.Property(e => e.Comment).HasColumnName("comment");
            entity.Property(e => e.CulturalPreferences).HasColumnName("culturalPreferences");
            entity.Property(e => e.EducationValue).HasColumnName("educationValue");
            entity.Property(e => e.GenderValue).HasColumnName("genderValue");
            entity.Property(e => e.MotherTongue).HasColumnName("motherTongue");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.NationalityValue).HasColumnName("nationalityValue");
            entity.Property(e => e.Object).HasColumnName("object");
            entity.Property(e => e.VastCreatedAt).HasColumnName("vastCreatedAt");
            entity.Property(e => e.VastLocation).HasColumnName("vastLocation");
            entity.Property(e => e.VastLocationCity).HasColumnName("vastLocationCity");
            entity.Property(e => e.VastMadeBy).HasColumnName("vastMadeBy");
            entity.Property(e => e.VastMemberOf).HasColumnName("vastMemberOf");
            entity.Property(e => e.VastName).HasColumnName("vastName");
            entity.Property(e => e.VastParticipatesIn).HasColumnName("vastParticipatesIn");
            entity.Property(e => e.VastSchool).HasColumnName("vastSchool");
            entity.Property(e => e.VastUpdatedAt).HasColumnName("vastUpdatedAt");
            entity.Property(e => e.VastUserId).HasColumnName("vastUserId");
            entity.Property(e => e.VastVisitDate).HasColumnName("vastVisitDate");
        });

        modelBuilder.Entity<PrimaryIndex>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("primary-index");

            entity.Property(e => e.Activity).HasColumnName("activity");
            entity.Property(e => e.ActivityName).HasColumnName("activityName");
            entity.Property(e => e.Concept).HasColumnName("concept");
            entity.Property(e => e.ConceptName).HasColumnName("conceptName");
            entity.Property(e => e.Event).HasColumnName("event");
            entity.Property(e => e.EventName).HasColumnName("eventName");
            entity.Property(e => e.Group).HasColumnName("group");
            entity.Property(e => e.GroupName).HasColumnName("groupName");
            entity.Property(e => e.Product).HasColumnName("product");
            entity.Property(e => e.ProductName).HasColumnName("productName");
            entity.Property(e => e.Statement).HasColumnName("statement");
            entity.Property(e => e.StatementName).HasColumnName("statementName");
            entity.Property(e => e.Step).HasColumnName("step");
            entity.Property(e => e.StepName).HasColumnName("stepName");
            entity.Property(e => e.Visitor).HasColumnName("visitor");
            entity.Property(e => e.VisitorName).HasColumnName("visitorName");
        });

        modelBuilder.Entity<ProductIndex>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("product-index");

            entity.Property(e => e.Comment).HasColumnName("comment");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.Object).HasColumnName("object");
            entity.Property(e => e.VastCreatedAt).HasColumnName("vastCreatedAt");
            entity.Property(e => e.VastDocumentUriref).HasColumnName("vastDocumentURIRef");
            entity.Property(e => e.VastImageUriref).HasColumnName("vastImageURIRef");
            entity.Property(e => e.VastMadeBy).HasColumnName("vastMadeBy");
            entity.Property(e => e.VastResourceId).HasColumnName("vastResourceId");
            entity.Property(e => e.VastResourceType).HasColumnName("vastResourceType");
            entity.Property(e => e.VastUpdatedAt).HasColumnName("vastUpdatedAt");
        });

        modelBuilder.Entity<StatementIndex>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("statement-index");

            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.Object).HasColumnName("object");
            entity.Property(e => e.ObjectRelation).HasColumnName("objectRelation");
            entity.Property(e => e.ObjectRelationNameDescription).HasColumnName("objectRelationNameDescription");
            entity.Property(e => e.Predicate).HasColumnName("predicate");
            entity.Property(e => e.PredicateNameDescription).HasColumnName("predicateNameDescription");
            entity.Property(e => e.StatementObject).HasColumnName("statementObject");
            entity.Property(e => e.StatementObjectNameDescription).HasColumnName("statementObjectNameDescription");
            entity.Property(e => e.Subject).HasColumnName("subject");
            entity.Property(e => e.SubjectNameDescription).HasColumnName("subjectNameDescription");
            entity.Property(e => e.VastCreatedAt).HasColumnName("vastCreatedAt");
            entity.Property(e => e.VastProduct).HasColumnName("vastProduct");
            entity.Property(e => e.VastUpdatedAt).HasColumnName("vastUpdatedAt");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
