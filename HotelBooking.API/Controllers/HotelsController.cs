using Microsoft.AspNetCore.Mvc;
using HotelBooking.Domain.Models;
using HotelBooking.DataAccessLayer;
using Microsoft.EntityFrameworkCore;
using HotelBooking.API.DTO;
using AutoMapper;

namespace HotelBooking.API.Controllers
{
    /// <summary>
    /// When a request comes in for hotels, the IActionResult GetRooms method is triggered. 
    /// This route tells the api to look through this code and set any class inheriting from controller class as the controller for the resource/Model/Entity 
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class HotelsController : Controller
    {
       
        private readonly ILogger<HotelsController> _logger;
        private readonly HotelContext _hotelContext;
        private readonly IMapper _mapper;
        public HotelsController(ILogger<HotelsController>logger, HotelContext hotel, IMapper mapper)
        {
          
            _logger = logger;
            _hotelContext = hotel;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> CreateHotel([FromBody] HotelCreateDTO hotel)
        {
            try
            {
               var domainHotel = _mapper.Map<Hotel>(hotel);

                var hotelCheck = await _hotelContext.Hotels.AnyAsync(x => x.Name == domainHotel.Name);
                if (hotelCheck)
                {
                    return BadRequest("Hotel Already Exists");
                }
                else
                {
                    _hotelContext.Hotels.Add(domainHotel);
                    await _hotelContext.SaveChangesAsync();

                    var hoteldto = _mapper.Map<HotelGetDTO>(domainHotel);
                    return CreatedAtAction(nameof(GetHotelByID), new {id = domainHotel.HotelId}, hoteldto);
                }
              
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating new hotel");
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
         
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateHotel([FromBody] HotelCreateDTO updateHotel, int id)
        {
            try
            {
                var hotelCheck = await _hotelContext.Hotels.FirstOrDefaultAsync(x => x.HotelId == id);
             
                if (hotelCheck != null)
                {
                    var update = _mapper.Map<Hotel>(updateHotel);
                    _hotelContext.Hotels.Update(update);
                    await _hotelContext.SaveChangesAsync();
                }
                else
                {
                    return NotFound("Hotel Doesn't Exist");
                }
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError("Error updating Hotel", ex);
                return StatusCode(StatusCodes.Status451UnavailableForLegalReasons, ex);
            }
          

        }

        [HttpGet]
        public async Task<IActionResult> GetAllHotels()
        {
            try
            {
                var hotels = await _hotelContext.Hotels.ToListAsync();
                var hotelsGet = _mapper.Map<List<HotelGetDTO>>(hotels);
                return Ok(hotelsGet);
            }
            catch (Exception ex)
            {
                _logger.LogError("An error has occured");
                return StatusCode(StatusCodes.Status503ServiceUnavailable, ex);

            }
            
        }

        [HttpGet]
        [Route("{id}")]
        public async Task <IActionResult> GetHotelByID(int id)
        {
            try
            {
                var hotel = await _hotelContext.Hotels.FirstOrDefaultAsync(x => x.HotelId == id);
                var hotelGet = _mapper.Map<HotelGetDTO>(hotel);
                if (hotel == null)
                {
                    return NotFound("Hotel doesn't exist");
                }
                else
                {
                    return Ok(hotelGet);
                }
                
                
            }
            catch(Exception ex)
            {
                _logger.LogError("An error has occured");
                return BadRequest(ex);
            }
            
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteHotel(int id)
        {
            try
            {
                var idCheck = await _hotelContext.Hotels.FindAsync(id);
                if (idCheck == null)
                {

                    _logger.LogError("An error has occured");
                    return NotFound("Hotel Doesn't Exist");                    
                }
                else
                {
                    _hotelContext.Hotels.Remove(idCheck);
                    await _hotelContext.SaveChangesAsync();
                    return NoContent();
                }
                
            }
            catch(Exception ex)
            {
                return BadRequest(ex);
            }                                                     
            

        }

    }
}
