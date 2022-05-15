namespace Yella.Core.Domain.Entities
{
    public abstract class CreationAuditedEntity : Entity, ICreationAuditedEntity
    {
        protected CreationAuditedEntity() { CreationTime = DateTime.Now; }
        public DateTime CreationTime { get; protected set; }
        public Guid? CreatorId { get; protected set; }
    }
}