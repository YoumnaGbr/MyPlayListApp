using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPlayListApp.Data.FilterModels
{
    public class SingersFilter : PagedResult
    {
        public string SingerName { get; set; }
    }
}
