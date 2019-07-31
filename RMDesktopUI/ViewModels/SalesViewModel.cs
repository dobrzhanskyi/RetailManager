using System.ComponentModel;
using Caliburn.Micro;

namespace RMDesktopUI.ViewModels
{
	public class SalesViewModel : Screen
	{
		private BindingList<string> _products;
		private string _itemQuantity;
		private BindingList<string> _cart;

		public BindingList<string> Products
		{
			get { return _products; }
			set
			{
				_products = value;
				NotifyOfPropertyChange(() => Products);
			}
		}

		public string ItemQuantity
		{
			get { return _itemQuantity; }
			set
			{
				_itemQuantity = value;
				NotifyOfPropertyChange(() => ItemQuantity);
			}
		}

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

		public string SubTotal
		{
			//TODO Make calculation
			get { return "$0.00"; }
		}

		public string Total
		{
			//TODO Make calculation
			get { return "$0.00"; }
		}

		public string Tax
		{
			//TODO Make calculation
			get { return "$0.00"; }
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

		public void CheckOut()
		{
		}

		public void RemoveFromCart()
		{
		}

		public void AddToCart()
		{
		}
	}
}