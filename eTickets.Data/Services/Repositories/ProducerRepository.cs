using eTickets.Data.Services.IRepositories;
using eTickets.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eTickets.Data.Services.Repositories
{
    public class ProducerRepository : GenericRepository<Producer>, IProducerRepository
    {
        private readonly ApplicationDbContext _context;
        public ProducerRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task Update(Producer producer)
        {
            _context.Update(producer);
            await _context.SaveChangesAsync();
        }
    }
}
