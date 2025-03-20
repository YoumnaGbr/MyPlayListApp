using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using AutoMapper.Execution;
using Microsoft.EntityFrameworkCore;
using MyPlayListApp.Data.Context;
using MyPlayListApp.Data.Entities;
using MyPlayListApp.Data.FilterModels;
using MyPlayListApp.Data.Interfaces;
using MyPlayListApp.Data.ViewModels;
namespace MyPlayListApp.Data.Repositories
{
    public class SingerRepository : Repository<Singer>, ISingerRepository
    {
        private readonly MyPlayListAppContext _context;
        public SingerRepository(MyPlayListAppContext context) : base(context)
        {
            _context = context;
        }

        
        public SingerItemResult GetSingers(SingersFilter filter)
        {
            var result = new SingerItemResult();
            var singers = _context.Singers.Select(s => new
            {
                s.Id,
                s.Name,
                s.DateOfBirth,
                s.Image
            });
            if (!string.IsNullOrEmpty(filter.SingerName))
            {
                singers = singers.Where(s => s.Name.Contains(filter.SingerName));
            }

            singers = singers.OrderBy(s => s.Name);
            result.TotalCount = singers.Count();
            result.TotalPages = (int)Math.Ceiling((double)result.TotalCount / (double)filter.PageSize);
            singers = singers.Skip(filter.PageIndex * filter.PageSize).Take(filter.PageSize);
            result.Singers = singers.Select( s => new SingerDetailes()
            {
                Id = s.Id,
                Name = s.Name,
                DateOfBirth = s.DateOfBirth,
                Image = s.Image,
            }).ToList();
       
            return result;
        }

        public ResultBase AddNewSinger(Singer singer)
        {
            var result = new ResultBase();
            try
            {
                var addedsinger = Add(singer);
                _context.SaveChanges();
                if (addedsinger != null) 
                {
                    result.Success = true;
                    result.Message = "Singer Added Successfully.";
                }
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = $"Failed To Add Singer: {ex.Message}";
            }
            return result;
        }

        public ResultBase UpdateSinger(Singer singer)
        {
            var result = new ResultBase();
            try
            {
                var isExists = IsSingerExists(singer.Id);
                if (!isExists) 
                {
                    result.Success = false;
                    result.Message = "Singer Not Found.";
                }
                else
                {
                    var updatedSinger = Update(singer);
                    _context.SaveChanges();
                    if (updatedSinger != null)
                    {
                        result.Success = true;
                        result.Message = "Singer Updated Successfully.";
                    }
                }

            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = $"Failed to Update Singer: {ex.Message}";
            }
            return result;
        }

        public ResultBase DeleteSinger(Guid singerId) 
        {
            var result = new ResultBase();
            try
            {
                var isExists = IsSingerExists(singerId);
                if (!isExists)
                {
                    result.Success = false;
                    result.Message = "Singer Not Found.";
                }
                if (HasSongs(singerId))
                {
                    result.Success = false;
                    result.Message = "Cannot Delele Singer Has Songs";
                }
                else
                {
                    Delete(singerId);
                    _context.SaveChanges();
                    result.Success = true;
                    result.Message = "Singer Deleted Successfully.";
                }
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = $"Failed to Delete Singer: {ex.Message}";
            }
            return result;
        }

        public bool IsSingerExists(Guid singerId) 
        {
            var isExists = _context.Singers.Any(s => s.Id == singerId);
            return isExists;
        }
        public bool HasSongs (Guid singerId)
        {
            var hasSongs = _context.Singers.Where(s => s.Id == singerId)
                                           .Any(s => s.Songs.Any(m => m.SingerId == singerId));
            return hasSongs;
        }
    }
}
