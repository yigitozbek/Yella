using System;

namespace Archseptia.Core.Domain.Entities
{
    public interface ICreationAuditedEntity
    {
        DateTime CreationTime { get; }
        Guid? CreatorId { get; }
    }
}