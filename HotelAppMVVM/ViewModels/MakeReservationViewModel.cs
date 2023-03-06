using HotelAppMVVM.Commands;
using HotelAppMVVM.Models;
using HotelAppMVVM.Services;
using HotelAppMVVM.Stores;
using System;
using System.Windows.Input;

namespace HotelAppMVVM.ViewModels;

public class MakeReservationViewModel : ViewModelBase
{
    public ICommand SubmitCommand { get; }
    public ICommand CancelCommand { get; }

    public MakeReservationViewModel(
        Hotel hotel, 
        NavigationService reservationViewNavigationService)
    {
        SubmitCommand = new MakeReservationCommand(this, hotel, reservationViewNavigationService);
        CancelCommand = new NavigateCommand(reservationViewNavigationService);
    }

    private string _username;
    public string Username
    {
        get
        {
            return _username;
        }
        set
        {
            _username = value;
            OnPropertyChanged(nameof(Username));
        }
    }

    private int _roomNumber;
    public int RoomNumber
    {
        get
        {
            return _roomNumber;
        }
        set
        {
            _roomNumber = value;
            OnPropertyChanged(nameof(RoomNumber));
        }
    }

    private int _floorNumber;
    public int FloorNumber
    {
        get
        {
            return _floorNumber;
        }
        set
        {
            _floorNumber = value;
            OnPropertyChanged(nameof(FloorNumber));
        }
    }

    private DateTime _startDate = DateTime.Now;
    public DateTime StartDate
    {
        get
        {
            return _startDate;
        }
        set
        {
            _startDate = value;
            OnPropertyChanged(nameof(StartDate));
        }
    }

    private DateTime _endDate = DateTime.Now.AddDays(1);
    public DateTime EndDate
    {
        get
        {
            return _endDate;
        }
        set
        {
            _endDate = value;
            OnPropertyChanged(nameof(EndDate));
        }
    }

}
