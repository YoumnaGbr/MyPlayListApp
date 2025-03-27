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
    public interface ISingerService
    {
        SingerItemResult GetSingers(SingersFilter filter);
        ResultBase AddNewSinger(SingerDTO singer);
        ResultBase UpdateSinger(SingerDTO singer);
        ResultBase DeleteSinger(Guid singerId);
        List<Singer> GetSingerList();
        SingerDTO GetSingerDetailes(Guid singerId);
    }
}

