using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPlayListApp.Data.FilterModels
{
    public class CategoriesFilter : PagedResult
    {
        public string CategoryName { get; set; }
    }
}
