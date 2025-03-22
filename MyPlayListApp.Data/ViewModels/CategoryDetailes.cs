using MyPlayListApp.Data.Entities;
using System.ComponentModel.DataAnnotations;

namespace MyPlayListApp.Data.ViewModels
{
    public class CategoryDetailes
    {
        public Guid CategoryId { get; set; }
        public string CategoryName { get; set; }

    }
}
