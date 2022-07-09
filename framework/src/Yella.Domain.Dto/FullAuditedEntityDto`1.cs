namespace Yella.Domain.Dto;

public abstract class FullAuditedEntityDto<TKey> : AuditedEntityDto<TKey>, IFullAuditedEntityDto
    where TKey : struct
{
    protected FullAuditedEntityDto(TKey id) : base(id) { IsDeleted = false; }
    protected FullAuditedEntityDto() : base() { IsDeleted = false; }

    public bool IsDeleted { get; protected set; }
    public Guid? DeleterId { get; protected set; }
    public DateTime? DeletionTime { get; protected set; }
}