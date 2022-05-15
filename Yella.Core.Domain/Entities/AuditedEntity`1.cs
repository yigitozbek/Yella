namespace Yella.Core.Domain.Entities
{
    public abstract class AuditedEntity<TKey> : CreationAuditedEntity<TKey>, IAuditedEntity
        where TKey : struct
    {
        protected AuditedEntity(TKey id) : base(id) { }

        protected AuditedEntity() { }
        public virtual DateTime? LastModificationTime { get; protected set; }
        public virtual Guid? LastModifierId { get; protected set; }
    }
}
