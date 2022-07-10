using Yella.Domain.Dto;

namespace Yella.AutoMapper.Test.Dtos;

public class PersonDto : FullAuditedEntityDto<Guid>
{
    public string FullName { get; set; }
}