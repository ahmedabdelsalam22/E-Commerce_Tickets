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

        public List<ShoppingCartItem> GetShoppingCartItems()
        {
            if (ShoppingCartItems != null) 
            {
                return ShoppingCartItems;
            }

            ShoppingCartItems =  _context.ShoppingCartItems.Where(x=>x.ShoppingCartId == ShoppingCartId).Include(x=>x.Movie).ToList();
            
            return ShoppingCartItems;
        }

        public double GetShoppingCartTotal() 
        {
            var total = _context.ShoppingCartItems.Where(x => x.ShoppingCartId == ShoppingCartId).Select(x => x.Movie.Price * x.Amount).Sum();
            return total;
        }
    }
}
