using CursoAPI.Models.DTOs;

namespace CursoAPI.Data
{
    public static class VillaStore
    {

        
            public static List<VillaDTO> VillaList { get; set; } = new List<VillaDTO>
            {
                new VillaDTO{Id = 1, Name = "Arrombado", Occupancy = 4, Sqft= 900},
                new VillaDTO{ Id = 2, Name = "Paradiso" , Occupancy = 2, Sqft = 500}
            }
            ;

}
}
