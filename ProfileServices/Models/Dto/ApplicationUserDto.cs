namespace ProfileServices.Models.Dto
{
    public class ApplicationUserDto
    {
        public int Id { get; set; }
        public string? Email { get; set; }
        public string? UserId { get; set; }
        public string? IsHiringManager { get; set; }
    }
}
