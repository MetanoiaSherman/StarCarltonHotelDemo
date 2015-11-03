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
    public class ReservationStatusController : ApiController
    {
        readonly protected StarCarltonHotelDemo.Models.StarCarltonHotelDemo db = new StarCarltonHotelDemo.Models.StarCarltonHotelDemo ( );
        protected string responseText = "";

        [ResponseType(typeof(List<ReservationStatus>))]
        [Route( "ReservationStatus/GetStatus" )]
        public IHttpActionResult GetStatus()
        {
            return Ok ( db.ReservationStatus.ToList ( ) );
        }

        [ResponseType(typeof(ReservationStatus))]
        [Route("ReservationStatus/GetStatusById")]
        public IHttpActionResult GetStatusById()
        {
            int statusId = Convert.ToInt32 ( HttpContext.Current.Request.QueryString [ "statusid" ] );

            return Ok ( db.ReservationStatus.Single ( s => s.ReservationStatusId == statusId ) );
        }

        [ResponseType(typeof(System.String))]
        [Route( "ReservationStatus/CreateReservationStatus" )]
        public IHttpActionResult CreateReservationStatus ()
        {
            string statusTitle = HttpContext.Current.Request.QueryString [ "statusTitle" ];

            var statusToCreate = new ReservationStatus ( )
            {
                ReservationStatusTitle = statusTitle
            };

            db.ReservationStatus.Add ( statusToCreate );
            db.SaveChanges ( );

            responseText = "Reservation Status: " + statusTitle + " Created";

            return Ok ( responseText );
        }

        [ResponseType(typeof(System.String))]
        [Route( "ReservationStatus/UpdateReservationStatus" )]
        public IHttpActionResult UpdateReservationStatus()
        {
            int statusId = Convert.ToInt32 ( HttpContext.Current.Request.QueryString [ "statusid" ] );
            string statusTitle = HttpContext.Current.Request.QueryString [ "statusTitle" ];

            ReservationStatus statusToUpdate = db.ReservationStatus.Single ( s => s.ReservationStatusId == statusId );

            statusToUpdate.ReservationStatusTitle = statusTitle;

            try
            {
                db.ReservationStatus.Attach ( statusToUpdate );
                db.Entry ( statusToUpdate ).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges ( );
            }
            catch (Exception ex)
            {
                Console.WriteLine ( ex.Message );
            }

            responseText = "Reservation Status: " + statusToUpdate.ReservationStatusTitle + " Updated";

            return Ok ( responseText );
        }

        [ResponseType(typeof(System.String))]
        [Route( "ReservationStatus/DeleteReservationStatus" )]
        public IHttpActionResult DeleteReservationStatus ()
        {
            int statusId = Convert.ToInt32 ( HttpContext.Current.Request.QueryString [ "statusid" ] );

            ReservationStatus statusToDelete = db.ReservationStatus.Single ( s => s.ReservationStatusId == statusId );

            responseText = "Reservation Status: " + statusToDelete.ReservationStatusTitle + " Updated";

            db.ReservationStatus.Attach ( statusToDelete );
            db.ReservationStatus.Remove ( statusToDelete );
            db.SaveChanges ( );

            return Ok ( responseText );
        }
    }
}
