using System.ComponentModel;

namespace RMDesktopUI.Models
{
	public class CartItemDisplayModel : INotifyPropertyChanged
	{
		private int _quantityInCart;

		public event PropertyChangedEventHandler PropertyChanged;

		public ProductDisplayModel Product { get; set; }

		public int QuantityInCart
		{
			get { return _quantityInCart; }
			set
			{
				_quantityInCart = value;
				CallPropertyChanged(nameof(QuantityInCart));
				CallPropertyChanged(nameof(DisplayText));
			}
		}

		public string DisplayText
		{
			get
			{
				return $"{Product.ProductName} ({QuantityInCart})";
			}
		}

		private void CallPropertyChanged(string propertyName)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}