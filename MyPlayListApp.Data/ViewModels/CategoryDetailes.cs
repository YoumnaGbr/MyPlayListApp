using MyPlayListApp.Data.Entities;
using System.ComponentModel.DataAnnotations;

namespace MyPlayListApp.Data.ViewModels
{
    public class CategoryDetailes
    {
        public Guid CategoryId { get; set; }
        [Required(ErrorMessage = "Category name is required.")]
        [StringLength(100, ErrorMessage = "Max length 100 characters.")]
        public string CategoryName { get; set; }

    }
}
