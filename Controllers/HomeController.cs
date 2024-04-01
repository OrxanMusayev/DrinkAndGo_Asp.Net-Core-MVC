using DrinkAndGo.Data.Interfaces;
using DrinkAndGo.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace DrinkAndGo.Controllers;

public class HomeController : Controller
{
    private readonly IDrinkRepository _drinkRepository;
    public HomeController(IDrinkRepository drinkRepository)
    {
        _drinkRepository = drinkRepository;
    }
    // GET
    public ViewResult Index()
    {
        var homeViewModel = new HomeViewModel()
        {
            PreferredDrinks = _drinkRepository.PreferredDrinks
        };
        
        return View(homeViewModel);
    }
}