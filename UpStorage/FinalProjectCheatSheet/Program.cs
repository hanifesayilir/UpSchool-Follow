// See https://aka.ms/new-console-template for more information
using FinalProjectCheatSheet;

Console.WriteLine("Hello, World!");

var order = new Order()
{
    Id = Guid.NewGuid(),
    RequestedAmount = 50,
    ProductCrawlType = ProductCrawlType.All
};



var OrderEvent = new OrderEvents()
{
    Id = Guid.NewGuid(),
    OrderId = order.Id,
    CreatedOn = DateTime.Now,
    Status = OrderStatus.BotStarted
};
