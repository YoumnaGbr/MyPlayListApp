using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPlayListApp.Data.Entities
{
    public class Category
    {
        public Guid Id { get; set; }
        [Required(ErrorMessage = "Category name is required.")]
        [StringLength(100, ErrorMessage = "Max length 100 characters.")]
        public string Name { get; set; }
        public virtual IList<Song> Songs { get; set; }
    }
}
