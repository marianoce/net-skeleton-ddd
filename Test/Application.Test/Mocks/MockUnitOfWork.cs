using Moq;
using Application.Contracts.Persistence;

namespace Test.Application.Mocks
{
    public static class MockUnitOfWork
    {
        public static Mock<IUnitOfWork> GetUnitOfWork()
        {
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            var mockVideoRepository = MockVideoRepository.GetVideoRepository();

            mockUnitOfWork.Setup(p => p.VideoRepository).Returns(mockVideoRepository.Object);

            return mockUnitOfWork;
        }
    }
}