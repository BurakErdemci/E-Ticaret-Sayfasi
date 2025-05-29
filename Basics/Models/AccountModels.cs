using System.ComponentModel.DataAnnotations;

namespace Basics.Models
{
    public class LoginViewModel
    {
        [Required]
        [Display(Name ="Eposta Adresi",Prompt ="Eposta Adresi")]
        [EmailAddress]
        public required string  Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Parola",Prompt ="Parola")]
        public required string Password { get; set; }
        [Display(Name =("Beni Hatırla"))]
        public bool RememberMe { get; set; }
    }
    public class RegisterViewModel
    {
        [Required]
        [Display(Name = "İsim", Prompt = "İsim")]
        public  required string  FirstName { get; set; }
        [Required]
        [Display(Name = "Soyisim", Prompt = "Soyisim")]
        public  required string LastName { get; set; }
        [Required]
        [Display(Name = "Doğum Tarihi", Prompt = "Doğum Tarihi")]
        [DataType(DataType.Date)]
        public required DateTime DateOfBirth { get; set; }
        [Required]
        [Display(Name = "Eposta Adresi", Prompt = "Eposta Adresi")]
        [EmailAddress]
        public required string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Parola", Prompt = "Parola")]
        public required string Password { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Parolanı Doğrula", Prompt = "Parolanı Doğrula")]
        [Compare("Password")]
        public required string ConfirmPassword { get; set; }
    }
} 