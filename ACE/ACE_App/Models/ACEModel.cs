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
        public List<string> TechnologyStack { get; set; } = new List<string>();
        public List<string> DevelopmentMethodology { get; set; } = new List<string>();
        public List<string> ProjectDomain { get; set; } = new List<string>();
        public List<string> RiskFactors { get; set; } = new List<string>();
        public List<string> QualityMetrics { get; set; } = new List<string>();
        public int team_size { get; set; }
        public int project_timeline { get; set; }
        public string external_factor { get; set; } = string.Empty;

        // Properties for dropdown SelectList
        public List<SelectListItem> ProjectComplexityOptions { get; set; } = new List<SelectListItem>();
        public List<SelectListItem> TechnologyStackOptions { get; set; } = new List<SelectListItem>();
        public List<SelectListItem> DevelopmentMethodologyOptions { get; set; } = new List<SelectListItem>();
        public List<SelectListItem> ProjectDomainOptions { get; set; } = new List<SelectListItem>();
        public List<SelectListItem> RiskFactorsOptions { get; set; } = new List<SelectListItem>();
        public List<SelectListItem> QualityMetricsOptions { get; set; } = new List<SelectListItem>();
    }

}
