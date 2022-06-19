namespace Yella.Framework.Domain.Entities;

public interface IFullAuditedEntity
{
    bool IsDeleted { get; }
    Guid? DeleterId { get; }
    DateTime? DeletionTime { get; }
}