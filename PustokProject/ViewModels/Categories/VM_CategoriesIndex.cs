using System.Collections;
using PustokProject.CoreModels;

namespace PustokProject.ViewModels.Categories;

public class VM_CategoriesIndex
{
    public ICollection<Category> Categories { get; set; }
}