using HotelAppMVVM.Exceptions;
using HotelAppMVVM.Models;
using System;
using System.Collections.Generic;
using System.Windows;

namespace HotelAppMVVM
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            Hotel hotel = new Hotel("Antonys B&B");
            try
            {

                hotel.MakeReservation(new Reservation(
                    new RoomId(1, 1),
                    new DateTime(2023, 1, 1),
                    new DateTime(2023, 1, 2),
                    "Steve"
                    ));

                hotel.MakeReservation(new Reservation(
                    new RoomId(1, 2),
                    new DateTime(2023, 1, 3),
                    new DateTime(2023, 1, 4),
                    "Brian"
                    ));

            }
            catch (ReservationConflictException)
            {

                throw;
            }
            IEnumerable<Reservation> reservations = hotel.GetAllReservations();

            base.OnStartup(e);
        }

    }
}
