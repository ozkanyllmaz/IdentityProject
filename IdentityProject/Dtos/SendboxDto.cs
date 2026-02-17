namespace IdentityProject.Dtos
{
    public class SendboxDto
    {
        public int MessageId { get; set; }
        public string ReceiverEmail { get; set; }
        public string Subject { get; set; }
        public DateTime SendDate{ get; set; }
    }
}
