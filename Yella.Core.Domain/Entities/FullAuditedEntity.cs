namespace Yella.Core.Domain.Entities
{
    public abstract class FullAuditedEntity : AuditedEntity, IFullAuditedEntity
    {
        protected FullAuditedEntity() : base()
        {
            IsDeleted = false;
        }

        public virtual bool IsDeleted { get; protected set; }
        public virtual Guid? DeleterId { get; protected set; }
        public virtual DateTime? DeletionTime { get; protected set; }
    }
}
