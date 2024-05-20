using LMS.Models;

namespace LMSWeb.Models
{
    public class SubjectOutlineViewModel
    {
        public Guid Id { get; set; }

        public Guid SubjectId { get; set; }

        public string? Overview { get; set; }

        public string? Importance { get; set; }

        public string? Objectives { get; set; }

        public string? FundamentalPrinciples { get; set; }

        public string? CoreConcepts { get; set; }

        public string? RealWorldApplications { get; set; }

        public string? Implications { get; set; }

        public string? StudyPath { get; set; }

        public string? KeyResearchers { get; set; }

        public string? MajorStudies { get; set; }

        public string? ResearchMethodologies { get; set; }

        public string? Textbooks { get; set; }

        public string? OnlineResources { get; set; }

        public string? AdditionalReferences { get; set; }

        public string? GradingRubrics { get; set; }

        public string? Summary { get; set; }

        public string? FutureStudy { get; set; }

        public bool? IsDeleted { get; set; }
        public string? SubjectName { get; set; }
        public virtual Subject? Subject { get; set; }
    }
}
