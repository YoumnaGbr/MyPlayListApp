using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace MyPlayListApp.Data.ViewModels
{
    public class SingerDTO
    {
        public Guid Id { get; set; }
        [Required(ErrorMessage = "Singer name is required.")]
        [StringLength(100, ErrorMessage = "Max length 100 characters.")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Singer Date Of Birth is required.")]
        [DataType(DataType.Date)]
        public DateOnly DateOfBirth { get; set; }

        [Required(ErrorMessage = "Kindely Upload An Image For The Singer.")]
        public IFormFile ImageFile { get; set; }
        public string Image { get; set; }
    }
}
