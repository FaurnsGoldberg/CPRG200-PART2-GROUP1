using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelExpertsDB
{
    public static class CustomerManager
    {
        /// <summary>
        /// authenticates customer by finding matching data
        /// </summary>
        /// <param name="db">database context</param>
        /// <param name="email">email to match</param>
        /// <param name="password">password to match</param>
        /// <returns>null if no Customer with the matching email and password was found. Customer if found.</returns>
        public static Customer Authenticate(TravelExpertsContext db, string email, string password)
        {
            var cust = db.Customers.SingleOrDefault(cst => cst.CustEmail == email
                                                    && cst.Password == password);
            return cust; //this will either be null or an object
        }

        /// <summary>
        /// adds customer to the database
        /// </summary>
        /// <param name="db">database context</param>
        /// <param name="customer">Customer to add</param>
        public static void CreateCustomer(TravelExpertsContext db, Customer customer)
        {
                db.Customers.Add(customer);
                db.SaveChanges();
        }

        /// <summary>
        /// updates selected customer
        /// </summary>
        /// <param name="db">database context</param>
        /// <param name="id">customer id</param>
        /// <param name="updatedCustomer">customer object with new data</param>
        public static void UpdateCustomer(TravelExpertsContext db, int id, Customer updatedCustomer)
        {
            Customer? customer = db.Customers.Find(id);
            if (customer != null)
            {
                customer.CustFirstName = updatedCustomer.CustFirstName;
                customer.CustLastName = updatedCustomer.CustLastName;
                customer.CustEmail = updatedCustomer.CustEmail;
                customer.CustAddress = updatedCustomer.CustAddress;
                customer.CustCity = updatedCustomer.CustCity;
                customer.CustCountry = updatedCustomer.CustCountry;
                customer.CustPostal = updatedCustomer.CustPostal;
                customer.CustProv = updatedCustomer.CustProv;
                if(updatedCustomer.CustBusPhone != null)
                {
                    customer.CustBusPhone = updatedCustomer.CustBusPhone;
                }
                db.Customers.Update(customer);
                db.SaveChanges(); // save  changes to the database
            }
        }

        /// <summary>
        /// updates the password on the selected customer
        /// </summary>
        /// <param name="db">database context</param>
        /// <param name="id">selected customer id</param>
        /// <param name="newPassword">new password</param>
        public static void UpdatePassword(TravelExpertsContext db, int? id, string newPassword)
        {
            Customer? customer = db.Customers.Find(id);
            if (customer != null)
            {
                customer.Password = newPassword;
                db.Customers.Update(customer);
                db.SaveChanges();
            }
        }

        /// <summary>
        /// validates unique email for database
        /// </summary>
        /// <param name="db">database context</param>
        /// <param name="email">email to test</param>
        /// <returns>true if a matching email exists. false if not.</returns>
        public static bool EmailExists(TravelExpertsContext db, string email)
        {
                bool exists = db.Customers.Any(cst => cst.CustEmail.ToLower() == email.ToLower());
                return exists;
        }

        /// <summary>
        /// validates unqie email for database, 
        /// excluding the one already assigned to the selected customer.
        /// </summary>
        /// <param name="db">database context</param>
        /// <param name="id">customer id</param>
        /// <param name="email">email to test</param>
        /// <returns>true if a matching email exists. false if not</returns>
        public static bool NewEmailExists(TravelExpertsContext db, int? id, string email)
        {
            bool exists = db.Customers.Any(cst => cst.CustomerId != id && cst.CustEmail.ToLower() == email.ToLower());
            return exists;

        }

        /// <summary>
        /// gets the customer of a given id
        /// </summary>
        /// <param name="db">database context</param>
        /// <param name="id">customer id</param>
        /// <returns>Customer with matching id</returns>
        public static Customer GetCustomerData(TravelExpertsContext db, int? id)
        {
            var cust = db.Customers.SingleOrDefault(cst => cst.CustomerId == id);
            return cust;
        }

    }
}
