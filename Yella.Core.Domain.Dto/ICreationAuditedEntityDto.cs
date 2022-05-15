namespace Yella.Core.Domain.Dto
{
    public interface ICreationAuditedEntityDto
    {
        DateTime CreationTime { get; }
        Guid? CreatorId { get; }
    }
}