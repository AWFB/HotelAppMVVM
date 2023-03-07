using HotelAppMVVM.Data;
using HotelAppMVVM.DTOs;
using HotelAppMVVM.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelAppMVVM.Services.ReservationProviders;

public class DatabaseReservationProvider : IReservationProvider
{
    private readonly HotelDbContextFactory _dbContextFactory;

    public DatabaseReservationProvider(HotelDbContextFactory dbContextFactory)
    {
        _dbContextFactory = dbContextFactory;
    }

    public async Task<IEnumerable<Reservation>> GetAllReservations()
    {
        using (HotelDbContext context = _dbContextFactory.CreateDbContext())
        {
            IEnumerable<ReservationDTO> reservationDTOs = await context.Reservations.ToListAsync();

            return reservationDTOs.Select(r => ToReservation(r));
        }
    }

    private static Reservation ToReservation(ReservationDTO r)
    {
        return new Reservation(new RoomId(r.FloorNumber, r.RoomNumber), r.StartTime, r.EndTime, r.Username);
    }
}
