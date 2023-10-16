using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eTickets.Models.Dtos
{
    public class ActorCreateDto
    {
        [Required]
        public string ProfilePictureUrl { get; set; }
        [Required]
        public string FullName { get; set; }
        [Required]
        public string Bio { get; set; }
    }
}
