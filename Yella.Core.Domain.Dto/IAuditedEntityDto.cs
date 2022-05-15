﻿using System;

namespace Archseptia.Core.Domain.Dto
{
    public interface IAuditedEntityDto
    {
        DateTime? LastModificationTime { get; }
        Guid? LastModifierId { get; }
    }
}