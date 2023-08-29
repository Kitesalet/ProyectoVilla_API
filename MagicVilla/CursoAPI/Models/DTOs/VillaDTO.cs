using System.ComponentModel.DataAnnotations;

namespace CursoAPI.Models.DTOs
{
    public record VillaDTO 
    {

        public int Id { get; set; }

        [Required]
        [MaxLength(30)]
        public string Name { get; set; }    

        public int Occupancy { get; set; }

        public int Sqft { get; set; }
    }


    
}
