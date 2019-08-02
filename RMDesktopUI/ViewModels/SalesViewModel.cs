using System.ComponentModel;
using System.Threading.Tasks;
using Caliburn.Micro;
using RMDesktopUI.Library.Api;
using RMDesktopUI.Library.Models;

namespace RMDesktopUI.ViewModels
{
	public class SalesViewModel : Screen
	{
		#region Private Fields

		private readonly IProductEndpoint _productEndpoint;
		private BindingList<string> _cart;
		private int _itemQuantity;
		private BindingList<ProductModel> _products;
		#endregion Private Fields


		#region Public Constructors

		public SalesViewModel(IProductEndpoint productEndpoint)
		{
			_productEndpoint = productEndpoint;
		}

		#endregion Public Constructors


		#region Protected Methods

		protected override async void OnViewLoaded(object view)
		{
			base.OnViewLoaded(view);
			await LoadProducts();
		}

		#endregion Protected Methods

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

		public BindingList<ProductModel> Products
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

		public async Task LoadProducts()
		{
			var productList = await _productEndpoint.GetAll();
			Products = new BindingList<ProductModel>(productList);
		}
		public void RemoveFromCart()
		{
		}

		#endregion Public Methods
	}
}