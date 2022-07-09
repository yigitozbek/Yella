namespace Yella.Domain.Dto;

public abstract class CreationAuditedEntityDto<TKey> : EntityDto<TKey>, ICreationAuditedEntityDto
    where TKey : struct
{
    protected CreationAuditedEntityDto(TKey id) : base(id) { CreationTime = DateTime.Now; }
    protected CreationAuditedEntityDto() { CreationTime = DateTime.Now; }
    public virtual DateTime CreationTime { get; protected set; }
    public virtual Guid? CreatorId { get; protected set; }
}