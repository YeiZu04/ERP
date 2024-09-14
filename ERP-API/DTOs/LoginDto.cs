namespace ERP_API.DTOs
{
    public class LoginDto
    {
        public string? Username { get; set; }
        public string? Password { get; set; }
        public DateTime Created_at { get; set; }
        public string?  CodeCompany { get; set; }

    }
}
