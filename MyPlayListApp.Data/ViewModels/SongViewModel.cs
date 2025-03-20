using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using MyPlayListApp.Data.FilterModels;

namespace MyPlayListApp.Data.ViewModels
{
    public class SongViewModel
    {
        public SongsFilter Filter { get; set; }
        public SongItemResult SongItemResult { get; set; }
        public SelectList Singers { get; set; }
        public SelectList Categories { get; set; }
    }
}
