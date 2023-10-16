using eTickets.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eTickets.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Actor> Actors { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<ActorMovie> ActorMovies { get; set; }
        public DbSet<Cinema> Cinemas { get; set; }
        public DbSet<Producer> Producers { get; set; }
        public DbSet<Category> Categories { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<ActorMovie>().HasKey(x => new
            {
                x.MovieId,
                x.ActorId
            });

            modelBuilder.Entity<ActorMovie>().HasOne(x => x.Movie).WithMany(x => x.ActorsMovies).HasForeignKey(x => x.MovieId);

            modelBuilder.Entity<ActorMovie>().HasOne(x => x.Actor).WithMany(x => x.ActorsMovies).HasForeignKey(x=>x.ActorId);

            base.OnModelCreating(modelBuilder);
        }

    }
}
