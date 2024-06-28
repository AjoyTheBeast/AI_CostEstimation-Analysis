namespace ACE_APIForTest.Models
{
    public class ACEModel
    {
        public string UserInput { get; set; } 
        public string MLOutput { get; set; } 
    }

    public class ACEDTOModel
    {
        public string UserInput { get; set; } = string.Empty;
        public string MLOutput { get; set; } = string.Empty;
    }
}
