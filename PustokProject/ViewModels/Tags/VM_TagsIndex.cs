using PustokProject.CoreModels;

namespace PustokProject.ViewModels.Tags
{
    public class VM_TagsIndex
    {
        public IEnumerable<Tag> Tags { get; set; } = new List<Tag>();
    }
}
