using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TravelExpertsDB;

[Index("AgentId", Name = "EmployeesCustomers")]
public partial class Customer
{
    [Key]
    public int CustomerId { get; set; }

    [Required(ErrorMessage = "Please enter your first name.")]
    [StringLength(25)]
    [DisplayName("First Name:")]
    public string CustFirstName { get; set; } = null!;

    [Required(ErrorMessage = "Please enter your last name.")]
    [StringLength(25)]
    [DisplayName("Last Name:")]
    public string CustLastName { get; set; } = null!;

    [Required(ErrorMessage = "Please enter a street address.")]
    [StringLength(75)]
    [DisplayName("Street Address:")]
    public string CustAddress { get; set; } = null!; //got

    [Required(ErrorMessage = "Please enter a City.")]
    [StringLength(50)]
    [DisplayName("City:")]
    public string CustCity { get; set; } = null!; //got

    [Required]
    [StringLength(2, ErrorMessage = "Please select a Province")]
    [DisplayName("Province:")]
    public string CustProv { get; set; } = null!; // create a drop down menu

    [Required(ErrorMessage = "Please enter a Postal code")]
    [StringLength(7)]
    [DisplayName("Postal Code:")]
    [RegularExpression("^[ABCEGHJ-NPRSTVXYabceghj-nprstvxy][0-9][ABCEGHJ-NPRSTV-Zabceghj-nprstv-z][ -]?[0-9][ABCEGHJ-NPRSTV-Zabceghj-nprstv-z][0-9]$", ErrorMessage = "Please enter a valid Postal code. (A1A 1A1)")]
    public string CustPostal { get; set; } = null!;

    [Required(ErrorMessage = "Please enter a Country.")]    
    [StringLength(25)]
    [DisplayName("Country:")]
    public string CustCountry { get; set; } = null!;

    [Required(ErrorMessage = "Please enter a phone number.")]
    [RegularExpression("^\\d{3}\\d{7,17}$", ErrorMessage = "Please enter a valid phone number. (4034443333)")]
    [StringLength(20)]
    [DisplayName("Phone:")]
    public string CustHomePhone { get; set; } = null!; //got

    [StringLength(20)]
    [RegularExpression("^\\d{3}\\d{7,17}$", ErrorMessage = "Please enter a valid phone number. (4034443333)")]
    [DisplayName("Business Phone:")]
    public string? CustBusPhone { get; set; } // not required but check for format

    [Required(ErrorMessage = "Please enter an email. Your email is used to sign in.")]
    [StringLength(50)]
    [RegularExpression("^[\\w-\\.]+@([\\w-]+\\.)+[\\w-]{2,4}$", ErrorMessage = "Please enter a valid email address. (example@email.com)")]
    [DisplayName("Email:")]
    public string? CustEmail { get; set; } // not required but check for format

    public int? AgentId { get; set; }

    [Required(ErrorMessage = "Please enter a Password.")]
    [StringLength(100, ErrorMessage = "Password must be less than 30 characters.")]
    [MinLength(8, ErrorMessage = "Password must be at least 8 characters.")]
    [DisplayName("Password:")]
    [Unicode(false)]
    public string? Password { get; set; } // got

    [Required(ErrorMessage = "Please confirm your password.")]
    [Display(Name = "Confirm Password:")]
    [Compare("Password", ErrorMessage = "Passwords to not match.")]
    [NotMapped]
    public string? ConfirmPassword { get; set; } //got

    [ForeignKey("AgentId")]
    [InverseProperty("Customers")]
    public virtual Agent? Agent { get; set; }

    [InverseProperty("Customer")]
    public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();

    [InverseProperty("Customer")]
    public virtual ICollection<CreditCard> CreditCards { get; set; } = new List<CreditCard>();

    [InverseProperty("Customer")]
    public virtual ICollection<CustomersReward> CustomersRewards { get; set; } = new List<CustomersReward>();

}
