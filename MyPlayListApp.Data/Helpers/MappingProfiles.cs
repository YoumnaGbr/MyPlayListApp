using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MyPlayListApp.Data.Entities;
using MyPlayListApp.Data.ViewModels;

namespace MyPlayListApp.Data.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<SingerDTO,Singer>().ReverseMap();
        }
    }
}
