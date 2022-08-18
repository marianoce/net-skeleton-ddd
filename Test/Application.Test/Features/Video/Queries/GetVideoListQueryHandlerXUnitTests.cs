using AutoMapper;
using Moq;
using Shouldly;
using Xunit;
using Application.Features.Videos.Querys.GetVideoListQuery;
using Microsoft.EntityFrameworkCore.InMemory;
using Application.Contracts.Persistence;
using Application.Mappings;
using Test.Application.Mocks;

namespace Test.Application.Features.Video.Queries
{
    public class GetVideoListQueryHandlerXUnitTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<IUnitOfWork> _unitOfWork;

        public GetVideoListQueryHandlerXUnitTests()
        {
            _unitOfWork = MockUnitOfWork.GetUnitOfWork();
            var mapperConfig = new MapperConfiguration(c => 
            {
                c.AddProfile<MappingProfile>();
            });
            _mapper = mapperConfig.CreateMapper();
        }

        [Fact]
        public async Task GetVideoListTest()
        {
            var handler = new GetVideoListQueryHandler(_unitOfWork.Object, _mapper);
            var request = new GetVideoListQuery("pepe");
            var result = await handler.Handle(request, CancellationToken.None);

            result.ShouldBeOfType<List<VideosVm>>();
            result.Count.ShouldBe(1);
        }
    }
}