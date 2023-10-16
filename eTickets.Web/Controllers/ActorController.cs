using AutoMapper;
using eTickets.Data.Services.UnitOfWork;
using eTickets.Models;
using eTickets.Models.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace eTickets.Web.Controllers
{
    public class ActorController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ActorController(IUnitOfWork unitOfWork,IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            IEnumerable<Actor> actors = await _unitOfWork.actorRepository.GetAllAsync();

            List<ActorDto> actorDtos = _mapper.Map<List<ActorDto>>(actors);

            return View(actorDtos);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(ActorCreateDto createDto)
        {
            if(createDto == null) 
            {
                return BadRequest();
            }
            if (ModelState.IsValid) 
            {
                Actor actorToDb = _mapper.Map<Actor>(createDto);
                await _unitOfWork.actorRepository.Create(actorToDb);
                return RedirectToAction("Index");
            }
            return View(createDto);
        }
    }
}
