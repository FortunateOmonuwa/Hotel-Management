using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBooking.Domain.Models
{
    public class Hotel
    {
        public Hotel() 
        {
        
        }
        //public Hotel(string name, int starRating, string address) 
        //{
        //    Name = name;
        //    StarRating = starRating;
        //    Address = address;

        //    if (string.IsNullOrWhiteSpace(name))
        //    {
        //        throw new ArgumentNullException("You have to input a valid hotel name");
        //    }
        //}
        public int HotelId { get; set; }
        public string Name { get; set; }
        public int StarRating { get; set; }
        public string Address { get; set; }
        public string City { get; set; }    
        public string Country { get; set; }
        public string Description { get; set; }
        public ICollection<Room>? Rooms { get; set; }

    }
}
