namespace ERP_API.DTOs
{
    public class RegisterUserDto
    {
        public string? UserName { get; set; }      
        public string? Password { get; set; } // Opcional, si se establece por defecto
    }
}
