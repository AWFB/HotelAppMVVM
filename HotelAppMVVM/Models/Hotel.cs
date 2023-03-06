using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelAppMVVM.Models;

public class Hotel
{
    private readonly ReservationBook _reservationBook;

    public string HotelName { get; }

    public Hotel(string hotelName)
    {
        HotelName = hotelName;
        _reservationBook = new ReservationBook();
    }

    public IEnumerable<Reservation> GetAllReservations()
    {
        return _reservationBook.GetAllReservations();
    }

    public void MakeReservation(Reservation reservation)
    {
        _reservationBook.AddReservation(reservation);
    }
}
