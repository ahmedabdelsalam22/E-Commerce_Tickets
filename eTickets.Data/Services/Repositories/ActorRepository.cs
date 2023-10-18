using eTickets.Data.Services.IRepositories;
using eTickets.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eTickets.Data.Services.Repositories
{
    public class ActorRepository : GenericRepository<Actor>, IActorRepository
    {
        private readonly ApplicationDbContext _context;
        public ActorRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public List<Actor> GetActorsByMovieId(int? movieId)
        {
            var actors = _context.ActorMovies.Where(x => x.MovieId == movieId).Select(x=>x.Actor).ToList();
            return actors;
        }

        public async Task Update(Actor actor)
        {
            _context.Update(actor);
            await _context.SaveChangesAsync();
        }
    }
}
