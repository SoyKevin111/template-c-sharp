using AutoMapper;
using templatebase.src.User.Entity;
using templatebase.src.User.Response;

namespace templatebase.src.Common
{
    public class EntityMapper : Profile
    {
        public EntityMapper()
        {
            CreateMap<UserEntity, UserResponse>().ReverseMap();
        }
    }
}