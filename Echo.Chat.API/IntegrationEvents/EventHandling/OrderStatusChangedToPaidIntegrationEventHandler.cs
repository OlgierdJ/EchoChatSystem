//using Echo.Domain.EntityFrameworkCore.EFCORE;
//using eShop.Catalog.API.IntegrationEvents.Events;

//namespace eShop.Catalog.API.IntegrationEvents.EventHandling;

//public class OrderStatusChangedToPaidIntegrationEventHandler(
//    EchoDbContext dbContext,
//    ILogger<OrderStatusChangedToPaidIntegrationEventHandler> logger) :
//    IIntegrationEventHandler<OrderStatusChangedToPaidIntegrationEvent>
//{
//    public async Task Handle(OrderStatusChangedToPaidIntegrationEvent @event)
//    {
//        logger.LogInformation("Handling integration event: {IntegrationEventId} - ({@IntegrationEvent})", @event.Id, @event);

//        //we're not blocking stock/inventory
//        foreach (var orderStockItem in @event.OrderStockItems)
//        {
//            var catalogItem = dbContext.CatalogItems.Find(orderStockItem.ProductId);

//            catalogItem.RemoveStock(orderStockItem.Units);
//        }

//        await dbContext.SaveChangesAsync();
//    }
//}
