using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using MyPlayListApp.Data.ViewModels;
using MyPlayListApp.Data.FilterModels;

namespace MyPlayListApp.Data.ViewModels
{
    public class SingerItemResult : PagedResult
    {
        public List<SingerDetailes> Singers { get; set; }
    }
}
