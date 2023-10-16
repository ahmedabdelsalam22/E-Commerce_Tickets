using eTickets.Data.Services.IRepositories;
using eTickets.Data.Services.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eTickets.Data.Services.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            actorRepository = new ActorRepository(_context);
            producerRepository = new ProducerRepository(_context);
            cinemaRepository = new CinemaRepository(_context);
            categoryRepository = new CategoryRepository(_context);
        }
        public IActorRepository actorRepository { get; private set; }
        public IProducerRepository producerRepository { get; private set; }
        public ICinemaRepository cinemaRepository { get; private set; }
        public ICategoryRepository categoryRepository { get; private set; }
    }
}
