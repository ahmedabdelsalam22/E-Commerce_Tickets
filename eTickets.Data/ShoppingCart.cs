using eTickets.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eTickets.Data
{
    public class ShoppingCart
    {
        private readonly ApplicationDbContext _context;

        public string ShoppingCartId { get; set; }

        public ShoppingCart(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<ShoppingCartItem> ShoppingCartItems { get; set; }

        public async Task<List<ShoppingCartItem>> GetShoppingCartItems()
        {
            if (ShoppingCartItems != null)
            {
                return ShoppingCartItems;
            }

            ShoppingCartItems = await _context.ShoppingCartItems.Where(x => x.ShoppingCartId == ShoppingCartId).Include(x => x.Movie).ToListAsync();

            return ShoppingCartItems;
        }

        public double GetShoppingCartTotal()
        {
            var total = _context.ShoppingCartItems.Where(x => x.ShoppingCartId == ShoppingCartId).Select(x => x.Movie.Price * x.Amount).Sum();
            return total;
        }

        public async Task AddItemToCart(Movie movie)
        {
            var shoppingCartItemFromDb =await _context.ShoppingCartItems.Where(x => x.Movie.Id == movie.Id && x.ShoppingCartId == ShoppingCartId).FirstOrDefaultAsync();

            if (shoppingCartItemFromDb == null)
            {
                var ShoppingCartItemToAdd = new ShoppingCartItem()
                {
                    ShoppingCartId = ShoppingCartId,
                    Movie = movie,
                    Amount = 1
                };

               await _context.ShoppingCartItems.AddAsync(ShoppingCartItemToAdd);
            }
            else 
            {
                shoppingCartItemFromDb.Amount++;
            }
           await _context.SaveChangesAsync();
        }
    }
}
