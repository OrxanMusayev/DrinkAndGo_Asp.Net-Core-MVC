using DrinkAndGo.Data.Models;

namespace DrinkAndGo.ViewModels;

public class HomeViewModel
{
    public IEnumerable<Drink> PreferredDrinks { get; set; }
}