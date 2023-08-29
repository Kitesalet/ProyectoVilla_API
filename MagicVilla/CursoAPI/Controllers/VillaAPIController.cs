using CursoAPI.Data;
using CursoAPI.Models;
using CursoAPI.Models.DTOs;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace CursoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VillaAPIController : ControllerBase
    {

        

        [HttpGet]
        [Route("GetVillas")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<VillaDTO>> GetVillas()
        {

            return Ok(VillaStore.VillaList);
            
     
        }

        

      

        [HttpGet()]
        [Route("GetVilla/{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult GetVilla(int id)
        {

            if(id == 0)
            {
                return BadRequest();
            }

            var villa = VillaStore.VillaList.FirstOrDefault(e => e.Id == id);

            if(villa == null)
            {
                return NotFound();
            }

            return Ok(villa);
        }

        

      [HttpPost]
      [Route("CreateVilla")]
      [ProducesResponseType(StatusCodes.Status400BadRequest)]
      [ProducesResponseType(StatusCodes.Status500InternalServerError)]
      [ProducesResponseType(StatusCodes.Status201Created)]
      public ActionResult<VillaDTO> CreateVilla([FromBody] VillaDTO villaDto)
      {

          if (!ModelState.IsValid)
          {
              return BadRequest(ModelState);
          }

          if(villaDto == null)
          {
              return BadRequest();
          }

          if(villaDto.Id > 0)
          {
              //Esto es porque el id deberia ser 0 si se crea un objeto
              return StatusCode(StatusCodes.Status500InternalServerError);
          }

          int id = VillaStore.VillaList.OrderByDescending(e => e.Id).FirstOrDefault().Id + 1;

          villaDto.Id = id;
          VillaStore.VillaList.Add(villaDto);

          return CreatedAtRoute(id, villaDto);

      }

        

      [HttpDelete()]
      [Route("DeleteVilla/{id:int}")]
      [ProducesResponseType(StatusCodes.Status404NotFound)]
      [ProducesResponseType(StatusCodes.Status400BadRequest)]
      [ProducesResponseType(StatusCodes.Status204NoContent)]
      public IActionResult DeleteVilla(int id)
      {

          if(!ModelState.IsValid)
          {
              return BadRequest(ModelState);
          }

          if(id == 0)
          {
              return BadRequest();
          }

          var villaDto = VillaStore.VillaList.FirstOrDefault(e => e.Id == id);

          if(villaDto == null)
          {
              return NotFound();
          }

          VillaStore.VillaList.Remove(villaDto);

          return NoContent();

      }

        

      [HttpPut()]
      [Route("UpdateVilla/{id:int}")]
      [ProducesResponseType(StatusCodes.Status400BadRequest)]
      [ProducesResponseType(StatusCodes.Status204NoContent)]
      [ProducesResponseType(StatusCodes.Status404NotFound)]
      public IActionResult UpdateVilla(int id,[FromBody] VillaDTO villaDto)
      {

          if(villaDto == null || id != villaDto.Id)
          {
              return BadRequest();
          }

          var villa = VillaStore.VillaList.FirstOrDefault(e => e.Id == id);

          if(villa == null)
          {
              return NotFound();
          }

          villa.Occupancy = villaDto.Occupancy;
          villa.Sqft = villaDto.Sqft;
          villa.Name = villaDto.Name;

          return NoContent();

      }


      [HttpPatch()]
      [Route("UpdatePartialVilla/{id:int}")]
      [ProducesResponseType(StatusCodes.Status404NotFound)]
      [ProducesResponseType(StatusCodes.Status204NoContent)]
      [ProducesResponseType(StatusCodes.Status400BadRequest)]
      public IActionResult UpdatePartialVilla(int id, JsonPatchDocument<VillaDTO> patchDto )
      {

          if(patchDto == null|| id == 0)
          {
              return BadRequest();    
          }


          var villa = VillaStore.VillaList.FirstOrDefault(e => e.Id == id);

          if(villa == null)
          {
              return NotFound();
          }

          patchDto.ApplyTo(villa, ModelState);

          if (!ModelState.IsValid)
          {
              return BadRequest(ModelState);
          }

          return NoContent();



      }
        
      

    }
}
