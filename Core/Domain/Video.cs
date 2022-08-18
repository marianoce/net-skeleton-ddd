using Domain.Common;

namespace Domain
{
    public class Video : BaseDomainModel
    {
        public string Name { get; set; }
        public bool Active { get; set; }
    }
}