using System.ComponentModel.DataAnnotations.Schema;

namespace PustokProject.CoreModels
{
    public class BaseModel
    {
        public int Id { get; set; }
        public bool IsDeleted { get; private set; } = false;

        [Column(TypeName="date")]
        public DateTime CreatedAt { get; set; } = DateTime.Now.Date;
        [Column(TypeName="date")]
        public DateTime UpdatedAt { get; set; } = DateTime.Now;

        public void Delete()
        {
            IsDeleted = true;
        }
        public void RevokeDelete()
        {
            IsDeleted = false;
        }
    }
}
