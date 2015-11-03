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
    public class RoomsController : ApiController
    {
        readonly protected StarCarltonHotelDemo.Models.StarCarltonHotelDemo db = new StarCarltonHotelDemo.Models.StarCarltonHotelDemo ( );
        protected string responseText = "";

        [ResponseType(typeof(List<Room>))]
        [Route( "Rooms/GetRooms" )]
        public IHttpActionResult GetRooms()
        {
            return Ok ( db.Rooms.ToList ( ) );
        }

        [ResponseType ( typeof ( Room ) )]
        [Route ( "Rooms/GetRoomByRoomNo" )]
        public IHttpActionResult GetRoomByRoomNo()
        {
            string roomNo = HttpContext.Current.Request.QueryString [ "roomno" ];

            return Ok ( db.Rooms.Single ( r => r.RoomNo.Equals ( roomNo ) ) );
        }

        [ResponseType ( typeof ( List<Room> ) )]
        [Route ( "Rooms/GetRoomsByFloorNo" )]
        public IHttpActionResult GetRoomsByFloorNo()
        {
            int floorNo = Convert.ToInt32 ( HttpContext.Current.Request.QueryString [ "floorno" ] );

            return Ok ( db.Rooms.Where ( r => r.FloorNo == floorNo ).ToList ( ) );
        }

        [ResponseType ( typeof ( List<Room> ) )]
        [Route ( "Rooms/GetRoomsByType" )]
        public IHttpActionResult GetRoomsByType()
        {
            int typeId = Convert.ToInt32 ( HttpContext.Current.Request.QueryString [ "typeid" ] );

            return Ok ( db.Rooms.Where ( r => r.RoomTypeId == typeId ).ToList ( ) );
        }

        [ResponseType ( typeof ( List<Room> ) )]
        [Route ( "Rooms/GetRoomsByIsOccupied" )]
        public IHttpActionResult GetRoomsByIsOccupied()
        {
            bool isOccupied = Convert.ToBoolean ( HttpContext.Current.Request.QueryString [ "occupied" ] );

            return Ok ( db.Rooms.Where ( r => r.IsOccupied == isOccupied ).ToList ( ) );
        }

        [ResponseType ( typeof ( List<Room> ) )]
        [Route ( "Rooms/GetRoomsByIsReserved" )]
        public IHttpActionResult GetRoomsByIsReserved ( )
        {
            bool isReserved = Convert.ToBoolean ( HttpContext.Current.Request.QueryString [ "reserved" ] );

            return Ok ( db.Rooms.Where ( r => r.IsReserved == isReserved ).ToList ( ) );
        }

        [ResponseType ( typeof ( List<Room> ) )]
        [Route ( "Rooms/GetRoomsByStatus" )]
        public IHttpActionResult GetRoomsByStatus ( )
        {
            int statusId = Convert.ToInt32 ( HttpContext.Current.Request.QueryString [ "statusid" ] );

            return Ok ( db.Rooms.Where ( r => r.RoomStatusId == statusId ).ToList ( ) );
        }

        [HttpPost]
        [ResponseType(typeof(System.String))]
        [Route("Rooms/CreateRoom")]
        public IHttpActionResult CreateRoom(Room roomToCreate)
        {
            try
            {
                db.Rooms.Add ( roomToCreate );
                db.SaveChanges ( );
            }
            catch (Exception ex)
            {
                Console.WriteLine ( ex.Message );
            }

            responseText = "Room: " + roomToCreate.RoomNo + " (Level: " + roomToCreate.FloorNo.ToString ( ) + ") Created";

            return Ok ( responseText );
        }

        [HttpPost]
        [ResponseType ( typeof ( System.String ) )]
        [Route ( "Rooms/UpdateRoom" )]
        public IHttpActionResult UpdateRoom(Room roomToUpdate)
        {
            try
            {
                db.Rooms.Attach ( roomToUpdate );
                db.Entry ( roomToUpdate ).State = EntityState.Modified;
                db.SaveChanges ( );
            }
            catch (Exception ex)
            {
                Console.WriteLine ( ex.Message );
            }

            responseText = "Room: " + roomToUpdate.RoomNo + " (Level: " + roomToUpdate.FloorNo.ToString ( ) + ") Created";

            return Ok ( responseText );
        }

        [ResponseType ( typeof ( System.String ) )]
        [Route ( "Rooms/DeleteRoom" )]
        public IHttpActionResult DeleteRoom()
        {
            int roomId = Convert.ToInt32 ( HttpContext.Current.Request.QueryString [ "roomid" ] );

            try
            {
                Room roomToDelete = db.Rooms.Single ( r => r.RoomId == roomId );

                responseText = "Room: " + roomToDelete.RoomNo + " (Level: " + roomToDelete.FloorNo.ToString ( ) + ") Created";

                db.Rooms.Attach ( roomToDelete );
                db.Rooms.Remove ( roomToDelete );
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
