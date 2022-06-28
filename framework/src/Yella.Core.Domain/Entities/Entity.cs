using System.ComponentModel.DataAnnotations;

namespace Yella.Framework.Domain.Entities;

public abstract class Entity : IEntity
{
    protected Entity() { }

}

public class Entity<TKey> : Entity, IEntity<TKey>
    where TKey : struct
{
    protected Entity() { }
    protected Entity(TKey id) { }
        
    [Key]
    public TKey Id { get; set; }

}