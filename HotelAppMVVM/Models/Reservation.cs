using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelAppMVVM.Models
{
    public class Reservation
    {
        public Reservation(RoomId roomId, DateTime startTime, DateTime endTime, string username)
        {
            RoomId = roomId;
            StartTime = startTime;
            EndTime = endTime;
            Username = username;
        }

        public string Username { get; }
        public RoomId RoomId { get; }
        public DateTime StartTime { get; }
        public DateTime EndTime { get; }
        public TimeSpan Length => EndTime.Subtract(StartTime);
    }
}
