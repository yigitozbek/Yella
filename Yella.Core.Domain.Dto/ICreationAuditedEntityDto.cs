namespace Yella.Framework.Domain.Dto;

public interface ICreationAuditedEntityDto
{
    DateTime CreationTime { get; }
    Guid? CreatorId { get; }
}