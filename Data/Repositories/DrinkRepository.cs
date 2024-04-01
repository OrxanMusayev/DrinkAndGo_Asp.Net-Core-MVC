using DrinkAndGo.Data.Interfaces;
using DrinkAndGo.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace DrinkAndGo.Data.Repositories;

public class DrinkRepository: IDrinkRepository
{
    private readonly AppDbContext _dbContext;

    public DrinkRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public IEnumerable<Drink> Drinks => _dbContext.Drinks.Include(c => c.Category);

    public IEnumerable<Drink> PreferredDrinks =>
        _dbContext.Drinks.Where(p => p.IsPreferredDrink).Include(c => c.Category);
    
    public Drink GetDrinkById(int drinkId)
    {
        throw new NotImplementedException();
    }
}