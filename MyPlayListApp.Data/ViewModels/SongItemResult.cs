using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyPlayListApp.Data.DTO;
using MyPlayListApp.Data.FilterModels;

namespace MyPlayListApp.Data.ViewModels
{
    public class SongItemResult : PagedResult
    {
        public List<SongDTO> songs { get; set; }

    }
}
