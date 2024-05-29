using System.ComponentModel.DataAnnotations;

public class Candidate
{
    public int Id { get; set; }
    [Required]
    public string FirstName { get; set; }
    [Required]
    public string LastName { get; set; }
    [Required]
    [EmailAddress]
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public string CallTime { get; set; }
    public string LinkedInProfile { get; set; }
    public string GitHubProfile { get; set; }
    [Required]
    public string Comment { get; set; }
}