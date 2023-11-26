using AutoMapper;
using eTickets.Data.Services.UnitOfWork;
using eTickets.Models;
using eTickets.Models.Dtos;
using Microsoft.AspNetCore.Authorization;
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
        [Authorize]
        public async Task<IActionResult> Index()
        {
            IEnumerable<Actor> actors = await _unitOfWork.actorRepository.GetAllAsync(tracked:false);

            List<ActorDto> actorDtos = _mapper.Map<List<ActorDto>>(actors);

            return View(actorDtos);
        }

        [HttpGet]
        [Authorize(Roles = "admin")]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [Authorize(Roles ="admin")]
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

        [Authorize(Roles ="admin")]
        [HttpGet]
        public async Task<IActionResult> Update(int? id)
        {
            if (id == null || id == 0) 
            {
                return BadRequest("id can't be null or zero");
            }
            Actor actor = await _unitOfWork.actorRepository.GetAsync(filter:x=>x.Id == id);

            ActorUpdateDto actorUpdateDto = _mapper.Map<ActorUpdateDto>(actor);
            return View(actorUpdateDto);
        }
        [Authorize(Roles ="admin")]
        [HttpPost]
        public async Task<IActionResult> Update(ActorUpdateDto updateDto) 
        {
            if (updateDto == null) 
            {
                return BadRequest();
            }
            if (ModelState.IsValid) 
            {
                Actor actorToDb = _mapper.Map<Actor>(updateDto);
                await _unitOfWork.actorRepository.Update(actorToDb);
                return RedirectToAction("Index");
            }
            return View(updateDto);
        }

        [Authorize(Roles ="admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return BadRequest("id can't be null or zero");
            }
            Actor actor = await _unitOfWork.actorRepository.GetAsync(filter: x => x.Id == id);

            await _unitOfWork.actorRepository.Delete(actor);
            return RedirectToAction("Index");
        }
    }
}
