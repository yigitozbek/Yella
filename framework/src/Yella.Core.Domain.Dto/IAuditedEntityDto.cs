namespace Yella.Framework.Domain.Dto;

public interface IAuditedEntityDto
{
    DateTime? LastModificationTime { get; }
    Guid? LastModifierId { get; }
}