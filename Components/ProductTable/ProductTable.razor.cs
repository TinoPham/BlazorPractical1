using BlazorProducts.Shared.Entities;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.IO;

namespace BlazorProducts.Client.Components.ProductTable
{
	public partial class ProductTable
	{
		[Parameter]
		public List<Product> Products { get; set; }

		[Inject]
		public NavigationManager NavigationManager { get; set; }
		private void RedirectToUpdate(Guid id)
		{
			var url = Path.Combine("/updateProduct/", id.ToString());
			NavigationManager.NavigateTo(url);
		}
	}
}
