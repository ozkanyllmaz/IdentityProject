namespace IdentityProject.Entities
{
    public class Attachment
    {
        public int AttachmentId { get; set; }
        public string FileName { get; set; }
        public string FileType { get; set; }
        public string FileUrl { get; set; }
        public DateTime CreateDate { get; set; }

        public int? MessageId { get; set; }
        public Message Message { get; set; }


        public string? AppUserId { get; set; }
        public AppUser AppUser { get; set; }


    }
}
