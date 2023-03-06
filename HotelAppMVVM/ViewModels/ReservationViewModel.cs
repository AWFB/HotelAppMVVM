using HotelAppMVVM.Models;
using System;

namespace HotelAppMVVM.ViewModels;

public class ReservationViewModel : ViewModelBase
{
    private readonly Reservation _reservation;

    public ReservationViewModel(Reservation reservation)
    {
        _reservation = reservation;
    }

    public string Username => _reservation.Username;
    public string RoomId => _reservation.RoomId?.ToString();
    public string StartDate => _reservation.StartTime.Date.ToString("d");
    public string EndDate => _reservation.EndTime.Date.ToString("d");


}
