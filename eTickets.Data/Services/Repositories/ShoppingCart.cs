using eTickets.Data.Services.IRepositories;
using eTickets.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Text;

namespace eTickets.Data.Services.Repositories
{
    public class ShoppingCart
    {
        private readonly ApplicationDbContext _context;
        public string ShoppingCartId { get; set; }
        public List<ShoppingCartItem> ShoppingCartItems { get; set; }

        public ShoppingCart(ApplicationDbContext context)
        {
            _context = context;
        }

        // to set value to "ShoppingCartId"
        public static ShoppingCart GetShoppingCart(IServiceProvider services)
        {
            ISession session = services.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;
            var context = services.GetService<ApplicationDbContext>();

            string cartId = session.GetString("CartId") ?? Guid.NewGuid().ToString();
            session.SetString("CartId", cartId);

            return new ShoppingCart(context) { ShoppingCartId = cartId };
        }

        public async Task AddItemToCart(Movie movie)
        {
            var shoppingCartItemFromDb = await _context.ShoppingCartItems.FirstOrDefaultAsync(x => x.Movie.Id == movie.Id && x.ShoppingCartId == ShoppingCartId);

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

        public List<ShoppingCartItem> GetShoppingCartItems()
        {
            if (ShoppingCartItems != null)
            {
                return ShoppingCartItems;
            }

            ShoppingCartItems = _context.ShoppingCartItems.Where(x => x.ShoppingCartId == ShoppingCartId).Include(x => x.Movie).ToList();

            return ShoppingCartItems;
        }

        public double GetShoppingCartTotal()
        {
            var total = _context.ShoppingCartItems.Where(x => x.ShoppingCartId == ShoppingCartId).Select(x => x.Movie.Price * x.Amount).Sum();
            return total;
        }

        public async Task RemoveItemFromCart(Movie movie)
        {
            var shoppingCartItemFromDb = await _context.ShoppingCartItems.Where(x => x.Movie.Id == movie.Id && x.ShoppingCartId == ShoppingCartId).FirstOrDefaultAsync();

            if (shoppingCartItemFromDb != null)
            {
                if (shoppingCartItemFromDb.Amount > 1)
                {
                    shoppingCartItemFromDb.Amount--;
                }
                else
                {
                    _context.ShoppingCartItems.Remove(shoppingCartItemFromDb);
                }
            }
            await _context.SaveChangesAsync();
        }

        public async Task ClearShoppingCartAsync()
        {
            var items = await _context.ShoppingCartItems.Where(n => n.ShoppingCartId == ShoppingCartId).ToListAsync();
            _context.ShoppingCartItems.RemoveRange(items);
            await _context.SaveChangesAsync();
        }
    }
}
