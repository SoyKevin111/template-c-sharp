using AutoMapper;
using templatebase.src.auth.Dtos;
using templatebase.src.Infraestructure.Adapters.In.Dto;
using templatebase.src.User.Infraestructure.Adapters.Out.Entities;

namespace templatebase.src.Common
{
    public class MapperAPI : Profile
    {
        public MapperAPI()
        {
            CreateMap<UserEntity, UserDataDTO>().ReverseMap();
        }
    }
}