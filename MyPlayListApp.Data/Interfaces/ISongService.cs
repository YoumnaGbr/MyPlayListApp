using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyPlayListApp.Data.DTO;
using MyPlayListApp.Data.Entities;
using MyPlayListApp.Data.FilterModels;
using MyPlayListApp.Data.ViewModels;

namespace MyPlayListApp.Data.Interfaces
{
    public interface ISongService
    {
        SongViewModel GetPlayList(SongsFilter filter);
        ResultBase AddNewSong(SongDetailes song);
        ResultBase UpdateSong(SongDetailes song);
        ResultBase DeleteSong(Guid songId);
    }
}
