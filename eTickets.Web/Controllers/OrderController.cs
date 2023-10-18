using eTickets.Data.Services.IRepositories;
using eTickets.Models;
using eTickets.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace eTickets.Web.Controllers
{
    public class OrderController : Controller
    {
        private readonly IMovieRepository _movieRepository;
        private readonly IShoppingCart _shoppingCart;

        public OrderController(IMovieRepository movieRepository, IShoppingCart shoppingCart)
        {
            _movieRepository = movieRepository;
            _shoppingCart = shoppingCart;
        }

        public async Task<IActionResult> Index()
        {
            List<ShoppingCartItem> shoppingCartItemsList = await _shoppingCart.GetShoppingCartItems();

            double cartTotalPrice =  _shoppingCart.GetShoppingCartTotal();

            ShoppingCartVM shoppingCartVM = new() 
            {
                ShoppingCartItems = shoppingCartItemsList,
                ShoppingCartTotal = cartTotalPrice
            };

            return View(shoppingCartVM);
        }

        public async Task<IActionResult> RemoveItemFromShoppingCart(int id)
        {

        }
        public async Task<IActionResult> AddItemToShoppingCart(int id)
        {

        }
    }
}
