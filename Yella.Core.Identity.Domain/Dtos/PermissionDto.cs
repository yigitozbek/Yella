using Archseptia.Core.Domain.Dto;

namespace Archseptia.Core.Identity.Service.Dtos
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