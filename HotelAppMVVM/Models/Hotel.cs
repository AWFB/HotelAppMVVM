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

    public Hotel(string hotelName, ReservationBook reservationBook)
    {
        HotelName = hotelName;
        _reservationBook = reservationBook;
    }

    public async Task<IEnumerable<Reservation>> GetAllReservations()
    {
        return await _reservationBook.GetAllReservations();
    }

    public async Task MakeReservation(Reservation reservation)
    {
        await _reservationBook.AddReservation(reservation);
    }
}
