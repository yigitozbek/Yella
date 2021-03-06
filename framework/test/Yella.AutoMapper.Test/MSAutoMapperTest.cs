using Yella.Aspect.PostSharp.Performances;
using Yella.AutoMapper.Extensions;
using Yella.AutoMapper.Test.Dtos;
using Yella.AutoMapper.Test.Entities;

namespace Yella.AutoMapper.Test
{
    //[TestClass]
    public class MSAutoMapperTest
    {
        //[TestMethod]
        [PerformanceAspect(1)]
        public PersonDto GetPerson()
        {
            Person person = new("Yiğit", "Özbek");
            return person.ToMapper<PersonDto>();
        }

        public List<PersonDto> GetListPerson()
        {
            var persons = new List<Person>();
            persons.Add(new("Yiğit", "Özbek"));
            persons.Add(new("TEST", "test"));
            persons.Add(new("TEST", "test"));

            return persons.ToMapper<List<PersonDto>>();
        }
    }
}