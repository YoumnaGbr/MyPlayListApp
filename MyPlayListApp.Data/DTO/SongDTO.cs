using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyPlayListApp.Data.Entities;
using MyPlayListApp.Data.ViewModels;

namespace MyPlayListApp.Data.DTO
{
    public class SongDTO
    {
        public Guid Id { get; set; }
        public string SongName { get; set; }
        public Guid SingerId { get; set; }
        public Guid CategoryId { get; set; }
        public string SingerName { get; set; }
        public string SingerImage { get; set; }
        public string TypeName { get; set; }
        public DateOnly SingerDateOfBirth { get; set; }
        public List<SingerDTO> singers {  get; set; }
        public List<CategoryDTO> categories { get; set; }
    }
}
