using Microsoft.AspNetCore.Identity;

namespace IdentityProject.Entities
{
    public class AppUser : IdentityUser
    {
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public string? ImageUrl { get; set; }
        public string? About { get; set; }
        public bool AcceptTerms { get; set; }

        public virtual ICollection<Message> SentMessages { get; set; }
        public virtual ICollection<Message> ReceivedMessages { get; set; }

        public string ConfirmCode { get; set; }
        public string Address { get; set; }
        public string MyAbout { get; set; }
        
        public List<Attachment> Attachments { get; set; }


    }
}
