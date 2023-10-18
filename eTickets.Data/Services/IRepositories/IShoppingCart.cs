using eTickets.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eTickets.Data.Services.IRepositories
{
    public interface IShoppingCart
    {
        Task<List<ShoppingCartItem>> GetShoppingCartItems();
        double GetShoppingCartTotal();
        Task AddItemToCart(Movie movie);
        Task RemoveItemFromCart(Movie movie);
    }
}
