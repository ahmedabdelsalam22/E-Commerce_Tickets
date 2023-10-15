using eTickets.Data.Services.UnitOfWork;
using eTickets.Models;
using Microsoft.AspNetCore.Mvc;

namespace eTickets.Web.Controllers
{
    public class ActorController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public ActorController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IActionResult> Index()
        {
            IEnumerable<Actor> actors = await _unitOfWork.actorRepository.GetAllAsync();

            return View();
        }
    }
}
