using System.ComponentModel.DataAnnotations;

namespace MvcApplication1.Entities.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "{0} Invalid username !")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "{0} Invalid password !"), 
        DataType(DataType.Password), 
        StringLength(30, ErrorMessage="{0} max {1} must be character.")]
        public string Password { get; set; }
    }
}
