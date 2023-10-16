using eTickets.Data.Services.IRepositories;
using eTickets.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eTickets.Data.Services.Repositories
{
    public class CinemaRepository : GenericRepository<Cinema>, ICinemaRepository
    {
        private readonly ApplicationDbContext _context;
        public CinemaRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task Update(Cinema cinema)
        {
            _context.Update(cinema);
            await _context.SaveChangesAsync();
        }
    }
}
