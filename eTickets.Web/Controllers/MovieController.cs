using AutoMapper;
using eTickets.Data.Services.UnitOfWork;
using eTickets.Models;
using eTickets.Models.Dtos;
using Microsoft.AspNetCore.Http.HttpResults;
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

        public async Task<IActionResult> Index()
        {
            IEnumerable<Movie> movies = await _unitOfWork.movieRepository.GetAllAsync();

            List<MovieDto> movieDtos = _mapper.Map<List<MovieDto>>(movies);  
            return View(movieDtos);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        public async Task<IActionResult> Create(MovieCreateDto createDto)
        {
            if (createDto == null) 
            {
                return BadRequest();
            }
            
            if (ModelState.IsValid)
            {
                // related entities -- cinema,producer,category
                Cinema cinema = await _unitOfWork.cinemaRepository.GetAsync(filter:x=>x.Id == createDto.CinemaId);
                Producer producer = await _unitOfWork.producerRepository.GetAsync(filter:x=>x.Id == createDto.ProducerId);
                Category category = await _unitOfWork.categoryRepository.GetAsync(filter:x=>x.Id == createDto.CategoryId);

                // check if those related entities exists in db or not
                if (cinema == null) 
                {
                    ModelState.AddModelError("","no cinemas found with this id");
                    return BadRequest(ModelState);
                }

                createDto.Cinema = cinema;

                if (producer == null) 
                {
                    ModelState.AddModelError("", "no producers found with this id");
                    return BadRequest(ModelState);
                }

                createDto.Producer = producer;

                if (category == null) 
                {
                    ModelState.AddModelError("", "no categories found with this id");
                    return BadRequest(ModelState);
                }
                
                createDto.Category = category;

                // after we make sured that related entities exists in db .. we will create movie
                Movie movieToDb = _mapper.Map<Movie>(createDto);
                await _unitOfWork.movieRepository.Create(movieToDb);
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
            Movie movie = await _unitOfWork.movieRepository.GetAsync(filter:x=>x.Id == id);

            MovieUpdateDto movieUpdateDto = _mapper.Map<MovieUpdateDto>(movie);
            return View(movieUpdateDto);
        }
        [HttpPost]
        public async Task<IActionResult> Update(MovieUpdateDto updateDto) 
        {
            if (updateDto == null)
            {
                return BadRequest();
            }
            if (ModelState.IsValid)
            {
                // related entities -- cinema,producer,category
                Cinema cinema = await _unitOfWork.cinemaRepository.GetAsync(filter: x => x.Id == updateDto.CinemaId);
                Producer producer = await _unitOfWork.producerRepository.GetAsync(filter: x => x.Id == updateDto.ProducerId);
                Category category = await _unitOfWork.categoryRepository.GetAsync(filter: x => x.Id == updateDto.CategoryId);

                // check if those related entities exists in db or not
                if (cinema == null)
                {
                    ModelState.AddModelError("", "no cinemas found with this id");
                    return BadRequest(ModelState);
                }

                updateDto.Cinema = cinema;

                if (producer == null)
                {
                    ModelState.AddModelError("", "no producers found with this id");
                    return BadRequest(ModelState);
                }

                updateDto.Producer = producer;

                if (category == null)
                {
                    ModelState.AddModelError("", "no categories found with this id");
                    return BadRequest(ModelState);
                }

                updateDto.Category = category;

                // after we make sured that related entities exists in db .. we will create movie
                Movie movieToDb = _mapper.Map<Movie>(updateDto);
                await _unitOfWork.movieRepository.Update(movieToDb);
                return RedirectToAction("Index");
            }
            return View("Index");
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return BadRequest("id can't be null or zero");
            }
            Movie movie = await _unitOfWork.movieRepository.GetAsync(filter: x => x.Id == id);

            await _unitOfWork.movieRepository.Delete(movie);
            return RedirectToAction("Index");
        }
    }
}
