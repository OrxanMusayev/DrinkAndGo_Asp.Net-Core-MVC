using DrinkAndGo.Data.Models;

namespace DrinkAndGo.Data.Interfaces;

public interface ICategoryRepository
{
    IEnumerable<Category> Categories { get; }
}