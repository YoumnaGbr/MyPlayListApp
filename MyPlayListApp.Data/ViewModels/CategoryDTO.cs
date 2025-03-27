using MyPlayListApp.Data.Entities;
using System.ComponentModel.DataAnnotations;

namespace MyPlayListApp.Data.ViewModels
{
    public class CategoryDTO
    {
        public Guid CategoryId { get; set; }
        public string CategoryName { get; set; }

    }
}
