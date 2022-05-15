using System.ComponentModel.DataAnnotations;

namespace Archseptia.Core.Domain.Dto
{
    public abstract class EntityDto : IEntityDto
    {
        protected EntityDto() { }

    }

    public class EntityDto<TKey> : EntityDto, IEntityDto<TKey>
        where TKey : struct
    {
        protected EntityDto() { }
        protected EntityDto(TKey id) { }

        [Key] public TKey Id { get; set; }

    }
}