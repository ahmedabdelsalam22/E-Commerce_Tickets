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
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(ProducerCreateDto createDto)
        {
            if (createDto == null)
            {
                return BadRequest();
            }
            if (ModelState.IsValid)
            {
                Producer producerToDb = _mapper.Map<Producer>(createDto);
                await _unitOfWork.producerRepository.Create(producerToDb);
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
            Producer producer = await _unitOfWork.producerRepository.GetAsync(filter: x => x.Id == id);

            ProducerUpdateDto producerUpdateDto = _mapper.Map<ProducerUpdateDto>(producer);
            return View(producerUpdateDto);
        }
        [HttpPost]
        public async Task<IActionResult> Update(ProducerUpdateDto updateDto)
        {
            if (updateDto == null)
            {
                return BadRequest();
            }
            if (ModelState.IsValid)
            {
                Producer producerToDb = _mapper.Map<Producer>(updateDto);
                await _unitOfWork.producerRepository.Update(producerToDb);
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
            Producer producer = await _unitOfWork.producerRepository.GetAsync(filter: x => x.Id == id);

            await _unitOfWork.producerRepository.Delete(producer);
            return RedirectToAction("Index");
        }
    }
}
