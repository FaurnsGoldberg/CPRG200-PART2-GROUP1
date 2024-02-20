using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TravelExpertsWebMVC.Models

{
    public class BookingPaymentViewModel
    {

        [Display(Name = "Remaining Balance")]
        [DisplayFormat(DataFormatString = "{0:C}")]
        public decimal ? Balance { get; set; }


        public string PkgName { get; set; } = null!;

        public DateTime? BookingDate { get; set; }

        [Required(ErrorMessage = "You must enter a valid number.")]
        public decimal ? Payment {  get; set; }

        public int ? BookingId { get; set; }
    }
}
