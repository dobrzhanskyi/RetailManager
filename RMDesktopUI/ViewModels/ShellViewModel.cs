using Caliburn.Micro;
using RMDesktopUI.EventModels;

namespace RMDesktopUI.ViewModels
{
	public class ShellViewModel : Conductor<object>, IHandle<LogOnEventModel>
	{
		#region Private Fields

		private readonly IEventAggregator _event;
		private readonly SalesViewModel _salesVM;

		#endregion Private Fields

		#region Public Constructors

		public ShellViewModel(IEventAggregator eventAggregator, SalesViewModel salesVM)
		{
			_event = eventAggregator;
			_salesVM = salesVM;

			_event.Subscribe(this);

			ActivateItem(IoC.Get<LoginViewModel>());
		}

		#endregion Public Constructors

		#region Public Methods

		public void Handle(LogOnEventModel message)
		{
			ActivateItem(_salesVM);
		}

		#endregion Public Methods
	}
}