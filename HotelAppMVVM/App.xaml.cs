using HotelAppMVVM.Exceptions;
using HotelAppMVVM.Models;
using HotelAppMVVM.Services;
using HotelAppMVVM.Stores;
using HotelAppMVVM.ViewModels;
using System;
using System.Collections.Generic;
using System.Windows;

namespace HotelAppMVVM;

public partial class App : Application
{
    private readonly Hotel _hotel;
    private readonly NavigationStore _navigationStore;

    public App()
    {
        _hotel = new Hotel("Antony's B&B");
        _navigationStore = new NavigationStore();
    }

    protected override void OnStartup(StartupEventArgs e)
    {
        _navigationStore.CurrentViewModel = CreateMakeReservationViewModel();

        MainWindow = new MainWindow()
        {
            DataContext = new MainViewModel(_navigationStore)
        };

        MainWindow.Show();

        base.OnStartup(e);
    }

    private MakeReservationViewModel CreateMakeReservationViewModel()
    {
        return new MakeReservationViewModel(_hotel, new NavigationService(_navigationStore, CreateReservationViewModel));
    }

    private ReservationListingViewModel CreateReservationViewModel()
    {
        return new ReservationListingViewModel(_hotel, new NavigationService(_navigationStore, CreateMakeReservationViewModel));
    }

}
