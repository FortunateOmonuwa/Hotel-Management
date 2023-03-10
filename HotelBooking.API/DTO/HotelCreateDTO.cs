namespace HotelBooking.API.DTO
{
    public class HotelCreateDTO
    {
        public string Name { get; set; }
        public int StarRating { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string Description { get; set; }
    }
}
