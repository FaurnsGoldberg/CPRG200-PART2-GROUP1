using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelExpertsDB
{
    public class OrderDTO
    {
        public int BookingID { get; set; }
        [Display(Name = "Booked Date")]
        [DisplayFormat(DataFormatString = "{0:MMMM dd, yyyy}")]
        public DateTime? OrderDate { get; set; }
        [Display(Name = "Total Travellers")]
        public double? TravelerCount { get; set; }
        [Display(Name = "Package ID")]
        public int? PackageId { get; set; }
        [Display(Name = "Package Name")]
        public string? PackageName { get; set; }
        [Display(Name = "Package Price")]
        [DisplayFormat(DataFormatString = "{0:C}")]
        public decimal? PackagePrice { get; set; }
        [Display(Name = "Order Total")]
        [DisplayFormat(DataFormatString = "{0:C}")]
        public decimal? OrderTotal { get; set; }
        [Display(Name = "Trip Type")]
        public string? TripType { get; set; }


    }
}
