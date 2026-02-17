namespace IdentityProject.Dtos
{
    public class UserEditDto
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ImageUrl { get; set; }
        public string PhoneNumber { get; set; }
        public string ConfirmPassword { get; set; }
        public string About { get; set; }
        public IFormFile Image { get; set; }
        public string MyAbout { get; set; }
        public string Address { get; set; }
    }
}
