namespace backend.Models
{
    public class Post
    {
        public int PostId { get; set; }
        public string? Title { get; set; }
        public string? ImageUrl { get; set; }
        public string? Description { get; set; }
        public DateTime PublishDate { get; set; }

        // Relation
        public int CategoryId { get; set; }
        public Category Category { get; set; } = new Category();
    }
}