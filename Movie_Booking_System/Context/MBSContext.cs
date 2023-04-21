using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Movie_Booking_System.Models;

namespace Movie_Booking_System.Context
{
    public class MBSContext:DbContext
    {
        public MBSContext(DbContextOptions<MBSContext>options):base(options) { }

        public virtual DbSet<Movie> Movies { get; set; }

        public virtual DbSet<MovieTheaterHandler> MovieTheaters { get; set; }

        public virtual DbSet<BookIn> BookIns { get; set; }
        public virtual DbSet<Ticket> Tickets { get; set; }
        public virtual DbSet<Showtime> Showtimes { get; set; }
        public virtual DbSet<Theater> Theaters { get; set; }
        public virtual DbSet<User> Users { get; set; }
    }
}
