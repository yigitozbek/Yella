using AutoMapper;
using Yella.AutoMapper.Test.Dtos;
using Yella.AutoMapper.Test.Entities;

namespace Yella.AutoMapper.Test.Profiles;

public class YellaAutoMapperProfile : Profile
{
    public YellaAutoMapperProfile()
    {
        CreateMap<PersonDto, Person>();
        CreateMap<Person, PersonDto>()
            .ForMember(i => i.FullName, o => o.MapFrom(x => $"{x.Name} {x.Surname}"));

    }
}