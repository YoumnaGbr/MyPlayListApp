using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyPlayListApp.Data.Entities;
using MyPlayListApp.Data.FilterModels;
using MyPlayListApp.Data.ViewModels;

namespace MyPlayListApp.Data.Interfaces
{
    public interface ISingerRepository : IRepository<Singer>
    {
        SingerItemResult GetSingers(SingersFilter filter);
        ResultBase AddNewSinger(Singer singer);
        ResultBase UpdateSinger(Singer singer);
        ResultBase DeleteSinger(Guid singerId);
        bool IsSingerExists(Guid singerId);
    }
}
