﻿using System.ComponentModel;
using System.Linq;
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
		private BindingList<CartItemModel> _cart = new BindingList<CartItemModel>();
		private int _itemQuantity = 1;
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

				if (ItemQuantity > 0 && SelectedProduct?.QuantityInStock >= ItemQuantity)
				{
					output = true;
				}
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

		public BindingList<CartItemModel> Cart
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
				NotifyOfPropertyChange(() => CanAddToCart);
			}
		}

		private ProductModel _selectedProduct;

		public ProductModel SelectedProduct
		{
			get { return _selectedProduct; }
			set
			{
				_selectedProduct = value;
				NotifyOfPropertyChange(() => SelectedProduct);
				NotifyOfPropertyChange(() => CanAddToCart);
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
			get
			{
				decimal subTotal = 0;
				foreach (var item in Cart)
				{
					subTotal += item.Product.RetailPrice * item.QuantityInCart;
				}
				return subTotal.ToString("C");
			}
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
			CartItemModel existingItem = Cart.FirstOrDefault(x => x.Product == SelectedProduct);

			if (existingItem != null)
			{
				existingItem.QuantityInCart += ItemQuantity;
				//TODO Find a better way of refreshing
				Cart.Remove(existingItem);
				Cart.Add(existingItem);
			}
			else
			{
				CartItemModel item = new CartItemModel
				{
					Product = SelectedProduct,
					QuantityInCart = ItemQuantity
				};
				Cart.Add(item);
			}

			SelectedProduct.QuantityInStock -= ItemQuantity;
			ItemQuantity = 1;
			NotifyOfPropertyChange(() => SubTotal);
			NotifyOfPropertyChange(() => existingItem.DisplayText);
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
			NotifyOfPropertyChange(() => SubTotal);
		}

		#endregion Public Methods
	}
}