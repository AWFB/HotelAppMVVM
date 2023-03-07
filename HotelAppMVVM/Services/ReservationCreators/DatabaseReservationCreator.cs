using HotelAppMVVM.Data;
using HotelAppMVVM.DTOs;
using HotelAppMVVM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelAppMVVM.Services.ReservationCreators
{
    public class DatabaseReservationCreator : IReservationCreator
    {

        private readonly HotelDbContextFactory _dbContextFactory;

        public DatabaseReservationCreator(HotelDbContextFactory dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        public async Task CreateReservation(Reservation reservation)
        {
            using (HotelDbContext context = _dbContextFactory.CreateDbContext())
            {
                ReservationDTO reservationDTO = ToReservationDTO(reservation);

                context.Reservations.Add(reservationDTO);
                await context.SaveChangesAsync();
            }
        }

        private ReservationDTO ToReservationDTO(Reservation reservation)
        {
            return new ReservationDTO()
            {
                FloorNumber = reservation.RoomId?.FloorNumber ?? 0,
                RoomNumber = reservation.RoomId?.RoomNumber ?? 0,
                Username = reservation.Username,
                StartTime = reservation.StartTime,
                EndTime = reservation.EndTime,
            };
            

        }
    }
}
