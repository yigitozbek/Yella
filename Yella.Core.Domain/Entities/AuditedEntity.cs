namespace Yella.Core.Domain.Entities;

public abstract class AuditedEntity : CreationAuditedEntity, IAuditedEntity
{
    public virtual DateTime? LastModificationTime { get; protected set; }
    public virtual Guid? LastModifierId { get; protected set; }
}