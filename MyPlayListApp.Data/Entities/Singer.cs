using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPlayListApp.Data.Entities
{
    public class Singer
    {
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }

        [DataType(DataType.Date)]
        public DateOnly DateOfBirth { get; set; }
        public string? Image {  get; set; }
        public virtual List<Song> Songs { get; set; }

    }
}
