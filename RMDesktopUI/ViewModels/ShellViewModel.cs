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

		public ShellViewModel(IEventAggregator eventAggregator, SalesViewModel salesVM, SimpleContainer container)
		{
			_event = eventAggregator;
			_salesVM = salesVM;
			_container = container;

			_event.Subscribe(this);

			ActivateItem(_container.GetInstance<LoginViewModel>());
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