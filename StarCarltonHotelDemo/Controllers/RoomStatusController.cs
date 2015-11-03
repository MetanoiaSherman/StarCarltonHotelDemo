using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;

using System.Data.Entity;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;

using StarCarltonHotelDemo.Models;

namespace StarCarltonHotelDemo.Controllers
{
    public class RoomStatusController : ApiController
    {
        readonly protected StarCarltonHotelDemo.Models.StarCarltonHotelDemo db = new StarCarltonHotelDemo.Models.StarCarltonHotelDemo ( );
        protected string responseText = "";

        [ResponseType(typeof(List<RoomStatus>))]
        [Route( "RoomStatus/GetRoomStatuses" )]
        public IHttpActionResult GetRoomStatuses()
        {
            return Ok ( db.RoomStatus.ToList ( ) );
        }

        [ResponseType(typeof(RoomStatus))]
        [Route( "RoomStatus/GetRoomStatusById" )]
        public IHttpActionResult GetRoomStatusById ()
        {
            int statusId = Convert.ToInt32 ( HttpContext.Current.Request.QueryString [ "statusId" ] );

            return Ok ( db.RoomStatus.Single ( s => s.RoomStatusId == statusId ) );
        }

        [ResponseType(typeof(System.String))]
        [Route("RoomStatus/CreateRoomStatus")]
        public IHttpActionResult CreateRoomStatus()
        {
            string statusTitle = HttpContext.Current.Request.QueryString [ "statusTitle" ];

            var statusToCreate = new RoomStatus ( )
            {
                RoomStatusTitle = statusTitle
            };

            try
            {
                db.RoomStatus.Add ( statusToCreate );
                db.SaveChanges ( );
            }
            catch (Exception ex)
            {
                Console.WriteLine ( ex.Message );
            }

            responseText = "Room Status: " + statusTitle + " Created";

            return Ok ( responseText );
        }

        [ResponseType(typeof(System.String))]
        [Route( "RoomStatus/UpdateRoomStatus" )]
        public IHttpActionResult UpdateRoomStatus()
        {
            int statusId = Convert.ToInt32 ( HttpContext.Current.Request.QueryString [ "statusId" ] );
            string statusTitle = HttpContext.Current.Request.QueryString [ "statusTitle" ];

            RoomStatus statusToUpdate = db.RoomStatus.Single ( s => s.RoomStatusId == statusId );

            statusToUpdate.RoomStatusTitle = statusTitle;

            try
            {
                db.RoomStatus.Attach ( statusToUpdate );
                db.Entry ( statusToUpdate ).State = EntityState.Modified;
                db.SaveChanges ( );
            }
            catch (Exception ex)
            {
                Console.WriteLine ( ex.Message );
            }

            responseText = "Room Status: " + statusTitle + " Updated";

            return Ok ( responseText );
        }

        [ResponseType(typeof(System.String))]
        [Route( "RoomStatus/DeleteRoomStatus" )]
        public IHttpActionResult DeleteRoomStatus()
        {
            int statusId = Convert.ToInt32 ( HttpContext.Current.Request.QueryString [ "statusId" ] );
            RoomStatus statusToDelete = db.RoomStatus.Single ( s => s.RoomStatusId == statusId );

            try
            {
                responseText = "Room Status: " + statusToDelete.RoomStatusTitle + " Deleted";

                db.RoomStatus.Attach ( statusToDelete );
                db.RoomStatus.Remove ( statusToDelete );
                db.SaveChanges ( );
            }
            catch ( Exception ex )
            {
                Console.WriteLine ( ex.Message );
            }

            return Ok ( responseText );
        }
    }
}
