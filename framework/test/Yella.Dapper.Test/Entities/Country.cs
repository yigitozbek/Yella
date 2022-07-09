namespace Yella.Dapper.Test.Entities;

public class Country
{
    public Country(string name, string code)
    {
        Name = name;
        Code = code;
    }

    public Country() { }

    public int Id { get; set; }
    public string Name { get; set; }
    public string Code { get; set; }

}