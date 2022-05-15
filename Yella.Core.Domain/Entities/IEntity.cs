﻿namespace Archseptia.Core.Domain.Entities
{
    public interface IEntity<out TKey> : IEntity
    {
        TKey Id { get; }
    }
}