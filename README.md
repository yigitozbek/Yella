## What is Yella?

* Yella Framework provides you with an infrastructure by providing you with the latest technology in a fast and efficient way while you are making a web project.

## Terse

* Instead of constantly manually updating the data such as the date of deletion, date of insertion and date of update in the table you created, assigns the value without your intervention.

* If there is a deleted data, it ensures that the data is not returned without writing any code instead of constantly writing to the condition so that the data does not come.

```c#
public class Order : FullAuditedEntity<Guid> 
{
    
  ...

}
```

* When you want to use the data in the table, it allows you to use them quickly and simply instead of writing cumbersome codes in the DAL layer.
* It allows you to pass the data you want to include very easily.
```c#
var query = (await _orderRepository.WithIncludeAsync(x => x.Customer, x => x.SalesRepresentative));
```

```c#
public class OrderApplicationService : IOrderService
{
    private readonly IRepository<Order, Guid> _orderRepository;

    public OrderApplicationService(IRepository<Order, Guid> orderRepository)
    {
        _orderRepository = orderRepository;
    }

    ...
}
```
* It allows you to write more readable and manageable code by adding Attributes such as Validation, Transaction.
```c#
[TransactionAspect(AspectPriority = 2)]
[FluentValidationAspect(typeof(CreateOrUpdateDemandValidator), AspectPriority = 1)]
public async Task<IDataResult<DemandDto>> UpdateAsync(CreateOrUpdateDemandDto input)
{

  var demand = await _demandRepository.GetAsync(x => x.Id == input.Id, x => x.DemandSalesRepresentatives);
  ...

}
```

## NUGET

* You can downlaod it from [Nuget](https://www.nuget.org/profiles/yigitozbek)



## Support US

* If you like the Yella, you can become a sponsor.

Donate using cryptocurrencies:
- ```BTC: 39Hse93PpgJCko4XdwKt5modXp1oKUFXcX```
- ```ETH: 0x21D20b052e9546a0Eb267B08828d185250767976```






