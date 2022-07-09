namespace Yella.Domain.Dto;

public abstract class AuditedEntityDto<TKey> : CreationAuditedEntityDto<TKey>, IAuditedEntityDto
    where TKey : struct
{
    protected AuditedEntityDto(TKey id) : base(id) { }

    protected AuditedEntityDto() { }
    public virtual DateTime? LastModificationTime { get; protected set; }
    public virtual Guid? LastModifierId { get; protected set; }
}