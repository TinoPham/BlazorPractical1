using BlazorProducts.Client.HttpRepository;
using BlazorProducts.Client.Shared;
using BlazorProducts.Shared.Entities;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace BlazorProducts.Client.Pages
{
	public partial class CreateProduct
	{
		private Product _product = new Product();
		private SuccessNotification _notification;

		[Inject]
		public IProductHttpRepository ProductRepo { get; set; }

		private async Task Create()
		{
			await ProductRepo.CreateProduct(_product);
			_notification.Show();
		}
	}
}
