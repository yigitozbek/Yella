namespace Yella.Framework.Domain.Entities;

public interface IAuditedEntity
{
    DateTime? LastModificationTime { get; }
    Guid? LastModifierId { get; }
}