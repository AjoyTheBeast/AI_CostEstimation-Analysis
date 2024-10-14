using System.Collections.Generic;

namespace ACE_App.Models
{
    public class ACEModel
    {
        // Properties for form input and output
        public string UserInput { get; set; } = string.Empty; 
        public string MLOutput { get; set; } = string.Empty;  
        public string ErrorMessage { get; set; } = string.Empty;  
        public List<string> History { get; set; }
        public bool Status { get; set; } = false;


    }  
}
