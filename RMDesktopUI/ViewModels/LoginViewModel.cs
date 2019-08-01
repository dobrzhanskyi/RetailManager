using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Caliburn.Micro;
using RMDesktopUI.EventModels;
using RMDesktopUI.Library.Api;

namespace RMDesktopUI.ViewModels
{
	public class LoginViewModel : Screen
	{
		#region Private Fields

		private readonly IAPIHelper _apiHelper;
		private string _errorMessage;
		private string _password;
		private string _userName;
		private IEventAggregator _eventAggregator;

		#endregion Private Fields

		#region Public Constructors

		public LoginViewModel(IAPIHelper apiHelper,IEventAggregator eventAggregator)
		{
			_apiHelper = apiHelper;
			_eventAggregator = eventAggregator;
		}

		#endregion Public Constructors

		#region Public Properties

		public bool CanLogIn
		{
			get
			{
				bool output = false;
				if (Username?.Length > 0 && Password?.Length > 0)
				{
					output = true;
				}
				return output;
			}
		}

		public string ErrorMessage
		{
			get { return _errorMessage; }
			set
			{
				_errorMessage = value;
				NotifyOfPropertyChange(() => ErrorMessage);
				NotifyOfPropertyChange(() => IsErrorVisible);
			}
		}

		public bool IsErrorVisible
		{
			get
			{
				bool output = false;
				if (ErrorMessage?.Length > 0)
				{
					output = true;
				}
				return output;
			}
		}

		public string Password
		{
			get => _password;
			set
			{
				_password = value;
				NotifyOfPropertyChange(() => Password);
				NotifyOfPropertyChange(() => CanLogIn);
			}
		}

		public string Username
		{
			get => _userName;
			set
			{
				_userName = value;
				NotifyOfPropertyChange(Username);
				NotifyOfPropertyChange(() => CanLogIn);
			}
		}

		#endregion Public Properties

		#region Public Methods

		public async Task LogIn()
		{
			try
			{
				ErrorMessage = "";
				var result = await _apiHelper.Authenticate(Username, Password);
				//Capture more user information
				await _apiHelper.GetLoggedInUserInfo(result.Access_Token);

				_eventAggregator.PublishOnUIThread(new LogOnEventModel());
			}
			catch (Exception ex)
			{
				ErrorMessage = ex.Message;
			}
		}

		#endregion Public Methods
	}
}