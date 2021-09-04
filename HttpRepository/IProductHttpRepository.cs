using BlazorProducts.Client.Features;
using BlazorProducts.Shared.Entities;
using BlazorProducts.Shared.RequestFetures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorProducts.Client.HttpRepository
{
	public interface IProductHttpRepository
	{
		Task<PagingResponse<Product>> GetProductsAsync(ProductParameters productParameters);
	}
}
