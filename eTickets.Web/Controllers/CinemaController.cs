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
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(CinemaCreateDto createDto)
        {
            if (createDto == null)
            {
                return BadRequest();
            }
            if (ModelState.IsValid)
            {
                Cinema cinemaToDb = _mapper.Map<Cinema>(createDto);
                await _unitOfWork.cinemaRepository.Create(cinemaToDb);
                return RedirectToAction("Index");
            }
            return View(createDto);
        }

        [HttpGet]
        public async Task<IActionResult> Update(int? id)
        {
            if (id == null || id == 0)
            {
                return BadRequest("id can't be null or zero");
            }
            Cinema cinema = await _unitOfWork.cinemaRepository.GetAsync(filter: x => x.Id == id);

            CinemaUpdateDto cinemaUpdateDto = _mapper.Map<CinemaUpdateDto>(cinema);
            return View(cinemaUpdateDto);
        }
        [HttpPost]
        public async Task<IActionResult> Update(CinemaUpdateDto updateDto)
        {
            if (updateDto == null)
            {
                return BadRequest();
            }
            if (ModelState.IsValid)
            {
                Cinema cinemaToDb = _mapper.Map<Cinema>(updateDto);
                await _unitOfWork.cinemaRepository.Update(cinemaToDb);
                return RedirectToAction("Index");
            }
            return View(updateDto);
        }
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return BadRequest("id can't be null or zero");
            }
            Cinema cinema = await _unitOfWork.cinemaRepository.GetAsync(filter: x => x.Id == id);

            await _unitOfWork.cinemaRepository.Delete(cinema);
            return RedirectToAction("Index");
        }
    }
}
