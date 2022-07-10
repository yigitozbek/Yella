using Yella.Domain.Entities;

namespace Yella.AutoMapper.Test.Entities;

public class Person : FullAuditedEntity<Guid>
{
    public Person(string name, string surname)
    {
        Id = new Guid();
        Name = name;
        Surname = surname;
    }
    public string Name { get; set; }
    public string Surname { get; set; }

}