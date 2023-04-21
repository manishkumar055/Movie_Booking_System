using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Movie_Booking_System.Context;
using Movie_Booking_System.Models;
using Movie_Booking_System.RequestDtos;
using System.Security.Claims;

namespace Movie_Booking_System.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class TheaterController : ControllerBase
    {
        private readonly MBSContext _context;
        private readonly IMapper _mapper;
        public TheaterController(IMapper mapper1, MBSContext mBSContext)
        {
            _context = mBSContext;
            _mapper = mapper1;
        }
        [HttpPost]
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> AddTheater(TheaterDto request)
        {
            var theaterObj = await _context.Theaters.FirstOrDefaultAsync(x => x.Name == request.Name && x.Address == request.Address);
            if (theaterObj != null)
            {
                throw new Exception($"This Theater {request.Name} Is Already Added for Address {request.Address}");

            }
            await _context.Theaters.AddAsync(new Theater()
            {
                Name = request.Name,
                Address = request.Address,
                PhoneNumber = request.PhoneNumber,
                Screen = request.Screen,
                SeatingCapacity = request.SeatingCapacity,

            });
            await _context.SaveChangesAsync();
            return Ok($"Theater Name {request.Name} Added for address {request.Address}");
        }
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllTheater()
        {
            return Ok(_mapper.Map<List<Theater>, List<TheaterDto>>(await _context.Theaters.ToListAsync()));
        }

        [HttpGet]
        [Authorize(Roles = "Admin,User")]
        public async Task<IActionResult> GetShowTimes(int theaterId)
        {
            var theaterObj = await _context.Theaters.Where(x => x.Id == theaterId).Include(x => x.Showtimes).FirstOrDefaultAsync();
            if (theaterObj == null)
            {
                return NotFound("Theater not Found");

            }
            return Ok(theaterObj.Showtimes);

        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddMovie(MovieDto request)
        {
            var theaterObj = await _context.Theaters.Where(x => x.Id == request.TheaterId).Include(x => x.Movies).ThenInclude(x => x.Movie).FirstOrDefaultAsync();
            if (theaterObj == null)
            {
                throw new Exception($"theater not found for  Id {request.TheaterId} not found");
            }
            theaterObj.Movies.Add(new MovieTheaterHandler
            {
                Movie = new Movie()
                {
                    Synopsis = request.Synopsis,
                    Cast = request.Cast,
                    Duration = request.Duration,
                    Genre = request.Genre,
                    Rating = request.Rating,
                    ReleaseDate = request.ReleaseDate,

                }

            });
            await _context.SaveChangesAsync();
            return Ok("Movie Added to theater ");
        }
        [HttpGet]
        [Authorize(Roles = "Admin,User")]
        public async Task<IActionResult> GetMovie(int movieId)
        {
            var movieObj = await _context.Movies.FindAsync(movieId);
            if (movieObj == null)
            {
                return NotFound("Movie Not Found");
            }
            return Ok(movieObj);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddShowtime(ShowtimeDto request)
        {
            try
            {
                var showtimeObj = await _context.Showtimes.Where(x => x.TheaterId == request.TheaterId && x.Date == request.Day && x.Time == request.Time).FirstOrDefaultAsync();
                if (showtimeObj != null)
                {
                    throw new Exception("This ShowTime Is Already Added to this Theater");
                }
                showtimeObj = new Showtime()
                {
                    TheaterId = request.TheaterId,
                    Date = request.Day,
                    Time = request.Time,
                    MovieId = request.MovieId,

                };

                await _context.BookIns.AddAsync(new BookIn()
                {
                    LeftMax = request.LeftMax,
                    NormalMax = request.NormalMax,
                    RightMax = request.RightMax,
                    UpperMax = request.UpperMax,
                    Showtime = showtimeObj
                });



                //await _context.BookIns.AddAsync(bookInObj);
                //await _context.SaveChangesAsync();


                await _context.SaveChangesAsync();
                return Ok("Showtime Added ");
            }
            catch (Exception)
            {

                throw;
            }

        }

        [HttpPost]
        [Authorize(Roles = "Admin,User")]
        public async Task<IActionResult> BookTicket(BookingDto request)
        {
            try
            {

                var bookedBy = User?.FindFirstValue(ClaimTypes.Email) ?? "System";
                var showtimeObj = await _context.Showtimes.Where(x => x.Id == request.ShowtimeId).Include(x => x.Movie).Include(x => x.Theater).Include(x => x.BookIn).FirstOrDefaultAsync();
                if (showtimeObj == null)
                {
                    throw new Exception("ShowTimeId Not Found");
                }
                if (showtimeObj.BookIn.FilledSeats == showtimeObj.Theater.SeatingCapacity) { throw new Exception("HouseFull...!"); }
                var bookInObj = showtimeObj.BookIn;
                int seatNumber;

                var Details = "";
                switch (request.SitSelection)
                {
                    case "UpperSide":
                        {
                            if (bookInObj.UpperSide == bookInObj.UpperMax) { throw new Exception("This Side Sits are Filled"); }
                            bookInObj.UpperSide += 1;
                            bookInObj.FilledSeats += 1;
                            seatNumber = bookInObj.UpperSide;

                            break;
                        }
                    case "LeftSide":
                        {
                            if (bookInObj.LeftSide == bookInObj.LeftMax) { throw new Exception("This Side Sits are Filled"); }
                            bookInObj.LeftSide += 1;
                            bookInObj.FilledSeats += 1;
                            seatNumber = bookInObj.LeftSide;

                            break;
                        }
                    case "RightSide":
                        {
                            if (bookInObj.RightSide == bookInObj.UpperMax) { throw new Exception("This Side Sits are Filled"); }
                            bookInObj.RightSide += 1;
                            bookInObj.FilledSeats += 1;
                            seatNumber = bookInObj.RightSide;

                            break;
                        }
                    default:
                        {
                            if (bookInObj.Normal == bookInObj.NormalMax) { throw new Exception("This Side Sits are Filled"); }
                            bookInObj.Normal += 1;
                            bookInObj.FilledSeats += 1;
                            seatNumber = bookInObj.Normal;

                            break;
                        }

                }
                var ticket = new Ticket
                {
                    BookedBy = bookedBy,
                    MovieTitle = showtimeObj.Movie.Name,
                    TheaterName = showtimeObj.Theater.Name,
                    ShowTiming = showtimeObj.Date + " " + showtimeObj.Time,
                    BookIn = bookInObj,
                    TicketType=request.SitSelection,
                    SeatNumber = seatNumber

                };
                await _context.Tickets.AddAsync(ticket);
                await _context.SaveChangesAsync();
                var res= _mapper.Map<TicketReturnDto>(ticket);
                return Ok(res);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
