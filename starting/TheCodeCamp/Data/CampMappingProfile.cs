using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TheCodeCamp.Models;

namespace TheCodeCamp.Data
{
    public class CampMappingProfile : Profile
    {
        public CampMappingProfile()
        {
            CreateMap<Camp, CampModel>()
                .ForMember(c => c.Venue, opt => opt.MapFrom(m => m.Location.VenueName))    //Configuramos un maepo especifico para un campo que no sigue el esquema de nombre prefijado.
                .ReverseMap();

            CreateMap<Talk, TalkModel>()
                .ReverseMap()
                .ForMember(t => t.Speaker, opt => opt.Ignore())
                .ForMember(t => t.Camp, opt => opt.Ignore());

            CreateMap<Speaker, SpeakerModel>().ReverseMap();



        }

    }
}