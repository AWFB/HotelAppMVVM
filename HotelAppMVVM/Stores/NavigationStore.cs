using HotelAppMVVM.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelAppMVVM.Stores;

public class NavigationStore
{
    private ViewModelBase _currentViewModel;

    public ViewModelBase CurrentViewModel
    {
        get => _currentViewModel;
        set
        {
            // prob need to implement dispose
            _currentViewModel = value;
            OnCurrentViewModelCHanged();
        }
    }
    public event Action CurrentViewModelChanged;

    private void OnCurrentViewModelCHanged()
    {
        CurrentViewModelChanged?.Invoke();
    }

    
}
