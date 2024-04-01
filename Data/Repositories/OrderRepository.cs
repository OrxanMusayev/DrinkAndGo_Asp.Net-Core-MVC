using DrinkAndGo.Data.Interfaces;
using DrinkAndGo.Data.Models;

namespace DrinkAndGo.Data.Repositories;

public class OrderRepository: IOrderRepository
{
    private readonly AppDbContext _dbContext;
    private readonly ShoppingCart _shoppingCart;

    public OrderRepository(ShoppingCart shoppingCart, AppDbContext dbContext)
    {
        _shoppingCart = shoppingCart;
        _dbContext = dbContext;
    }

    public void CreateOrder(Order order)
    {
        order.OrderPlaced = DateTime.Now;
        _dbContext.Orders.Add(order);
        _dbContext.SaveChanges();
        var shoppingCartItems = _shoppingCart.ShoppingCartItems;

        foreach (var item in shoppingCartItems)
        {
            var orderDetail = new OrderDetails()
            {
                Amount = item.Amount,
                DrinkId = item.Drink.DrinkId,
                OrderId = order.OrderId,
                Price = item.Drink.Price
            };
            _dbContext.OrderDetails.Add(orderDetail);
        }

        _dbContext.SaveChanges();
    }
}