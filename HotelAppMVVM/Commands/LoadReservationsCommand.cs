using HotelAppMVVM.Models;
using HotelAppMVVM.Stores;
using HotelAppMVVM.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace HotelAppMVVM.Commands;

public class LoadReservationsCommand : ASyncCommandBase
{

    private readonly HotelStore _hotelStore;
    private readonly ReservationListingViewModel _ViewModel;

    public LoadReservationsCommand(HotelStore hotelStore, ReservationListingViewModel viewModel)
    {
        _hotelStore = hotelStore;
        _ViewModel = viewModel;
    }

    public override async Task ExecuteAsync(object? parameter)
    {
        try
        {
            await _hotelStore.Load();

            _ViewModel.UpdateReservations(_hotelStore.Reservations);
        }
        catch (Exception)
        {
            MessageBox.Show("Failed to load reservations", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}
