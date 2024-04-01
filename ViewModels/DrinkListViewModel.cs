using DrinkAndGo.Data.Models;

namespace DrinkAndGo.ViewModels;

public class DrinkListViewModel
{
    public IEnumerable<Drink> Drinks { get; set; }
    public string CurrentCategory { get; set; }
}