﻿namespace Archseptia.Core.Domain.Dto
{
    public interface IEntityDto<out TKey> : IEntityDto
    {
        TKey Id { get; }
    }
}