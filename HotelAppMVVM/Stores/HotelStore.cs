using HotelAppMVVM.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HotelAppMVVM.Stores;

public class HotelStore
{
    private readonly List<Reservation> _reservations;
    private readonly Hotel _hotel;
    private readonly Lazy<Task> _initializeLazy;

    // Use IEnumerbale here to prvent any external class from adding or removing reservations
    public IEnumerable<Reservation> Reservations => _reservations;

    public HotelStore(Hotel hotel)
    {
        _reservations = new List<Reservation>();
        _hotel = hotel;
        _initializeLazy = new Lazy<Task>(Initialize);
    }

    // Create a quieriable list and popualate with reservations from database
    // Prevents having to call the database every time you switch to reservation list
    public async Task Load()
    {
        await _initializeLazy.Value;
    }

    public async Task MakeReservation(Reservation reservation)
    {
        // Add to database
        await _hotel.MakeReservation(reservation);

        // add to list of in memory reservations
        _reservations.Add(reservation);
    }

    private async Task Initialize()
    {
        IEnumerable<Reservation> reservations = await _hotel.GetAllReservations();

        _reservations.Clear();
        _reservations.AddRange(reservations);
    }
}
