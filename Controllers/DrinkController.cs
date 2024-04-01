using DrinkAndGo.Data.Interfaces;
using DrinkAndGo.Data.Models;
using DrinkAndGo.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace DrinkAndGo.Controllers;

public class DrinkController: Controller
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly IDrinkRepository _drinkRepository;
    public DrinkController(IDrinkRepository drinkRepository, ICategoryRepository categoryRepository)
    {
        _drinkRepository = drinkRepository;
        _categoryRepository = categoryRepository;
    }

    [HttpGet]
    public ViewResult List(string category)
    {
        string _category = category;
        IEnumerable<Drink> drinks;

        string currentCategory = string.Empty;

        if (string.IsNullOrEmpty(category))
        {
            drinks = _drinkRepository.Drinks.OrderBy(n => n.DrinkId);
            currentCategory = "All drinks";
        }
        else
        {
            if (string.Equals("Alcoholic", _category, StringComparison.OrdinalIgnoreCase))
            {
                drinks = _drinkRepository.Drinks.Where(d => d.Category.CategoryName.Equals("Alcoholic"));
            }
            else
            {
                drinks = _drinkRepository.Drinks.Where(d => d.Category.CategoryName.Equals("Non-alcoholic"));
            }

            currentCategory = category;
        }
        DrinkListViewModel drinkListViewModel = new DrinkListViewModel();
        drinkListViewModel.Drinks = _drinkRepository.Drinks;
        drinkListViewModel.CurrentCategory = currentCategory;
        
        return View(drinkListViewModel);
    }
}