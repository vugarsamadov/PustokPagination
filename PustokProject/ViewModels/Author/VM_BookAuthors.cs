using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using PustokProject.CoreModels;

namespace PustokProject.ViewModels.AuthorVMS
{
    public class VM_BookAuthors
    {

        public string Name { get; set; }
        public string Surname { get; set; }
        public VM_PaginatedEntityTable<Author>? PaginatedModel { get; set; }

    }
}
