using HotelAppMVVM.Data;
using HotelAppMVVM.DTOs;
using HotelAppMVVM.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelAppMVVM.Services.ReservationConflictValidators;

public class DatabaseReservationConflictValidator : IReservationConflictValidator
{
    private readonly HotelDbContextFactory _dbContextFactory;

    public DatabaseReservationConflictValidator(HotelDbContextFactory dbContextFactory)
    {
        _dbContextFactory = dbContextFactory;
    }

    public async Task<Reservation> GetConflictingReservation(Reservation reservation)
    {
        using (HotelDbContext context = _dbContextFactory.CreateDbContext())
        {
            ReservationDTO reservationDTO = await context.Reservations
                .Where(r => r.FloorNumber == reservation.RoomId.FloorNumber)
                .Where(r => r.RoomNumber == reservation.RoomId.RoomNumber)
                .Where(r => r.EndTime > reservation.StartTime)
                .Where(r => r.StartTime < reservation.EndTime)
                .FirstOrDefaultAsync();

            if (reservationDTO == null)
            {
                return null;
            }

            return ToReservation(reservationDTO);
        }
    }

    private static Reservation ToReservation(ReservationDTO r)
    {
        return new Reservation(new RoomId(r.FloorNumber, r.RoomNumber), r.StartTime, r.EndTime, r.Username);
    }
}
