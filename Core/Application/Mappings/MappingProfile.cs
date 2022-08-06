using Application.Features.Videos.Querys.GetVideoListQuery;
using AutoMapper;
using Core.Application.Features.Videos.Querys.GetVideoListQuery;
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