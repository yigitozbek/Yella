using Yella.Aspect.PostSharp.Performances;
using Yella.AutoMapper.Extensions;
using Yella.AutoMapper.Test.Dtos;
using Yella.AutoMapper.Test.Entities;

namespace Yella.AutoMapper.Test
{
    //[TestClass]
    public class UnitTestAutoMapper
    {
        //[TestMethod]
        [PerformanceAspect(1)]
        public PersonDto GetPerson()
        {
            Thread.Sleep(15000);
            Person person = new("Yiğit", "Özbek");
            return person.ToMapper<PersonDto>();
        }
    }
}