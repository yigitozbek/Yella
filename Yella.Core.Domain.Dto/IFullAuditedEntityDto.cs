namespace Yella.Core.Domain.Dto;

public interface IFullAuditedEntityDto
{
    bool IsDeleted { get; }
    Guid? DeleterId { get; }
    DateTime? DeletionTime { get; }
}