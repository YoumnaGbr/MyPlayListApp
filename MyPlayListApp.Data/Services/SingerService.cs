using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MyPlayListApp.Data.Entities;
using MyPlayListApp.Data.FilterModels;
using MyPlayListApp.Data.Helpers;
using MyPlayListApp.Data.Interfaces;
using MyPlayListApp.Data.ViewModels;

namespace MyPlayListApp.Data.Services
{
    public class SingerService : ISingerService
    {
        private readonly ISingerRepository _singerRepository;

        public SingerService(ISingerRepository singerRepository, IMapper mapper)
        {
            _singerRepository = singerRepository;
        }

        public SingerItemResult GetSingers(SingersFilter filter)
        {
            return _singerRepository.GetSingers(filter);
        }
       
        public ResultBase AddNewSinger(SingerDTO singer)
        {
            var result = new ResultBase();
            if (singer.ImageFile == null || ImageSettings.IsValidImage(singer.ImageFile) == false)
            {
                result.Success = false;
                result.Message = "Invalid image format. Only PNG and JPG are allowed.";
                return result;
            }
            var imageName = ImageSettings.UploadImage(singer.ImageFile);
            var addedSinger = new Singer()
            {
                Id = Guid.NewGuid(),
                Name = singer.Name,
                DateOfBirth = singer.DateOfBirth,
                Image = imageName
            };
            result = _singerRepository.AddNewSinger(addedSinger);
            return result;
        }
        public ResultBase UpdateSinger (SingerDTO singer)
        {
            var result = new ResultBase();
            var isExists = _singerRepository.IsSingerExists(singer.Id);
            if (!isExists)
            {
                result.Success = false;
                result.Message = "Singer Not Found.";
                return result;
            }
            if (singer.ImageFile != null)
            {
                if (ImageSettings.IsValidImage(singer.ImageFile) == false)
                {
                    result.Success = false;
                    result.Message = "Invalid image format. Only PNG and JPG are allowed.";
                }
                else
                {
                    ImageSettings.DeleteFile(singer.Image);
                    singer.Image = ImageSettings.UploadImage(singer.ImageFile);
                }
            }
            var updatedSinger = new Singer()
            {
                Id = singer.Id,
                Name = singer.Name,
                DateOfBirth = singer.DateOfBirth,
                Image = singer.Image
            };
            result = _singerRepository.UpdateSinger(updatedSinger);
            return result;
        }
        public ResultBase DeleteSinger(Guid singerId)
        {
            var result = new ResultBase();
            var singerImage = _singerRepository.GetById(singerId).Image;
            var isExists = _singerRepository.IsSingerExists(singerId);
            var hasSongs = _singerRepository.HasSongs(singerId);
            if (!isExists)
            {
                result.Success = false;
                result.Message = "Singer Not Found.";
                return result;
            }
            if (hasSongs)
            {
                result.Success = false;
                result.Message = "Cannot Delele Singer Has Songs";
                return result;
            }
            result = _singerRepository.DeleteSinger(singerId);
            if (result.Success) 
            {
                ImageSettings.DeleteFile(singerImage);
            }
            return result;
        }
        public List<Singer> GetSingerList() 
        {
            return _singerRepository.GetAll();
        }
        public SingerDTO GetSingerDetailes(Guid singerId) 
        {
            var result = _singerRepository.GetById(singerId);
            var singer = new SingerDTO()
            {
                Name = result.Name,
                Image = result.Image,
                DateOfBirth = result.DateOfBirth,
            };
            return singer;
        }

    }
}
