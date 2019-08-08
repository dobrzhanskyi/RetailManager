using Caliburn.Micro;
using RMDesktopUI.EventModels;

namespace RMDesktopUI.ViewModels
{
	public class ShellViewModel : Conductor<object>, IHandle<LogOnEventModel>
	{
		#region Private Fields

		private IEventAggregator _event;
		private SalesViewModel _salesVM;
		private SimpleContainer _container;

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