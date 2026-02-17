namespace IdentityProject.Entities
{
    public class Message
    {
        public int MessageId { get; set; }
        public string? ReceiverId { get; set; }
        public string? SenderId { get; set; }

        public AppUser? Receiver { get; set; }
        public AppUser? Sender { get; set; }

        public int? CategoryId { get; set; }
        public Category? Category { get; set; }

        public string Subject { get; set; }
        public string MessageDetail { get; set; }
        public DateTime SendDate { get; set; }
        public bool IsStatus { get; set; }

        public bool IsSenderStarred { get; set; }
        public bool IsReceiverStarred { get; set; }

        public List<Attachment> Attachments { get; set; }
        public bool IsDeletedBySender { get; set; }
        public bool IsDeletedByReceiver { get; set; }


    }
}
