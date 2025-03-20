using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyPlayListApp.Data.FilterModels;

namespace MyPlayListApp.Data.ViewModels
{
    public class CategoryItemResult : PagedResult
    {
        public List<CategoryDetailes> Categories { get; set; }
    }
}
