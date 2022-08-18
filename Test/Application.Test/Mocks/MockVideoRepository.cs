using AutoFixture;
using Moq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.InMemory;
using Application.Contracts.Persistence;
using Domain;
using Infrastructure.Repositories;
using Infrastructure.Persistence;


namespace Test.Application.Mocks
{
    public static class MockVideoRepository
    {
        public static Mock<IVideoRepository> GetVideoRepository()
        {
            var fixture = new Fixture();
            fixture.Behaviors.Add(new OmitOnRecursionBehavior());
            var videos = fixture.CreateMany<Video>().ToList();
            videos.Add(fixture.Build<Video>()
                .With(tr => tr.Name, "pepe")
                .Create()
            );

            var options = new DbContextOptionsBuilder<VideoDbContext>().UseInMemoryDatabase(databaseName: $"{Guid.NewGuid()}").Options;

            var videoDbContextFake = new VideoDbContext(options);
            videoDbContextFake.Videos!.AddRange(videos);
            videoDbContextFake.SaveChanges();

            var mockRepository = new Mock<IVideoRepository>(videoDbContextFake);
            
            return mockRepository;
        }
    }
}