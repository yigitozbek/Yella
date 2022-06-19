namespace Yella.Framework.Domain.Entities;

public abstract class CreationAuditedEntity<TKey> : Entity<TKey>, ICreationAuditedEntity
    where TKey : struct
{
    protected CreationAuditedEntity(TKey id) : base(id) { }
    protected CreationAuditedEntity() { }
    public virtual DateTime CreationTime { get; protected set; }
    public virtual Guid? CreatorId { get; protected set; }
}