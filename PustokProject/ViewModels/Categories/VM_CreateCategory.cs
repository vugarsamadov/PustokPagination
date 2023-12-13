using System.ComponentModel.DataAnnotations;

namespace PustokProject.ViewModels.Categories;

public class VM_CreateCategory
{
    [Required]
    public string Name { get; set; }
    public int? ParentId { get; set; }
}