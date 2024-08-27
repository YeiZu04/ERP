namespace ERP_API.DTOs
{
    public class RegisterEmployee
    {
        public PersonDto PersonDto { get; set; }
        public UserDto UserDto { get; set; }
        public EmployeeDto EmployeeDto { get; set; }
        public UserRoleDto UserRoleDto { get; set; }
        public CurriculumDto CurriculumDto { get; set; }
    }
}
