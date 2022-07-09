namespace Yella.AutoMapper.Test.Entities;

public class Person
{
    public Person(string name, string surname)
    {
        Id = new Guid();
        Name = name;
        Surname = surname;
    }
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }

}