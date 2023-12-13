using System.ComponentModel.DataAnnotations;
using PustokProject.Enums;

namespace PustokProject.ViewModels.Sliders;

public class VM_CreateSlider
{
    

    [Microsoft.Build.Framework.Required]
    [MaxLength(30)]
    public string Title { get; set; }

    [Microsoft.Build.Framework.Required]
    [MaxLength(40)]
    public string Description { get; set; }

    [Microsoft.Build.Framework.Required]
    [MaxLength(40)]
    public string ButtonText { get; set; }

    [Microsoft.Build.Framework.Required]
    public int TextPosition { get; set; }

    [Microsoft.Build.Framework.Required]
    public string ThumpnailUrl { get; set; }

}