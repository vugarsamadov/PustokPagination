namespace PustokProject.CoreModels
{
    public class Tag : BaseModel
    {
        public string Title { get; set; }
        public ICollection<Blog> Blogs { get; set; }
    }
}
