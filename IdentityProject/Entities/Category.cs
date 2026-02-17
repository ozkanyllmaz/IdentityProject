namespace IdentityProject.Entities
{
    public class Category
    {
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public string Label { get; set; }
        public virtual ICollection<Message> Messages { get; set; }

    }
}
