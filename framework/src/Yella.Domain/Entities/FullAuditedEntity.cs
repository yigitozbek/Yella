namespace Yella.Domain.Entities;

public abstract class FullAuditedEntity : AuditedEntity, IFullAuditedEntity
{
    private FullAuditedEntity() : base()
    {
        IsDeleted = false;
    }

    public bool IsDeleted { get; protected set; }
    public Guid? DeleterId { get; protected set; }
    public DateTime? DeletionTime { get; protected set; }
}