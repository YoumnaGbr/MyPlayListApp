﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyPlayListApp.Data.DTO;
using MyPlayListApp.Data.Entities;
using MyPlayListApp.Data.FilterModels;
using MyPlayListApp.Data.Interfaces;
using MyPlayListApp.Data.Repositories;
using MyPlayListApp.Data.ViewModels;

namespace MyPlayListApp.Data.Services
{
    public class SongService : ISongService
    {
        private readonly ISongRepository _songRepository;

        public SongService(ISongRepository songRepository)
        {
            _songRepository = songRepository;
        }
      
        public SongItemResult GetPlayList(SongsFilter filter)
        {
            return _songRepository.GetPlayList(filter);
        }

        public ResultBase AddNewSong(SongDetailes song)
        {
            var result = new ResultBase();
            var addedSong = new Song()
            {
                Id = Guid.NewGuid(),
                Name = song.Name,
                SingerId = song.SingerId,
                CategoryId = song.CategoryId
            };
            result = _songRepository.AddNewSong(addedSong);
            return result;

        }
        public ResultBase UpdateSong(SongDetailes song)
        {
            var result = new ResultBase();
            var isExists = _songRepository.IsSongExists(song.Id);
            if (!isExists)
            {
                result.Success = false;
                result.Message = "Song Not Found.";
                return result;
            }
            var updatedSong = new Song()
            {
                Id = song.Id,
                Name = song.Name,
                SingerId = song.SingerId,
                CategoryId = song.CategoryId
            };
            result = _songRepository.UpdateSong(updatedSong);
            return result;
        }
        public ResultBase DeleteSong(Guid songId)
        {
            var result = new ResultBase();
            var isExists = _songRepository.IsSongExists(songId);
            if (!isExists)
            {
                result.Success = false;
                result.Message = "Song Not Found.";
                return result;
            }
            result = _songRepository.DeleteSong(songId);
            return result;
        }
       
    }
}
