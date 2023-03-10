using HotelAppMVVM.Exceptions;
using HotelAppMVVM.Models;
using HotelAppMVVM.Services;
using HotelAppMVVM.Stores;
using HotelAppMVVM.ViewModels;
using System;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows;

namespace HotelAppMVVM.Commands;

internal class MakeReservationCommand : ASyncCommandBase
{
    private readonly MakeReservationViewModel _makeReservationViewModel;
    private readonly HotelStore _hotelStore;
    private readonly NavigationService _reservationViewNavigationService;

    public MakeReservationCommand(MakeReservationViewModel makeReservationViewModel, 
        HotelStore hotelStore, 
        NavigationService reservationViewNavigationService)
    {
        _makeReservationViewModel = makeReservationViewModel;
        _hotelStore = hotelStore;
        _reservationViewNavigationService = reservationViewNavigationService;
        _makeReservationViewModel.PropertyChanged += OnViewModelPropertyChanged;
    }

    private void OnViewModelPropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(MakeReservationViewModel.Username) ||
            e.PropertyName == nameof(MakeReservationViewModel.FloorNumber))
        {
            OnCanExecuteChanged();
        }
    }

    public override bool CanExecute(object? parameter)
    {
        return string.IsNullOrEmpty(_makeReservationViewModel.Username) == false && 
            _makeReservationViewModel.FloorNumber > 0 && base.CanExecute(parameter);
    }

    public override async Task ExecuteAsync(object? parameter)
    {
        Reservation reservation = new Reservation(
            new RoomId(_makeReservationViewModel.FloorNumber, _makeReservationViewModel.RoomNumber),
            _makeReservationViewModel.StartDate,
            _makeReservationViewModel.EndDate,
            _makeReservationViewModel.Username
            );

        try
        {
            await _hotelStore.MakeReservation(reservation);

            MessageBox.Show("Successfully booked room", "Success", MessageBoxButton.OK, MessageBoxImage.Information);

            _reservationViewNavigationService.Navigate();

        }
        catch (ReservationConflictException)
        {

            MessageBox.Show("This room is already taken", "Error", MessageBoxButton.OK, MessageBoxImage.Error);

        }
        catch (Exception)
        {
            MessageBox.Show("Failed to make reservation", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}
