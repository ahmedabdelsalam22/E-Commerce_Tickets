using AutoMapper;
using eTickets.Data.Services.UnitOfWork;
using eTickets.Models.Dtos;
using eTickets.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace eTickets.Web.Controllers
{
    public class CategoryController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CategoryController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        [Authorize]
        public async Task<IActionResult> Index()
        {
            IEnumerable<Category> categories = await _unitOfWork.categoryRepository.GetAllAsync(tracked: false);

            List<CategoryDto> categoryDtos = _mapper.Map<List<CategoryDto>>(categories);

            return View(categoryDtos);
        }
        [HttpGet]
        [Authorize(Roles ="admin")]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [Authorize(Roles ="admin")]
        public async Task<IActionResult> Create(CategoryCreateDto createDto)
        {
            if (createDto == null)
            {
                return BadRequest();
            }
            if (ModelState.IsValid)
            {
                Category categoryToDb = _mapper.Map<Category>(createDto);
                await _unitOfWork.categoryRepository.Create(categoryToDb);
                return RedirectToAction("Index");
            }
            return View(createDto);
        }
        [HttpGet]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Update(int? id)
        {
            if (id == null || id == 0)
            {
                return BadRequest("id can't be null or zero");
            }
            Category category = await _unitOfWork.categoryRepository.GetAsync(filter: x => x.Id == id);

            CategoryUpdateDto categoryUpdateDto = _mapper.Map<CategoryUpdateDto>(category);
            return View(categoryUpdateDto);
        }
        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Update(CategoryUpdateDto updateDto)
        {
            if (updateDto == null)
            {
                return BadRequest();
            }
            if (ModelState.IsValid)
            {
                Category categoryToDb = _mapper.Map<Category>(updateDto);
                await _unitOfWork.categoryRepository.Update(categoryToDb);
                return RedirectToAction("Index");
            }
            return View(updateDto);
        }
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return BadRequest("id can't be null or zero");
            }
            Category category = await _unitOfWork.categoryRepository.GetAsync(filter: x => x.Id == id);

            await _unitOfWork.categoryRepository.Delete(category);
            return RedirectToAction("Index");
        }
    }
}
