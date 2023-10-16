using AutoMapper;
using eTickets.Data.Services.UnitOfWork;
using Microsoft.AspNetCore.Mvc;

namespace eTickets.Web.Controllers
{
    public class MovieController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public MovieController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
