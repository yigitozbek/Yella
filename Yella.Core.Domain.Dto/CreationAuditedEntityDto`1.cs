namespace Yella.Core.Domain.Dto
{
    public abstract class CreationAuditedEntityDto<TKey> : EntityDto<TKey>, ICreationAuditedEntityDto
        where TKey : struct
    {
        protected CreationAuditedEntityDto(TKey id) : base(id) { }
        protected CreationAuditedEntityDto() { }
        public virtual DateTime CreationTime { get; protected set; }
        public virtual Guid? CreatorId { get; protected set; }
    }
}
