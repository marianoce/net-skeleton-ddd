using Application.Features.Videos.Querys.GetVideoListQuery;
using AutoMapper;
using Domain;

namespace Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Video, VideosVm>();
        }
    }
}