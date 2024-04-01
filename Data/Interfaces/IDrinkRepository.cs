using DrinkAndGo.Data.Models;

namespace DrinkAndGo.Data.Interfaces;

public interface IDrinkRepository
{
    IEnumerable<Drink> Drinks { get; }
    IEnumerable<Drink> PreferredDrinks { get; }
    Drink GetDrinkById(int drinkId);
}