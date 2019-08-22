using Caliburn.Micro;
using RMDesktopUI.EventModels;
using RMDesktopUI.Library.Models;

namespace RMDesktopUI.ViewModels
{
	public class ShellViewModel : Conductor<object>, IHandle<LogOnEventModel>
	{
		#region Private Fields

		private readonly IEventAggregator _event;
		private readonly SalesViewModel _salesVM;
		private readonly ILoggedInUserModel _user;

		#endregion Private Fields

		#region Public Constructors

		public ShellViewModel(IEventAggregator eventAggregator, SalesViewModel salesVM, ILoggedInUserModel user)
		{
			_event = eventAggregator;
			_salesVM = salesVM;
			_user = user;
			_event.Subscribe(this);

			ActivateItem(IoC.Get<LoginViewModel>());
		}

		#endregion Public Constructors

		#region Public Properties

		public bool IsLoggedIn
		{
			get
			{
				bool output = false;
				if (string.IsNullOrWhiteSpace(_user.Token) == false)
				{
					output = true;
				}
				return output;
			}
		}

		#endregion Public Properties

		#region Public Methods

		public void ExitApplication()
		{
			TryClose();
		}

		public void Handle(LogOnEventModel message)
		{
			ActivateItem(_salesVM);
			NotifyOfPropertyChange(() => IsLoggedIn);
		}

		public void LogOut()
		{
			_user.LogOffUser();
			ActivateItem(IoC.Get<LoginViewModel>());
			NotifyOfPropertyChange(() => IsLoggedIn);
		}
		#endregion Public Methods
	}
}