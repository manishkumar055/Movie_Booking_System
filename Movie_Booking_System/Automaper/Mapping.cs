using AutoMapper;
using Movie_Booking_System.Models;
using Movie_Booking_System.RequestDtos;

namespace Movie_Booking_System.Automaper
{
    public class Mapping:Profile
    {
        public Mapping()
        {
            CreateMap<Theater,TheaterDto>().ReverseMap();
            CreateMap<TicketReturnDto,Ticket> ().ReverseMap();
        }
    }
}
