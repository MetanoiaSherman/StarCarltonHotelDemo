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
    public class RoomTypesController : ApiController
    {
        readonly protected StarCarltonHotelDemo.Models.StarCarltonHotelDemo db = new StarCarltonHotelDemo.Models.StarCarltonHotelDemo ( );
        protected string responseText = "";

        [ResponseType ( typeof ( List<RoomType> ) )]
        [Route ( "RoomTypes/GetRoomTypes" )]
        public IHttpActionResult GetRoomTypes ( )
        {
            return Ok ( db.RoomTypes.ToList ( ) );
        }

        [ResponseType ( typeof ( RoomType ) )]
        [Route ( "RoomTypes/GetRoomTypeById" )]
        public IHttpActionResult GetRoomTypeById ( )
        {
            int typeId = Convert.ToInt32 ( HttpContext.Current.Request.QueryString [ "typeid" ] );

            return Ok ( db.RoomTypes.Single ( t => t.RoomTypeId == typeId ) );
        }

        [ResponseType ( typeof ( System.String ) )]
        [Route ( "RoomTypes/CreateRoomType" )]
        public IHttpActionResult CreateRoomType ( )
        {
            string typeTitle = HttpContext.Current.Request.QueryString [ "typeTitle" ];

            var typeToCreate = new RoomType ( )
            {
                RoomTypeTitle = typeTitle
            };

            try
            {
                db.RoomTypes.Add ( typeToCreate );
                db.SaveChanges ( );
            }
            catch ( Exception ex )
            {
                Console.WriteLine ( ex.Message );
            }

            responseText = "Room Type: " + typeTitle + " Created";

            return Ok ( responseText );
        }

        [ResponseType ( typeof ( System.String ) )]
        [Route ( "RoomTypes/UpdateRoomType" )]
        public IHttpActionResult UpdateRoomType ( )
        {
            int typeId = Convert.ToInt32 ( HttpContext.Current.Request.QueryString [ "typeid" ] );
            string typeTitle = HttpContext.Current.Request.QueryString [ "typeTitle" ];

            RoomType typeToUpdate = db.RoomTypes.Single ( t => t.RoomTypeId == typeId );

            typeToUpdate.RoomTypeTitle = typeTitle;

            try
            {
                db.RoomTypes.Attach ( typeToUpdate );
                db.Entry ( typeToUpdate ).State = EntityState.Modified;
                db.SaveChanges ( );
            }
            catch ( Exception ex )
            {
                Console.WriteLine ( ex.Message );
            }

            responseText = "Room Type: " + typeTitle + " Updated";

            return Ok ( responseText );
        }

        [ResponseType(typeof(System.String))]
        [Route( "RoomTypes/DeleteRoomType" )]
        public IHttpActionResult DeleteRoomType ( )
        {
            int typeId = Convert.ToInt32 ( HttpContext.Current.Request.QueryString [ "typeid" ] );

            RoomType typeToDelete = db.RoomTypes.Single ( t => t.RoomTypeId == typeId );

            responseText = "Room Type: " + typeToDelete.RoomTypeTitle + " Deleted";

            try
            {
                db.RoomTypes.Attach ( typeToDelete );
                db.RoomTypes.Remove ( typeToDelete );
                db.SaveChanges ( );
            }
            catch (Exception ex)
            {
                Console.WriteLine ( ex.Message );
            }

            return Ok ( responseText );
        }
    }
}
