using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace DrinkAndGo.Data.Models;

public class OrderDetails
{
    [Key] public int OrderDetailId { get; set; }
    public int OrderId { get; set; }
    public int DrinkId { get; set; }
    public int Amount { get; set; }
    public decimal Price { get; set; }
    public virtual Drink Drink { get; set; }
    public virtual Order Order { get; set; }
}