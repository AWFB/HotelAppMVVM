using HotelAppMVVM.Data;
using HotelAppMVVM.Models;
using HotelAppMVVM.Services;
using HotelAppMVVM.Services.ReservationConflictValidators;
using HotelAppMVVM.Services.ReservationCreators;
using HotelAppMVVM.Services.ReservationProviders;
using HotelAppMVVM.Stores;
using HotelAppMVVM.ViewModels;
using Microsoft.EntityFrameworkCore;
using System.Windows;

namespace HotelAppMVVM;

public partial class App : Application
{
    private readonly Hotel _hotel;
    private readonly HotelStore _hotelStore;
    private readonly NavigationStore _navigationStore;
    private readonly HotelDbContextFactory _hotelDbContextFactory;
    private const string CONNECTION_STRING = "Data Source=HotelApp.db";

    public App()
    {
        _hotelDbContextFactory = new HotelDbContextFactory(CONNECTION_STRING);

        IReservationProvider reservationProvider = new DatabaseReservationProvider(_hotelDbContextFactory);
        IReservationCreator reservationCreator = new DatabaseReservationCreator(_hotelDbContextFactory);
        IReservationConflictValidator reservationConflictValidator = new DatabaseReservationConflictValidator(_hotelDbContextFactory);

        ReservationBook reservationBook = new ReservationBook(reservationProvider, reservationCreator, reservationConflictValidator);
        _hotel = new Hotel("Antony's B&B", reservationBook);
        _hotelStore = new HotelStore(_hotel);
        _navigationStore = new NavigationStore();
    }

    protected override void OnStartup(StartupEventArgs e)
    {
        using (HotelDbContext dbContext = _hotelDbContextFactory.CreateDbContext())
        {
            dbContext.Database.Migrate();
        }

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
        return new MakeReservationViewModel(_hotelStore, new NavigationService(_navigationStore, CreateReservationViewModel));
    }

    private ReservationListingViewModel CreateReservationViewModel()
    {
        return ReservationListingViewModel.LoadViewModel(_hotelStore, new NavigationService(_navigationStore, CreateMakeReservationViewModel));
    }

}
