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
    public interface ISongRepository : IRepository<Song>
    {
        SongItemResult GetPlayList(SongsFilter filter);
        ResultBase AddNewSong(Song song);
        ResultBase UpdateSong(Song song);
        ResultBase DeleteSong(Guid songId);
        bool IsSongExists(Guid songId); 
    }
}
