using DrinkAndGo.Data.Models;

namespace DrinkAndGo.ViewModels;

public class ShoppingCartViewModel
{
    public ShoppingCart ShoppingCart { get; set; }
    public decimal ShoppingCartTotal { get; set; }
}