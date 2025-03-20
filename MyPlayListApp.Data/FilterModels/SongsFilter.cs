using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPlayListApp.Data.FilterModels
{
    public class SongsFilter : PagedResult
    {
        public Guid CategoryId { get; set; }
        public Guid SingerId { get; set; }
    }
}
