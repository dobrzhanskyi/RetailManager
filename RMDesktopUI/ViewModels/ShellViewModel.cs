using Caliburn.Micro;

namespace RMDesktopUI.ViewModels
{
	public class ShellViewModel : Conductor<object>
	{
		private LoginViewModel _loginVM;

		public ShellViewModel(LoginViewModel loginViewModel)
		{
			_loginVM = loginViewModel;
			ActivateItem(_loginVM);
		}
	}
}