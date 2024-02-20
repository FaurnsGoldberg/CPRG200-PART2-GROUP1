using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelExpertsDB
{
    /// <summary>
    /// Repository for methods to work with OrderDTO. (OrderDTO object is used to display necessary booking information)
    /// </summary>
    public class OrderDB
    {
        /// <summary>
        /// Creates an OrderDTO object.
        /// </summary>
        /// <param name="bookId">booking id</param>
        /// <param name="date">booking date</param>
        /// <param name="travellers">traveller count</param>
        /// <param name="packId">package id</param>
        /// <param name="packName">package name</param>
        /// <param name="packPrice">package price</param>
        /// <returns>a OrderDTO object containing all provided data</returns>
        public OrderDTO CreateOrder(int bookId, DateTime? date, double? travellers, int? packId, string packName, decimal packPrice) 
        {
            decimal orderTotal = packPrice * (decimal)travellers;
            OrderDTO order = new OrderDTO()
            {
                BookingID = bookId,
                OrderDate = date,
                TravelerCount = travellers,
                PackageId = packId,
                PackageName = packName,
                PackagePrice = packPrice,
                OrderTotal = orderTotal
            };
            return order;
        }

        /// <summary>
        /// Creates OrderDTO object with provided booking id
        /// </summary>
        /// <param name="db">db context</param>
        /// <param name="id">booking id</param>
        /// <returns>OrderDTO object containing all information related to provided booking id</returns>
        public OrderDTO GetOrderDetails(TravelExpertsContext db, int? id)
        {
            // Retrieve bookings for the specified customer
            Booking booking = db.Bookings.FirstOrDefault(b => b.BookingId == id)!;

            // Create a list to store OrderDTO objects
            OrderDTO order = new OrderDTO()
            {
                BookingID = booking.BookingId,
                OrderDate = booking.BookingDate,
                TravelerCount = booking.TravelerCount,
                PackageId = booking.PackageId
            };

            // Retrieve package information
            Package package = db.Packages.FirstOrDefault(p => p.PackageId == booking.PackageId);
            if (package != null)
            {
                order.PackageName = package.PkgName;
                order.PackagePrice = package.PkgBasePrice;
                order.OrderTotal = (decimal)order.TravelerCount * order.PackagePrice;
            }

            // Return the OrderDTO object
            return order;

        }
    }
}
