using Microsoft.EntityFrameworkCore;

namespace DrinkAndGo.Data.Models;

public class ShoppingCart
{
    private readonly AppDbContext _dbContext;
    public ShoppingCart(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public string ShoppingCartId { get; set; }
    public List<ShoppingCartItem> ShoppingCartItems { get; set; }
    
    public static ShoppingCart GetCart(IServiceProvider services)
    {
        ISession session = services.GetRequiredService<IHttpContextAccessor>().HttpContext.Session;

        var context = services.GetService<AppDbContext>();
        string cartId = session.GetString("CartId") ?? Guid.NewGuid().ToString();
        
        session?.SetString("CartId", cartId);

        return new ShoppingCart(context) { ShoppingCartId = cartId };
    }
    
    public void AddToCart(Drink drink, int amount)
    {
        var shoppingCartItem = _dbContext.ShoppingCartItems.SingleOrDefault(s =>
            s.Drink.DrinkId == drink.DrinkId && s.ShoppingCartId == ShoppingCartId);

        if (shoppingCartItem is null)
        {
            shoppingCartItem = new ShoppingCartItem()
            {
                ShoppingCartId = ShoppingCartId,
                Drink = drink,
                Amount = 1
            };

            _dbContext.ShoppingCartItems.Add(shoppingCartItem);
        }

        else
        {
            shoppingCartItem.Amount++;
        }

        _dbContext.SaveChanges();
    }

    public int RemoveFromCart(Drink drink)
    {
        var shoppingCartItem =
            _dbContext.ShoppingCartItems.SingleOrDefault(s => s.Drink == drink && s.ShoppingCartId == ShoppingCartId);

        var localAmount = 0;

        if (shoppingCartItem.Amount > 1)
        {
            shoppingCartItem.Amount--;
            localAmount = shoppingCartItem.Amount;
        }
        else
        {
            _dbContext.ShoppingCartItems.Remove(shoppingCartItem);
        }

        _dbContext.SaveChanges();
        return localAmount;
    }

    public List<ShoppingCartItem> GetShoppingCartItems()
    {
        return ShoppingCartItems ?? (ShoppingCartItems = _dbContext.ShoppingCartItems
            .Where(c => c.ShoppingCartId == ShoppingCartId).Include(s => s.Drink).ToList());
    }

    public void ClearCart()
    {
        var cartItems = _dbContext.ShoppingCartItems.Where(cart => cart.ShoppingCartId == ShoppingCartId);

        _dbContext.ShoppingCartItems.RemoveRange(cartItems);
        _dbContext.SaveChanges();
    }

    public decimal GetShoppingCartTotal()
    {
        var total = _dbContext.ShoppingCartItems.Where(c => c.ShoppingCartId == ShoppingCartId)
            .Select(c => c.Drink.Price * c.Amount).Sum();
        return total;
    }
}