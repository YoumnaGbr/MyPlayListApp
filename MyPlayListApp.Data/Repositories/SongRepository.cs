using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MyPlayListApp.Data.Context;
using MyPlayListApp.Data.DTO;
using MyPlayListApp.Data.Entities;
using MyPlayListApp.Data.FilterModels;
using MyPlayListApp.Data.Interfaces;
using MyPlayListApp.Data.ViewModels;

namespace MyPlayListApp.Data.Repositories
{
    public class SongRepository : Repository<Song>, ISongRepository
    {
        private readonly MyPlayListAppContext _context;
        public SongRepository(MyPlayListAppContext context) : base(context)
        {
            _context = context;
        }

        public ResultBase AddNewSong(Song song)
        {
            var result = new ResultBase();
            try
            {
                var addedsong = Add(song);
                _context.SaveChanges();
                if (addedsong != null)
                {
                    result.Success = true;
                    result.Message = "Song Added Successfully.";
                }
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = $"Failed To Add Song: {ex.Message}";
            }
            return result;

        }

        public ResultBase DeleteSong(Guid songId)
        {
            var result = new ResultBase();
            try
            {
                var isExists = IsSongExists(songId);
                if (!isExists)
                {
                    result.Success = false;
                    result.Message = "Song Not Found.";
                }
                else
                {
                    Delete(songId);
                    _context.SaveChanges();
                    result.Success = true;
                    result.Message = "Song Deleted Successfully.";
                }
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = $"Failed to Delete Song: {ex.Message}";
            }
            return result;
        }

        public SongViewModel GetPlayList(SongsFilter filter) 
        {

            var result = new SongViewModel();
            var playlist = _context.Songs.Select(s => new
            {
                SongId = s.Id,
                SongName = s.Name,
                singers = s.Singer,
                categories = s.Category
            });

            if (filter.SingerId != null && filter.SingerId != Guid.Empty)
            {
                playlist = playlist.Where(s => s.singers.Id == filter.SingerId);
            }

            if (filter.CategoryId != null && filter.CategoryId != Guid.Empty)
            {
                playlist = playlist.Where(s => s.categories.Id == filter.CategoryId);
            }

            playlist = playlist.OrderBy(s => s.SongName);


            var totalCount = playlist.Count(); 
            var totalPages = (int)Math.Ceiling((double)totalCount / filter.PageSize);

            if (filter.PageIndex >= 0 && filter.PageSize > 0)
            {
                playlist = playlist.Skip((filter.PageIndex - 1) * filter.PageSize).Take(filter.PageSize);
            }

                var songs = playlist
                .Select(s => new SongDTO()
                {
                    Id = s.SongId,
                    SongName = s.SongName,
                    TypeName = s.categories.Name,
                    SingerName = s.singers.Name,
                    SingerDateOfBirth = s.singers.DateOfBirth,
                    SingerId = s.singers.Id,
                    CategoryId = s.categories.Id,
                    SingerImage = s.singers.Image
                }).ToList();

            result = new SongViewModel
            {
                songs = songs,
                TotalCount = totalCount,
                TotalPages = totalPages,
                PageIndex = filter.PageIndex,
                PageSize = filter.PageSize
            };
            return result;
        }

        public bool IsSongExists(Guid songId)
        {
            var isExists = _context.Songs.Any(s => s.Id == songId);
            return isExists;
        }

        public ResultBase UpdateSong(Song song)
        {
            var result = new ResultBase();
            try
            {
                var isExists = IsSongExists(song.Id);
                if (!isExists)
                {
                    result.Success = false;
                    result.Message = "Song Not Found.";
                }
                else
                {
                    var updatedSong = Update(song);
                    if (updatedSong != null)
                    {
                        result.Success = true;
                        result.Message = "Song Updated Successfully.";
                    }
                }
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = $"Failed to Update Song: {ex.Message}";
            }
            return result;
        }
    }
}
