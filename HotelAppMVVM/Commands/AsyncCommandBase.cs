using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelAppMVVM.Commands;

public abstract class ASyncCommandBase : CommandBase
{

	private bool _isExecuting;
	public bool IsExecuting
	{
		get
		{
			return _isExecuting;
		}
		set
		{
			_isExecuting = value;
			OnCanExecuteChanged();
		}
	}

	// stops button spamming
    public override bool CanExecute(object? parameter)
    {
        return !IsExecuting && base.CanExecute(parameter);
    }

    public override async void Execute(object? parameter)
    {
        IsExecuting = true;

        try
		{
            await ExecuteAsync(parameter);
        }
		finally
		{
            IsExecuting = false;
        }
    }

    public abstract Task ExecuteAsync(object? parameter);
}
