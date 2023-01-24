namespace backend.Models
{
    public class Category
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }  = String.Empty;
        public List<Post>? Posts { get; set; }
    }
}