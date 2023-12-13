using System.Net.Http.Headers;

namespace PustokProject.CoreModels
{
    public class BookImage : BaseModel
    {
        
        public string ImagePath { get; set; }
        
        public int BookId { get; set; }
        public Book Book { get; set; }


        public bool IsMatchImagePath(string imageUrl)
        {
            return imageUrl == ImagePath;
        }
        
    }
}
