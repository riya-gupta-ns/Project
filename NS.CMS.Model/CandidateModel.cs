using System.ComponentModel;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace NS.CMS.Model 
{
  public class CandidateModel {

    [Key]
    public int Id { get; set; }

    [Required(ErrorMessage = "Please enter name")]
    [StringLength(30, MinimumLength = 4, ErrorMessage = "Name should be between 4 and 30 characters")]
    [RegularExpression("^([a-zA-Z]{2,}\\s[a-zA-Z]{1,}'?-?[a-zA-Z]{2,}\\s?([a-zA-Z]{1,})?)", ErrorMessage = "Valid Charactors include (A-Z) (a-z) (' space -)") ]
    public string Name { get; set; } = null!;
    
    [DataType(DataType.Date)]
    [DisplayName("Date Of Birth")]
    [Required(ErrorMessage = "Please enter date of birth")]
    public DateOnly Dob { get; set; }

    [Required(ErrorMessage = "This Field is Required.")]
    [MinLength(5, ErrorMessage = "The Address must be at least 5 characters")]
    [MaxLength(25, ErrorMessage = "The Address cannot be more than 25 characters")]
    public string Address { get; set; } = null!;

    [Phone]
    [Display(Name = "Phone Number")]
    [Required(ErrorMessage = "Please enter phone number")]
    [RegularExpression(@"^[6-9]\d{9}$", ErrorMessage = "Please enter without any country code")]
    public string Mobile { get; set; } = null!;

    [EmailAddress]
    [Display(Name = "Email Address")]
    [Required(ErrorMessage = "Please enter email address")]
    public string Email { get; set; } = null!;

    [Required(ErrorMessage = "Please select atleat one")]
    public string Tech { get; set; } = null!;

    [DisplayName("Upload Image")]
    [Required(ErrorMessage = "Please choose an image")]
    public IFormFile Image { get; set; } = null!;

    [DataType(DataType.Upload)]
    [Display(Name ="Select File")]
    [Required(ErrorMessage = "Please upload a resume")]
    public IFormFile Resume { get; set; } = null!;

    [Required(ErrorMessage = "Please describe yourself a bit.")]
    [StringLength(50, MinimumLength = 3, ErrorMessage = "should be between 3 and 50 characters")]
    public string? Description { get; set; }

    [Required(ErrorMessage = "Please select one")]
    public string? Gender { get; set; }
  }
}
