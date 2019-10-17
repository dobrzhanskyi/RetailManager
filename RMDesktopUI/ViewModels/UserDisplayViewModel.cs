using System.ComponentModel;
using System.Dynamic;
using System.Threading.Tasks;
using System.Windows;
using Caliburn.Micro;
using RMDesktopUI.Library.Api;
using RMDesktopUI.Library.Models;

namespace RMDesktopUI.ViewModels
{
	public class UserDisplayViewModel : Screen
	{
		private readonly StatusInfoViewModel _status;
		private readonly IWindowManager _window;
		private readonly IUserEndPoint _userEndpoint;

		private BindingList<UserModel> _users;

		public BindingList<UserModel> Users
		{
			get
			{
				return _users;
			}
			set
			{
				_users = value;
				NotifyOfPropertyChange(() => Users); ;
			}
		}

		public UserDisplayViewModel(StatusInfoViewModel status, IWindowManager windowManager, IUserEndPoint userEndpoint)
		{
			_status = status;
			_window = windowManager;
			_userEndpoint = userEndpoint;
		}

		protected override async void OnViewLoaded(object view)
		{
			base.OnViewLoaded(view);
			try
			{
				await LoadUsers();
			}
			catch (System.Exception ex)
			{
				dynamic settings = new ExpandoObject();
				settings.WindowStartUpLocation = WindowStartupLocation.CenterOwner;
				settings.ResizeMode = ResizeMode.NoResize;
				settings.Title = "System Error";

				if (ex.Message == "Unauthorized")
				{
					_status.UpdateMessage("Unauthorized Access", "You don't have permissions to interact SalesView Form");
					_window.ShowDialog(_status, null, settings);
				}
				else
				{
					_status.UpdateMessage("Fatal Exceprion", ex.Message);
					_window.ShowDialog(_status, null, settings);
				}

				TryClose();
			}
		}

		public async Task LoadUsers()
		{
			var userList = await _userEndpoint.GetAll();

			Users = new BindingList<UserModel>(userList);
		}
	}
}