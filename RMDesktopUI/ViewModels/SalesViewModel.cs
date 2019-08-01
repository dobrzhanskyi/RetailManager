using System.ComponentModel;
using Caliburn.Micro;

namespace RMDesktopUI.ViewModels
{
	public class SalesViewModel : Screen
	{
		#region Private Fields

		private BindingList<string> _cart;
		private BindingList<string> _products;
		private int _itemQuantity;

		#endregion Private Fields

		#region Public Properties

		public bool CanAddToCart
		{
			get
			{
				bool output = false;
				//TODO if something is selected
				//TODO check item quantity
				return output;
			}
		}

		public bool CanCheckOut
		{
			get
			{
				bool output = false;
				//TODO if something in the cart
				return output;
			}
		}

		public bool CanRemoveFromCart
		{
			get
			{
				bool output = false;
				//TODO if something is selected
				return output;
			}
		}

		public BindingList<string> Cart
		{
			get { return _cart; }
			set
			{
				_cart = value;
				NotifyOfPropertyChange(() => Cart);
			}
		}

		public int ItemQuantity
		{
			get { return _itemQuantity; }
			set
			{
				_itemQuantity = value;
				NotifyOfPropertyChange(() => ItemQuantity);
			}
		}

		public BindingList<string> Products
		{
			get { return _products; }
			set
			{
				_products = value;
				NotifyOfPropertyChange(() => Products);
			}
		}

		public string SubTotal
		{
			//TODO Make calculation
			get { return "$0.00"; }
		}

		public string Tax
		{
			//TODO Make calculation
			get { return "$0.00"; }
		}

		public string Total
		{
			//TODO Make calculation
			get { return "$0.00"; }
		}

		#endregion Public Properties

		#region Public Methods

		public void AddToCart()
		{
		}

		public void CheckOut()
		{
		}

		public void RemoveFromCart()
		{
		}

		#endregion Public Methods
	}
}