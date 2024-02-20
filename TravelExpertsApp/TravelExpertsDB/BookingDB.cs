using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelExpertsDB
{
    public class BookingDB
    {
        public static List<TripType> GetTripTypes(TravelExpertsContext db)
        {
            List <TripType> types = db.TripTypes.OrderBy(x => x.Ttname).ToList();
            return types;
        }
        public static Booking CreateBooking(int custId, int? packId, int numTravel, string tripType)
        {
            Booking booking = new Booking();
            booking.BookingDate = DateTime.Now; ;
            booking.CustomerId = custId;
            booking.PackageId = packId;
            booking.TravelerCount = numTravel;
            if(tripType != null)
            {
                booking.TripTypeId = tripType[0].ToString();
            }
            return booking;
        }
        public static bool DeleteBooking(TravelExpertsContext db, Booking booking)
        {
            db.Bookings.Remove(booking);
            db.SaveChanges();
            return true;
        }
        public static bool SaveBooking(TravelExpertsContext db, Booking booking)
        {
            db.Bookings.Add(booking);
            db.SaveChanges();
            return true;
        }
        public static Booking FindBooking(TravelExpertsContext db, int? bookingId)
        {
            Booking booking; 
            var query =  db.Bookings.Include(b => b.Package).Include(b => b.TripType).Where(b => b.BookingId == bookingId);
            booking = query.First() ;
            return booking;
        }

        public static List<Booking> GetAllBookings(TravelExpertsContext db, int customerId)
        {
            List<Booking> bookings = db.Bookings.Include(b => b.Package).Include(b => b.TripType).Where(b => b.CustomerId == customerId).OrderByDescending(b => b.BookingDate).ToList();
            return bookings;
        }

    }
}