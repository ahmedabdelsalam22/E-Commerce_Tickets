using AutoMapper;
using eTickets.Data.Services.UnitOfWork;
using eTickets.Models.Dtos;
using eTickets.Models;
using Microsoft.AspNetCore.Mvc;

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

        public async Task<IActionResult> Index()
        {
            IEnumerable<Category> categories = await _unitOfWork.categoryRepository.GetAllAsync(tracked: false);

            List<CategoryDto> categoryDtos = _mapper.Map<List<CategoryDto>>(categories);

            return View(categoryDtos);
        }

    }
}
