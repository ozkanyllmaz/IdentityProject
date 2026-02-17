using Microsoft.AspNetCore.Http;

namespace IdentityProject.Dtos
{
    public class SendMessageDto
    {
        public string ReceiverEmail { get; set; }
        public string Subject { get; set; }
        public string MessageDetail { get; set; }
        public int CategoryId { get; set; }

        public List<IFormFile>? Attachments { get; set; }
    }
}
