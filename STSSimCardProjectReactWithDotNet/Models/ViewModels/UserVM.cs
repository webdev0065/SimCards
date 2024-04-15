using System.ComponentModel.DataAnnotations;

namespace STSSimCardProjectReactWithDotNet.Models.ViewModels
{
    public class UserVM
    {

        
        [Required]
        public string EMail { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
