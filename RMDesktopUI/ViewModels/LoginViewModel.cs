using System;
using Caliburn.Micro;

namespace RMDesktopUI.ViewModels
{
	public class LoginViewModel : Screen
	{
		private string _userName;
		private string _password;

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

		public void LogIn()
		{

		}
	}
}