using System;

namespace Archseptia.Core.Domain.Dto
{
    public interface ICreationAuditedEntityDto
    {
        DateTime CreationTime { get; }
        Guid? CreatorId { get; }
    }
}