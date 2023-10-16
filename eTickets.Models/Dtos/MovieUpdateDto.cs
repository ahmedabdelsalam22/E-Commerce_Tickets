using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eTickets.Models.Dtos
{
    public class MovieUpdateDto
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public double Price { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public DateTime EndDate { get; set; }
        [Required]
        public int CinemaId { get; set; }
        [Required]
        public int ProducerId { get; set; }
        [Required]
        public int CategoryId { get; set; }
        public Cinema? Cinema { get; set; }
        public Producer? Producer { get; set; }
        public Category? Category { get; set; }


    }
}
