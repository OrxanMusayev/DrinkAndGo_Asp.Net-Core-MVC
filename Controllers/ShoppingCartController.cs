using DrinkAndGo.Data.Interfaces;
using DrinkAndGo.Data.Models;
using DrinkAndGo.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace DrinkAndGo.Controllers;

public class ShoppingCartController: Controller
{
    private readonly IDrinkRepository _drinkRepository;
    private readonly ShoppingCart _shoppingCart;
    public ShoppingCartController(ShoppingCart shoppingCart, IDrinkRepository drinkRepository)
    {
        _shoppingCart = shoppingCart;
        _drinkRepository = drinkRepository;
    }

    public ViewResult Index()
    {
        var items = _shoppingCart.GetShoppingCartItems();
        _shoppingCart.ShoppingCartItems = items;

        var scVM = new ShoppingCartViewModel()
        {
            ShoppingCart = _shoppingCart,
            ShoppingCartTotal = _shoppingCart.GetShoppingCartTotal()
        };

        return View(scVM);
    }

    public RedirectToActionResult AddToShoppingCart(int drinkId)
    {
        var selectedDrink = _drinkRepository.Drinks.FirstOrDefault(p => p.DrinkId == drinkId);
        if (selectedDrink != null)
        {
            _shoppingCart.AddToCart(selectedDrink, 1);
        }

        return RedirectToAction("Index");
    }

    public RedirectToActionResult RemoveFromShoppingCart(int drinkId)
    {
        var selectedDrink = _drinkRepository.Drinks.FirstOrDefault(p => p.DrinkId == drinkId);
        if (selectedDrink != null)
        {
            _shoppingCart.RemoveFromCart(selectedDrink);
        }

        return RedirectToAction("Index");
    }
}