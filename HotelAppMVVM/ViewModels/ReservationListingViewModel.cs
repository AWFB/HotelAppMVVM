using HotelAppMVVM.Commands;
using HotelAppMVVM.Models;
using HotelAppMVVM.Services;
using HotelAppMVVM.Stores;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace HotelAppMVVM.ViewModels;

public class ReservationListingViewModel : ViewModelBase
{
    private readonly ObservableCollection<ReservationViewModel> _reservations;
    private readonly Hotel _hotel;

    public IEnumerable<ReservationViewModel> Reservations => _reservations;

    public ICommand MakeReservationCommand { get; }

    public ReservationListingViewModel(Hotel hotel, NavigationService makeReservationNavigationService)
    {
        _hotel = hotel;

        _reservations = new ObservableCollection<ReservationViewModel>();

        MakeReservationCommand = new NavigateCommand(makeReservationNavigationService);

        UpdateReservations();
    }

    private void UpdateReservations()
    {
        _reservations.Clear();

        foreach (var reservation in _hotel.GetAllReservations())
        {
            ReservationViewModel reservationViewModel = new ReservationViewModel(reservation);
            _reservations.Add(reservationViewModel);
        }

    }

}
