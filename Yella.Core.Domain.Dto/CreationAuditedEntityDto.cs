namespace Yella.Core.Domain.Dto
{
    public abstract class CreationAuditedEntityDto : EntityDto, ICreationAuditedEntityDto
    {
        protected CreationAuditedEntityDto() { }

        public DateTime CreationTime { get; protected set; }
        public Guid? CreatorId { get; protected set; }
    }
}