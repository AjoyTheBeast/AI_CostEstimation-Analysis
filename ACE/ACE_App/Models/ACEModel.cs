using Microsoft.AspNetCore.Mvc.Rendering;

namespace ACE_App.Models
{
    public class ACEModel
    {
        // Properties for form input
        public string UserInput { get; set; } = string.Empty;
        public string MLOutput { get; set; } = string.Empty;
        public string ErrorMessage { get; set; } = string.Empty;


        // Properties for dropdown options  
        public string ProjectComplexity { get; set; } = string.Empty;
        public List<string> TechnologyStack { get; set; } = null;
        public string DevelopmentMethodology { get; set; } = string.Empty;
        public string ProjectDomain { get; set; } = string.Empty;
        public List<string> RiskFactors { get; set; } = null;
        public List<string> QualityMetrics { get; set; } = null;
        public int team_size { get; set; }
        public int project_timeline { get; set; }
        public string external_factor { get; set; } = string.Empty;

        // Properties for dropdown SelectList
        public SelectList ProjectComplexityOptions { get; set; } = null;
        public MultiSelectList TechnologyStackOptions { get; set; } = null;
        public SelectList DevelopmentMethodologyOptions { get; set; } = null;
        public SelectList ProjectDomainOptions { get; set; } = null;
        public MultiSelectList RiskFactorsOptions { get; set; } = null;
        public MultiSelectList QualityMetricsOptions { get; set; } = null;
    }

}
