using AutoMapper;
using eTickets.Data.Services.UnitOfWork;
using eTickets.Models;
using eTickets.Models.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace eTickets.Web.Controllers
{
    public class ProducerController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProducerController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<IActionResult> Index()
        {
            IEnumerable<Producer> producers = await _unitOfWork.producerRepository.GetAllAsync(tracked:false);

            List<ProducerDto> producerDtos = _mapper.Map<List<ProducerDto>>(producers);
            return View(producerDtos);
        }
    }
}
