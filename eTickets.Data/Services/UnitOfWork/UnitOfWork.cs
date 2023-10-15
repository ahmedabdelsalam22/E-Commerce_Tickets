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
        }
        public IActorRepository actorRepository { get; private set; }
    }
}
