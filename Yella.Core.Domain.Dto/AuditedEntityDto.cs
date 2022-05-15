using System;

namespace Archseptia.Core.Domain.Dto
{
    public abstract class AuditedEntityDto : CreationAuditedEntityDto, IAuditedEntityDto
    {
        public virtual DateTime? LastModificationTime { get; protected set; }
        public virtual Guid? LastModifierId { get; protected set; }
    }
}