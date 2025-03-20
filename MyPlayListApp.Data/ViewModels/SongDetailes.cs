using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using MyPlayListApp.Data.Entities;



namespace MyPlayListApp.Data.ViewModels
{
    public class SongDetailes
    {
        public Guid Id { get; set; }
        [Required(ErrorMessage = "Song name is required.")]
        [StringLength(100, ErrorMessage = "Max length 100 characters.")]
        public string Name { get; set; }

        public Guid SingerId { get; set; }
        public Guid CategoryId { get; set; }
        public SelectList Singers { get; set; }
        public SelectList Categories { get; set; }
    }
}
