using AutoMapper;
using eTickets.Data.Services.UnitOfWork;
using eTickets.Models.Dtos;
using eTickets.Models;
using Microsoft.AspNetCore.Mvc;

namespace eTickets.Web.Controllers
{
    public class CinemaController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CinemaController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            IEnumerable<Cinema> cinemas = await _unitOfWork.cinemaRepository.GetAllAsync(tracked: false);

            List<CinemaDto> cinemaDtos = _mapper.Map<List<CinemaDto>>(cinemas);

            return View(cinemaDtos);
        }

    }
}
