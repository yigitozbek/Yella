using Yella.Core.Domain.Dto;

namespace Yella.Core.Identity.Domain.Dtos
{
    public class PermissionDto : EntityDto<short>
    {
        public PermissionDto()
        {
            IsChecked = false;
        }

        public string Name { get; set; }
        public string Description { get; set; }
        public string Code { get; set; }
        public string Tag { get; set; }
        public bool IsChecked { get; set; }
    }

}