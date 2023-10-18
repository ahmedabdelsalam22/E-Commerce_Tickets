using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eTickets.Models.ViewModels
{
    public class Movie_Actors_VM
    {
        public Movie Movie { get; set; }
        public List<Actor> Actors { get; set; }
    }
}
