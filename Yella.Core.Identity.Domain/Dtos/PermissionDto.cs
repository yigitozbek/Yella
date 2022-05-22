using Yella.Core.Domain.Dto;

namespace Yella.Core.Identity.Dtos;

public class PermissionDto : EntityDto<short>
{
    public PermissionDto(string name, string description, string code, string tag)
    {
        Name = name;
        Description = description;
        Code = code;
        Tag = tag;
        IsChecked = false;
    }

    public string Name { get; set; }
    public string Description { get; set; }
    public string Code { get; set; }
    public string Tag { get; set; }
    public bool IsChecked { get; set; }
}