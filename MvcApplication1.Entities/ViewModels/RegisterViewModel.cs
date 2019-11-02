using System.ComponentModel.DataAnnotations;

namespace MvcApplication1.Entities.ViewModels
{
    public class RegisterViewModel
    {
   
        [Required(ErrorMessage = "{0} Invalid name !"),
        StringLength(30, ErrorMessage = "{0} max {1} must be character.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "{0} Invalid name !"),
        StringLength(30, ErrorMessage = "{0} max {1} must be character.")]
        public string SurName { get; set; }


        [Required(ErrorMessage = "{0} Invalid password !"),
       EmailAddress(ErrorMessage = "{0} Invalid e-mail adress"),
       StringLength(70, ErrorMessage = "{0} max {1} must be character.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "{0} Invalid username !"), 
        StringLength(25, ErrorMessage="{0} max {1} must be character.")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "{0} Invalid password !"), 
        DataType(DataType.Password), 
        StringLength(30, ErrorMessage = "{0} max {1} must be character.")]
        public string Password { get; set; }

        [Required(ErrorMessage = "{0} Invalid password !"), 
        DataType(DataType.Password), 
        StringLength(30, ErrorMessage = "{0} max {1} must be character."),
        Compare("Password",ErrorMessage= "{0} with {1} are not equal")]
        public string RePassword { get; set; }
    }
}
