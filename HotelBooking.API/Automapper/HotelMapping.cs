using AutoMapper;
using HotelBooking.API.DTO;
using HotelBooking.Domain.Models;

namespace HotelBooking.API.Automapper
{
    public class HotelMapping : Profile
    {
        public HotelMapping() 
        {
            CreateMap<HotelCreateDTO, Hotel>();
            CreateMap<Hotel, HotelGetDTO>();
        
        }
    }
}
