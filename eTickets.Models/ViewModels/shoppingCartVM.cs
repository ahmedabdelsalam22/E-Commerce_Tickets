using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eTickets.Models.ViewModels
{
    public class ShoppingCartVM
    {
        public List<ShoppingCartItem> ShoppingCartItems { get; set; }
        public double ShoppingCartTotal { get; set; }
    }
}
