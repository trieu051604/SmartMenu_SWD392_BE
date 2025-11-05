namespace SmartMenu.Application.DTOs
{
    public class UserDto
    {
        public string FullName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public long RoleId { get; set; }    // 1: Admin, 2: Manager, 3: Staff, 4: User
        public bool IsActive { get; set; } = true;
    }
}
