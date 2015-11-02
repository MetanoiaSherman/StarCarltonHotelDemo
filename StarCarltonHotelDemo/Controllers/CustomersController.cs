using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;

using StarCarltonHotelDemo.Models;

namespace StarCarltonHotelDemo.Controllers
{
    public class CustomersController : ApiController
    {
        readonly protected StarCarltonHotelDemo.Models.StarCarltonHotelDemo db = new StarCarltonHotelDemo.Models.StarCarltonHotelDemo ( );
        private string responseText = "";

        [HttpGet]
        [ResponseType(typeof(List<Customer>))]
        [Route( "Customers/GetCustomers" )]
        public IHttpActionResult GetCustomers()
        {
            return Ok ( db.Customers.ToList ( ) );
        }
        
        [ResponseType(typeof( List<Customer> ) )]
        [Route ( "Customers/GetCustomersByCountry/{country}" )]
        public IHttpActionResult GetCustomersByCountry(string country)
        {
            return Ok ( db.Customers.Where ( c => c.Country == country ).ToList() );
        }
        
        [ResponseType(typeof(Customer))]
        [Route( "Customers/GetCustomerByEmail/" )]
        public IHttpActionResult GetCustomerByEmail()
        {
            string email = HttpContext.Current.Request.QueryString [ "email" ];
            Customer customer = db.Customers.Single ( c => c.Email == email );

            return Ok ( customer );
        }

        [ResponseType(typeof(Customer))]
        [Route( "Customers/GetCustomerById" )]
        public IHttpActionResult GetCustomerById()
        {
            int customerId = Convert.ToInt32 ( HttpContext.Current.Request.QueryString [ "customerid" ] );

            return Ok ( db.Customers.Single ( c => c.CustomerId == customerId ) );
        }

        [HttpPost]
        [ResponseType(typeof(System.String ))]
        [Route( "Customers/CreateCustomer" )]
        public IHttpActionResult CreateCustomer(Customer customerToCreate)
        {
            db.Customers.Add ( customerToCreate );
            db.SaveChanges ( );

            responseText = "Customer Created";

            return Ok ( responseText );
        }

        [HttpPost]
        [ResponseType(typeof(System.String))]
        [Route("Customers/UpdateCustomer")]
        public IHttpActionResult UpdateCustomer(Customer customerToUpdate )
        {
            try
            {
                db.Customers.Attach ( customerToUpdate );
                db.Entry ( customerToUpdate ).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges ( );
            }
            catch (Exception ex)
            {
                Console.WriteLine ( ex.Message );
            }

            responseText = "Customer Updated";

            return Ok ( responseText );
        }
    }
}
