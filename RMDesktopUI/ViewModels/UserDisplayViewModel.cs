﻿using System.ComponentModel;
using System.Dynamic;
using System.Linq;
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
				NotifyOfPropertyChange(() => Users);
			}
		}

		private UserModel _selectedUser;

		public UserModel SelectedUser
		{
			get { return _selectedUser; }
			set
			{
				_selectedUser = value;
				SelectedUserName = value.Email;
				UserRoles.Clear();
				UserRoles = new BindingList<string>(value.Roles.Select(x => x.Value).ToList());
				LoadRoles();
				NotifyOfPropertyChange(() => SelectedUser);
			}
		}

		private string _selectedUserName;

		public string SelectedUserName
		{
			get { return _selectedUserName; }
			set
			{
				_selectedUserName = value;
				NotifyOfPropertyChange(() => SelectedUserName);
			}
		}

		private BindingList<string> _userRoles = new BindingList<string>();

		public BindingList<string> UserRoles
		{
			get { return _userRoles; }
			set
			{
				_userRoles = value;
				NotifyOfPropertyChange(() => UserRoles);
			}
		}

		private BindingList<string> _availableRoles = new BindingList<string>();

		public BindingList<string> AvailableRoles
		{
			get { return _availableRoles; }
			set
			{
				_availableRoles = value;
				NotifyOfPropertyChange(() => AvailableRoles);
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

		private async Task LoadRoles()
		{
			var roles = await _userEndpoint.GetAllRoles();
			foreach (var role in roles)
			{
				if (UserRoles.IndexOf(role.Value) < 0)
				{
					AvailableRoles.Add(role.Value);
				}
			}
		}

		private string _selectedUserRole;

		public string SelectedUserRole
		{
			get { return _selectedUserRole; }
			set
			{
				_selectedUserRole = value;
				NotifyOfPropertyChange(() => SelectedUserRole);
			}
		}

		private string _selectedAvailableRole;

		public string SelectedAvailableRole
		{
			get { return _selectedAvailableRole; }
			set
			{
				_selectedAvailableRole = value;
				NotifyOfPropertyChange(() => SelectedAvailableRole);
			}
		}

		public async void AddSelectedRole()
		{
			await _userEndpoint.AddUserToRole(SelectedUser.Id, SelectedAvailableRole);

			UserRoles.Add(SelectedAvailableRole);
			AvailableRoles.Remove(SelectedAvailableRole);
		}

		public async void RemoveSelectedRole()
		{
			await _userEndpoint.RemoveUserFromRole(SelectedUser.Id, SelectedUserRole);

			AvailableRoles.Add(SelectedAvailableRole);
			UserRoles.Remove(SelectedAvailableRole);
		}
	}
}