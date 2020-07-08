using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GenericRestService.Requests;
using GenericRestService.Responses;
using AutoMapper;
using Data.entities;

namespace GenericRestService
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<AlbumRequest, Albums>().ForMember(x => x.Id, opt => opt.Ignore());
            CreateMap<Albums, AlbumResponse>();
        }

        public static string MapEnum(Enum anyEnum)
        {
            return anyEnum.ToString();
        }
    }
}
