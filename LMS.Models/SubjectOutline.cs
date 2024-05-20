using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace LMS.Models;

[Table(nameof(SubjectOutline))]
public class SubjectOutline
{
    [Key]
    [Column(nameof(SubjectOutline.Id))]
    public Guid Id { get; set; }

    [Required]
    [Column(nameof(SubjectOutline.SubjectId))]
    [ForeignKey(nameof(SubjectOutline.SubjectId))]
    public Guid SubjectId { get; set; }

    [Column(nameof(SubjectOutline.Overview))]
    public string? Overview { get; set; }

    [Column(nameof(SubjectOutline.Importance))]
    public string? Importance { get; set; }

    [Column(nameof(SubjectOutline.Objectives))]
    public string? Objectives { get; set; }

    [Column(nameof(SubjectOutline.FundamentalPrinciples))]
    public string? FundamentalPrinciples { get; set; }

    [Column(nameof(SubjectOutline.CoreConcepts))]
    public string? CoreConcepts { get; set; }

    [Column(nameof(SubjectOutline.RealWorldApplications))]
    public string? RealWorldApplications { get; set; }

    [Column(nameof(SubjectOutline.Implications))]
    public string? Implications { get; set; }

    [Column(nameof(SubjectOutline.StudyPath))]
    public string? StudyPath { get; set; }

    [Column(nameof(SubjectOutline.KeyResearchers))]
    public string? KeyResearchers { get; set; }

    [Column(nameof(SubjectOutline.MajorStudies))]
    public string? MajorStudies { get; set; }

    [Column(nameof(SubjectOutline.ResearchMethodologies))]
    public string? ResearchMethodologies { get; set; }

    [Column(nameof(SubjectOutline.Textbooks))]
    public string? Textbooks { get; set; }

    [Column(nameof(SubjectOutline.OnlineResources))]
    public string? OnlineResources { get; set; }

    [Column(nameof(SubjectOutline.AdditionalReferences))]
    public string? AdditionalReferences { get; set; }

    [Column(nameof(SubjectOutline.GradingRubrics))]
    public string? GradingRubrics { get; set; }

    [Column(nameof(SubjectOutline.Summary))]
    public string? Summary { get; set; }

    [Column(nameof(SubjectOutline.FutureStudy))]
    public string? FutureStudy { get; set; }

    [DefaultValue(false)]
    [Column(nameof(SubjectOutline.IsDeleted))]
    public bool? IsDeleted { get; set; }

    public virtual Subject Subject { get; set; }
}
