namespace Yella.Identity.Models;

public class PermissionModel 
{
    public short Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string Code { get; set; }
    public string Tag { get; set; }
    public bool IsChecked { get; set; }
}