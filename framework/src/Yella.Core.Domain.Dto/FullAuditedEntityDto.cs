namespace Yella.Framework.Domain.Dto;

public abstract class FullAuditedEntityDto : AuditedEntityDto, IFullAuditedEntityDto
{
    protected FullAuditedEntityDto() : base() { }

    public virtual bool IsDeleted { get; protected set; }
    public virtual Guid? DeleterId { get; protected set; }
    public virtual DateTime? DeletionTime { get; protected set; }
}