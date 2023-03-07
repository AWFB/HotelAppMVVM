using HotelAppMVVM.Commands;
using HotelAppMVVM.Models;
using HotelAppMVVM.Services;
using HotelAppMVVM.Stores;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace HotelAppMVVM.ViewModels;

public class ReservationListingViewModel : ViewModelBase
{
    private readonly ObservableCollection<ReservationViewModel> _reservations;

    public IEnumerable<ReservationViewModel> Reservations => _reservations;

    public ICommand MakeReservationCommand { get; }
    public ICommand LoadReservationsCommand { get; }

    public ReservationListingViewModel(HotelStore hotel, NavigationService makeReservationNavigationService)
    {
        _reservations = new ObservableCollection<ReservationViewModel>();

        LoadReservationsCommand = new LoadReservationsCommand(hotel, this);
        MakeReservationCommand = new NavigateCommand(makeReservationNavigationService);
    }

    public static ReservationListingViewModel LoadViewModel(HotelStore hotelStore, NavigationService makeReservationNavigationService)
    {
        ReservationListingViewModel viewModel = new ReservationListingViewModel(hotelStore, makeReservationNavigationService);

        viewModel.LoadReservationsCommand.Execute(null);

        return viewModel;
    }

    public void UpdateReservations(IEnumerable<Reservation> reservations)
    {
        _reservations.Clear();

        foreach (var reservation in reservations)
        {
            ReservationViewModel reservationViewModel = new ReservationViewModel(reservation);
            _reservations.Add(reservationViewModel);
        }

    }

}
