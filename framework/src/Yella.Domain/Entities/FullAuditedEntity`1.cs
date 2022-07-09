namespace Yella.Domain.Entities;

public abstract class FullAuditedEntity<TKey> : AuditedEntity<TKey>, IFullAuditedEntity
    where TKey : struct
{
    protected FullAuditedEntity(TKey id) : base(id) { }
    protected FullAuditedEntity() : base() { }

    public bool IsDeleted { get; protected set; }
    public Guid? DeleterId { get; protected set; }
    public DateTime? DeletionTime { get; protected set; }
}